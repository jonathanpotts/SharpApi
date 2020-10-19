using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SharpApi
{
    /// <summary>
    /// Interface used to implement API-specific startup.
    /// </summary>
    public interface IApiStartup
    {
        /// <summary>
        /// Configures the app configuration. Use <see cref="IConfigurationBuilder.Build"/> to get an <see cref="IConfiguration"/> if needed.
        /// </summary>
        /// <param name="configurationBuilder"><see cref="IConfigurationBuilder"/> to add app configuration to.</param>
        public void ConfigureAppConfiguration(IConfigurationBuilder configurationBuilder);

        /// <summary>
        /// Configures services used by the API.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/> to add services to.</param>
        /// <param name="configuration"><see cref="IConfiguration"/> containing the app configuration.</param>
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration);
    }
}
