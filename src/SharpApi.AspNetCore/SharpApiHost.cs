using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace SharpApi.AspNetCore
{
    /// <summary>
    /// Utility methods used for creating an ASP.NET Core host for use with SharpAPI.
    /// </summary>
    public static class SharpApiHost
    {
        /// <summary>
        /// Creates a <see cref="IHostBuilder"/> used for building a host to use with SharpAPI.
        /// </summary>
        /// <typeparam name="T">Type within the assembly to search for the <see cref="UserSecretsIdAttribute"/>.</typeparam>
        /// <param name="args">Command line arguments to add to the app configuration.</param>
        /// <returns></returns>
        public static IHostBuilder CreateBuilder(string[] args)
        {
            var builder = new HostBuilder();
            builder.ConfigureAppConfiguration((context, configurationBuilder) =>
            {
                Startup.ConfigureAppConfiguration(configurationBuilder, args);
            });

            builder.ConfigureLogging((context, loggingBuilder) =>
            {
                loggingBuilder.AddConsole();
                loggingBuilder.AddDebug();
                loggingBuilder.AddEventSourceLogger();
            });

            builder.ConfigureServices((context, services) =>
            {
                Startup.ConfigureServices(services, context.Configuration);
            });

            return builder;
        }
    }
}
