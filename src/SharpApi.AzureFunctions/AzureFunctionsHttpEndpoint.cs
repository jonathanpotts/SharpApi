using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SharpApi.AspNetCore;
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
            var result = await AspNetCoreEndpoint.ProcessRequest(request.HttpContext);

            if (result == null)
            {
                return new SharpApiResult(new StatusCodeResult(StatusCodes.Status404NotFound));
            }

            return new SharpApiResult(result);
        }
    }
}
