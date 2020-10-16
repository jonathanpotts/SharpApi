using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace SharpApi.AzureFunctions
{
    /// <summary>
    /// Handles startup of an Azure Functions host.
    /// </summary>
    public class AzureFunctionsStartup : FunctionsStartup
    {
        /// <summary>
        /// Configures the Azure Functions host.
        /// </summary>
        /// <param name="builder">Azure Functions host builder.</param>
        public override void Configure(IFunctionsHostBuilder builder)
        {
            Startup.ConfigureServices(builder.Services);
        }
    }
}
