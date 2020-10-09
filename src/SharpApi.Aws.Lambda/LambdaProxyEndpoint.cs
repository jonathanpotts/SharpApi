using System.IO;
using System.Net;
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
        public static async Task<Stream> Handler(Stream input)
        {
            var request = await JsonSerializer.DeserializeAsync<LambdaProxyRequest>(input);

            var endpoint = ApiEndpointManager.GetApiEndpoint(request.HttpMethod, request.Path);

            if (endpoint == null)
            {
                return await new LambdaProxyResponse((int)HttpStatusCode.NotFound).ToJsonStreamAsync();
            }

            var apiRequest = new ApiRequest(request.MultiValueHeaders, request.MultiValueQueryStringParameters, request.Body);
            var result = await endpoint.RunAsync(apiRequest);

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
                response = new LambdaProxyResponse((int)result.StatusCode, result.Body)
                {
                    IsBase64Encoded = result.BodyIsBase64Encoded,
                    MultiValueHeaders = result.Headers
                };
            }

            return await response.ToJsonStreamAsync();
        }
    }
}
