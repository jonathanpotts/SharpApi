using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using SharpApi.AzureFunctions;
using System.Threading.Tasks;

namespace SharpApi.Example.AzureFunctions
{
    /// <summary>
    /// The program used for handling Azure Functions HTTP requests.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// An Azure Functions function that calls the SharpAPI Azure Functions HTTP function handler.
        /// </summary>
        /// <param name="request">Azure Functions HTTP request.</param>
        /// <param name="logger">Azure Functions logger.</param>
        /// <returns>Azure Functions HTTP response.</returns>
        [FunctionName("Program")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, Route = "{**path}")] HttpRequest request,
            ILogger logger)
        {
            return await AzureFunctionsEndpoint.HandleAsync(request, logger);
        }
    }
}
