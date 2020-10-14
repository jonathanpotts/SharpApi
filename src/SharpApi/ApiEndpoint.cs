using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Primitives;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharpApi
{
    /// <summary>
    /// An endpoint that handles API requests.
    /// </summary>
    public abstract class ApiEndpoint
    {
        /// <summary>
        /// Creates a new instance of the endpoint.
        /// </summary>
        public ApiEndpoint()
        {
        }

        public async Task HandleAsync(HttpContext context)
        {
            var headers = context.Request.Headers.ToDictionary(h => h.Key, h => (IList<string>)h.Value.ToList());
            var query = context.Request.Query.ToDictionary(q => q.Key, q => (IList<string>)q.Value.ToList());
            var routeValues = context.Request.RouteValues.ToDictionary(d => d.Key, d => d.Value);

            using var apiRequest = new ApiRequest(headers, query, routeValues, context.Request.Body);
            using var result = await RunAsync(apiRequest);

            foreach (var header in result.Headers ?? Enumerable.Empty<KeyValuePair<string, List<string>>>())
            {
                context.Response.Headers.Add(header.Key, new StringValues(header.Value.ToArray()));
            }

            context.Response.Headers.ContentLength = result.Body?.Length;

            context.Response.StatusCode = (int)result.StatusCode;

            if (context.Request.Method.ToUpper() != "HEAD")
            {
                await result.Body.CopyToAsync(context.Response.Body);
            }
        }

        /// <summary>
        /// Runs upon receiving API requests.
        /// </summary>
        /// <param name="request">API request data.</param>
        /// <returns>API response data.</returns>
        public abstract Task<ApiResult> RunAsync(ApiRequest request);
    }

    /// <summary>
    /// An endpoint that handles API requests.
    /// </summary>
    /// <typeparam name="T">Model type for body data.</typeparam>
    public abstract class ApiEndpoint<T> : ApiEndpoint
    {
        /// <summary>
        /// Creates a new instance of the endpoint.
        /// </summary>
        public ApiEndpoint()
            : base()
        {
        }

        /// <summary>
        /// Runs upon receiving API requests.
        /// </summary>
        /// <param name="request">API request data.</param>
        /// <returns>API response data.</returns>
        public abstract Task<ApiResult> RunAsync(ApiRequest<T> request);

        public override Task<ApiResult> RunAsync(ApiRequest request) => RunAsync(new ApiRequest<T>(request));
    }
}
