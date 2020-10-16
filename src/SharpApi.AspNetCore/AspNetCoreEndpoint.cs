using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
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
            var result = await ProcessRequest(context);

            if (result == null)
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                return;
            }

            await new SharpApiResult(result).ExecuteAsync(context);
        }

        /// <summary>
        /// Processes an API request.
        /// </summary>
        /// <param name="context">HTTP context used by the request.</param>
        /// <returns>API result for the request (null if endpoint was not found).</returns>
        public static async Task<ApiResult> ProcessRequest(HttpContext context)
        {
            var router = context.RequestServices.GetService<IRouter>();

            var routeValues = new Dictionary<string, object>();
            var endpoint = router.Route(context.Request.Method, context.Request.Path, routeValues);

            if (endpoint == null)
            {
                return null;
            }

            var headers = context.Request.Headers.ToDictionary(h => h.Key, h => (IList<string>)h.Value.ToList());
            var query = context.Request.Query.ToDictionary(q => q.Key, q => (IList<string>)q.Value.ToList());

            var request = new ApiRequest(headers, query, routeValues, context.Request.Body);
            return await endpoint.RunAsync(request);
        }
    }
}
