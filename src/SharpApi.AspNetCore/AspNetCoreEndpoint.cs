using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
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
            var routeContext = new RouteContext(context);
            await ApiEndpointManager.Router.RouteAsync(routeContext);

            var handler = routeContext.Handler;

            if (handler == null)
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                return;
            }

            await handler(context);
        }
    }
}
