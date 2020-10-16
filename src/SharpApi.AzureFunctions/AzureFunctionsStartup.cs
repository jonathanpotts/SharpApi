using Microsoft.Azure.Functions.Extensions.DependencyInjection;

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
            // TODO: Wire up Azure Functions logging

            Startup.ConfigureServices(builder.Services);
        }
    }
}
