using Google.Cloud.Functions.Hosting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SharpApi.GoogleCloudFunctions
{
    /// <summary>
    /// Handles startup of a Google Cloud Functions host.
    /// </summary>
    public class GoogleCloudFunctionsStartup : FunctionsStartup
    {
        /// <summary>
        /// Configures the app configuration of the Google Cloud Functions host.
        /// </summary>
        /// <param name="context">Google Cloud Functions host context.</param>
        /// <param name="configuration">Google Cloud Functions configuration builder.</param>
        public override void ConfigureAppConfiguration(WebHostBuilderContext context, IConfigurationBuilder configuration)
        {
            base.ConfigureAppConfiguration(context, configuration);
        }

        /// <summary>
        /// Configures dependency injection for the Google Cloud Functions host.
        /// </summary>
        /// <param name="context">Google Cloud Functions host context.</param>
        /// <param name="services">Google Cloud Functions service collection.</param>
        public override void ConfigureServices(WebHostBuilderContext context, IServiceCollection services)
        {
        }
    }
}
