using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
        /// Handles proxy integration requests to the API.
        /// </summary>
        /// <param name="input">Request data stream from API Gateway.</param>
        /// <returns>Response data stream to API Gateway.</returns>
        public static async Task<APIGatewayProxyResponse> HandleAsync(APIGatewayProxyRequest request, ILambdaContext context)
        {
            var features = new FeatureCollection();
            features.Set<IHttpRequestFeature>(new HttpRequestFeature()
            {
                Method = request.HttpMethod,
                Path = request.Path,
                Body = new MemoryStream(request.IsBase64Encoded ? Convert.FromBase64String(request.Body) : Encoding.UTF8.GetBytes(request.Body))
            });
            features.Set<IHttpResponseFeature>(new HttpResponseFeature());

            var factory = new DefaultHttpContextFactory(ApiEndpointManager.ServiceProvider);
            var httpContext = factory.Create(features);

            var routeContext = new RouteContext(httpContext);
            await ApiEndpointManager.Router.RouteAsync(routeContext);

            var handler = routeContext.Handler;

            if (handler == null)
            {

            }

            var endpoint = ApiEndpointManager.GetApiEndpoint(request.HttpMethod, request.Path);

            if (endpoint == null)
            {
                return new APIGatewayProxyResponse
                {
                    StatusCode = (int)HttpStatusCode.NotFound
                };
            }

            using var body = new MemoryStream(request.IsBase64Encoded ? Convert.FromBase64String(request.Body) : Encoding.UTF8.GetBytes(request.Body));
            using var apiRequest = new ApiRequest(request.MultiValueHeaders, request.MultiValueQueryStringParameters, null, body);
            using var result = await endpoint.RunAsync(apiRequest);

            var response = new APIGatewayProxyResponse
            {
                StatusCode = (int)result.StatusCode,
                MultiValueHeaders = result.Headers.ToDictionary(d => d.Key, d => (IList<string>)d.Value)
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
                response.Body = Convert.ToBase64String(bytes);
                response.IsBase64Encoded = true;
            }

            return response;
        }
    }
}
