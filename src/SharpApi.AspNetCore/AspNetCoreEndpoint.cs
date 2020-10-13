using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SharpApi.AspNetCore
{
    /// <summary>
    /// An endpoint that handles ASP.NET Core requests.
    /// </summary>
    public static class AspNetCoreEndpoint
    {
        /// <summary>
        /// Handles requests to the API.
        /// </summary>
        /// <param name="context">ASP.NET Core pipeline context.</param>
        /// <returns>Task that handles the request asynchronously.</returns>
        public static async Task HandleAsync(HttpContext context)
        {
            var endpoint = ApiEndpointManager.GetApiEndpoint(context.Request.Method, context.Request.Path);

            if (endpoint == null)
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                return;
            }

            var headers = context.Request.Headers.ToDictionary(h => h.Key, h => (IList<string>)h.Value.ToList());
            var query = context.Request.Query.ToDictionary(q => q.Key, q => (IList<string>)q.Value.ToList());

            using var apiRequest = new ApiRequest(headers, query, context.Request.Body);
            using var result = await endpoint.RunAsync(apiRequest);

            await new SharpApiResult(result).ExecuteAsync(context);
        }
    }
}
