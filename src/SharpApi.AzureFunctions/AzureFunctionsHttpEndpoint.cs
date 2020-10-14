using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace SharpApi.AzureFunctions
{
    /// <summary>
    /// An endpoint that handles Azure Functions HTTP requests.
    /// </summary>
    public static class AzureFunctionsHttpEndpoint
    {
        /// <summary>
        /// Handles requests to the API.
        /// </summary>
        /// <param name="request">Azure Functions HTTP request.</param>
        /// <param name="logger">Azure Functions logger.</param>
        /// <returns>Azure Functions HTTP response.</returns>
        public static async Task<IActionResult> HandleAsync(HttpRequest request, ILogger logger)
        {
            var routeContext = new RouteContext(request.HttpContext);
            await ApiEndpointManager.Router.RouteAsync(routeContext);

            var handler = routeContext.Handler;

            if (handler == null)
            {
                return new NotFoundResult();
            }

            return new RequestDelegateResult(handler);
        }
    }
}
