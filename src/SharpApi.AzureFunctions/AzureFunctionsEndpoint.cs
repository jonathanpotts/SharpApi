using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharpApi.AzureFunctions
{
    /// <summary>
    /// An endpoint that handles Azure Functions HTTP requests.
    /// </summary>
    public static class AzureFunctionsEndpoint
    {
        /// <summary>
        /// Handles requests to the API.
        /// </summary>
        /// <param name="request">Azure Functions HTTP request.</param>
        /// <param name="logger">Azure Functions logger.</param>
        /// <returns>Azure Functions HTTP response.</returns>
        public static async Task<IActionResult> HandleAsync(HttpRequest request, ILogger logger)
        {
            var path = request.Path.ToString().Replace("/api", "");
            var endpoint = ApiEndpointManager.GetApiEndpoint(request.Method, path);

            if (endpoint == null)
            {
                return new NotFoundResult();
            }

            var headers = request.Headers.ToDictionary(h => h.Key, h => (IList<string>)h.Value.ToList());
            var query = request.Query.ToDictionary(q => q.Key, q => (IList<string>)q.Value.ToList());

            using var apiRequest = new ApiRequest(headers, query, request.Body);

            return new SharpApiResult(await endpoint.RunAsync(apiRequest));
        }
    }
}
