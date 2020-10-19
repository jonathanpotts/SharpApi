using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace SharpApi.AspNetCore
{
    public static class SharpApiHost
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">Type within the assembly to search for the <see cref="UserSecretsIdAttribute"/>.</typeparam>
        /// <param name="args"></param>
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
