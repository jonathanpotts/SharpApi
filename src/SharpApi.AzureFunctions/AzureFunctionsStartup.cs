using Microsoft.Azure.Functions.Extensions.DependencyInjection;

namespace SharpApi.AzureFunctions
{
    /// <summary>
    /// Handles startup of an Azure Functions host.
    /// </summary>
    public class AzureFunctionsStartup : FunctionsStartup
    {
        /// <summary>
        /// Configures the app configuration of the Azure Functions host.
        /// </summary>
        /// <param name="builder">Azure Functions configuration builder.</param>
        public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
        {
            base.ConfigureAppConfiguration(builder);

            Startup.ConfigureAppConfiguration(builder.ConfigurationBuilder);
        }

        /// <summary>
        /// Configures the Azure Functions host.
        /// </summary>
        /// <param name="builder">Azure Functions host builder.</param>
        public override void Configure(IFunctionsHostBuilder builder)
        {
            Startup.ConfigureServices(builder.Services, builder.GetContext().Configuration);
        }
    }
}
