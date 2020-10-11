using Amazon.Lambda.APIGatewayEvents;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
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
        public static async Task<APIGatewayProxyResponse> HandleAsync(APIGatewayProxyRequest request)
        {
            var endpoint = ApiEndpointManager.GetApiEndpoint(request.HttpMethod, request.Path);

            if (endpoint == null)
            {
                return new APIGatewayProxyResponse
                {
                    StatusCode = (int)HttpStatusCode.NotFound
                };
            }

            using var body = new MemoryStream(request.IsBase64Encoded ? Convert.FromBase64String(request.Body) : Encoding.UTF8.GetBytes(request.Body));
            using var apiRequest = new ApiRequest(request.MultiValueHeaders, request.MultiValueQueryStringParameters, body);
            using var result = await endpoint.RunAsync(apiRequest);

            var response = new APIGatewayProxyResponse
            {
                StatusCode = (int)result.StatusCode,
                MultiValueHeaders = result.Headers.ToDictionary(d => d.Key, d => (IList<string>)d.Value)
            };

            if (request.HttpMethod != "HEAD")
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

                response.Body = Convert.ToBase64String(bytes);
                response.IsBase64Encoded = true;

                response.MultiValueHeaders.Add("Content-Length", new List<string> { bytes.Length.ToString() });
            }

            return response;
        }
    }
}
