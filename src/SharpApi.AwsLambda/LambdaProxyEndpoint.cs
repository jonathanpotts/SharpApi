using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SharpApi.Aws.Lambda
{
    /// <summary>
    /// An endpoint that handles Lambda requests through proxy integration.
    /// </summary>
    public static class LambdaProxyEndpoint
    {
        /// <summary>
        /// Handles proxy integration requests to the API.
        /// </summary>
        /// <param name="input">Request data stream from API Gateway.</param>
        /// <returns>Response data stream to API Gateway.</returns>
        public static async Task<Stream> HandleAsync(Stream input)
        {
            var request = await JsonSerializer.DeserializeAsync<LambdaProxyRequest>(input);

            var endpoint = ApiEndpointManager.GetApiEndpoint(request.HttpMethod, request.Path);

            if (endpoint == null)
            {
                return await new LambdaProxyResponse((int)HttpStatusCode.NotFound).ToJsonStreamAsync();
            }

            using var body = new MemoryStream(request.IsBase64Encoded ? Convert.FromBase64String(request.Body) : Encoding.UTF8.GetBytes(request.Body));
            using var apiRequest = new ApiRequest(request.MultiValueHeaders, request.MultiValueQueryStringParameters, body);
            using var result = await endpoint.RunAsync(apiRequest);

            LambdaProxyResponse response;

            if (request.HttpMethod == "HEAD")
            {
                response = new LambdaProxyResponse((int)result.StatusCode)
                {
                    MultiValueHeaders = result.Headers
                };
            }
            else
            {
                byte[] bytes;

                if (result.Body is MemoryStream ms)
                {
                    bytes = ms.ToArray();
                }
                else
                {
                    using (ms = new MemoryStream())
                    {
                        await result.Body.CopyToAsync(ms);
                        bytes = ms.ToArray();
                    }
                }

                response = new LambdaProxyResponse((int)result.StatusCode, Convert.ToBase64String(bytes), true)
                {
                    MultiValueHeaders = result.Headers
                };

                response.MultiValueHeaders.Add("Content-Length", new List<string> { bytes.Length.ToString() });
            }

            return await response.ToJsonStreamAsync();
        }
    }
}
