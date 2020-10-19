using Microsoft.Extensions.Configuration;

namespace SharpApi
{
    public abstract class ApiStartup
    {
        /// <summary>
        /// Configures the app configuration. Use <see cref="IConfigurationBuilder.Build"/> to get an <see cref="IConfiguration"/> if needed.
        /// </summary>
        /// <param name="configurationBuilder">Configuration builder to add app configuration to.</param>
        public abstract void ConfigureAppConfiguration(IConfigurationBuilder configurationBuilder);
    }
}
