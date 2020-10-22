using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpApi.AwsLambda
{
    /// <summary>
    /// An endpoint that handles Lambda requests through proxy integration.
    /// </summary>
    public static class AwsLambdaProxyEndpoint
    {
        /// <summary>
        /// Text-based content types.
        /// </summary>
        private static readonly string[] s_textContentTypes =
        {
            "text/plain",
            "text/csv",
            "text/html",
            "text/css",
            "text/javascript",
            "application/javascript",
            "application/json",
            "application/ld+json",
            "text/xml",
            "application/xml"
        };

        /// <summary>
        /// Handles proxy integration requests to the API.
        /// </summary>
        /// <param name="request">Request data stream from API Gateway.</param>
        /// <param name="context">AWS Lambda context that the function is being executed in.</param>
        /// <returns>Response data stream to API Gateway.</returns>
        public static async Task<APIGatewayProxyResponse> HandleAsync(APIGatewayProxyRequest request, ILambdaContext context)
        {
            var router = AwsLambdaProgram.ServiceProvider.GetService<IRouter>();

            var routeValues = new Dictionary<string, object>();

            ApiEndpoint endpoint;

            endpoint = router.Route(request.HttpMethod, request.Path, routeValues);

            if (endpoint == null)
            {
                return new APIGatewayProxyResponse
                {
                    StatusCode = StatusCodes.Status404NotFound
                };
            }

            Stream body = null;

            if (request.Body?.Length > 0)
            {
                body = new MemoryStream(request.IsBase64Encoded ? Convert.FromBase64String(request.Body) : Encoding.UTF8.GetBytes(request.Body));
            }

            using var apiRequest = new ApiRequest(request.MultiValueHeaders, request.MultiValueQueryStringParameters, routeValues, body);
            using var result = await endpoint.RunAsync(apiRequest);

            body?.Dispose();

            var response = new APIGatewayProxyResponse
            {
                StatusCode = result.StatusCode,
                MultiValueHeaders = result.Headers
            };

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

            response.MultiValueHeaders.Add("Content-Length", new List<string> { bytes.Length.ToString() });

            if (request.HttpMethod.ToUpper() != "HEAD")
            {
                var contentType = result.Headers["Content-Type"]?.FirstOrDefault();

                if (contentType != null && s_textContentTypes.Contains(contentType.Split(';').First().Trim().ToLower()))
                {
                    response.Body = Encoding.UTF8.GetString(bytes);
                    response.IsBase64Encoded = false;
                }
                else
                {
                    response.Body = Convert.ToBase64String(bytes);
                    response.IsBase64Encoded = true;
                }
            }

            return response;
        }
    }
}
