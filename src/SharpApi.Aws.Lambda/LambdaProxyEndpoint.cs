using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;

namespace SharpApi.Aws.Lambda
{
    /// <summary>
    /// An endpoint that handles Lambda requests through proxy integration.
    /// </summary>
    public class LambdaProxyEndpoint
    {
        /// <summary>
        /// Map of endpoint paths to handler types.
        /// </summary>
        private static readonly Dictionary<string, Type> s_endpoints = new Dictionary<string, Type>();

        /// <summary>
        /// Handles proxy integration requests to the API.
        /// </summary>
        /// <param name="input">Request data stream from API Gateway.</param>
        /// <returns>Response data stream to API Gateway.</returns>
        public async Task<Stream> Handler(Stream input)
        {
            var request = await JsonSerializer.DeserializeAsync<LambdaProxyRequest>(input);

            var endpointKey = $"{request.HttpMethod} {request.Path}";

            if (!s_endpoints.TryGetValue(endpointKey, out var endpointType))
            {
                var routeEndpoints = AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(a => a.GetTypes())
                    .Where(t => typeof(ApiEndpoint).IsAssignableFrom(t))
                    .Select(t => (Type: t, Attribute: t.GetCustomAttribute<ApiEndpointAttribute>()))
                    .Where(t => t.Attribute?.Path == request.Path);

                endpointType = routeEndpoints.FirstOrDefault(t => t.Attribute.Method == request.HttpMethod).Type;

                // Use GET endpoint for HEAD requests when applicable
                if (endpointType == null && request.HttpMethod == "HEAD")
                {
                    endpointType = routeEndpoints.FirstOrDefault(t => t.Attribute.Method == "GET" && t.Attribute.UseGetForHead).Type;
                }

                if (endpointType == null)
                {
                    return await new LambdaProxyResponse((int)HttpStatusCode.NotFound)
                        .ToJsonStreamAsync();
                }

                s_endpoints.Add(endpointKey, endpointType);
            }

            var endpoint = (ApiEndpoint)Activator.CreateInstance(endpointType);

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
