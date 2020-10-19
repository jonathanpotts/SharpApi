using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace SharpApi
{
    /// <summary>
    /// Handles startup and dependency injection.
    /// </summary>
    public static class Startup
    {
        public static void ConfigureAppConfiguration(IConfigurationBuilder configurationBuilder, string[] args = null)
        {
            var location = new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName;

            configurationBuilder.SetBasePath(location);

            configurationBuilder.AddJsonFile("appsettings.json", true);
            configurationBuilder.AddJsonFile($"appsettings.{ApiEnvironment.EnvironmentName}.json", true);

            configurationBuilder.AddEnvironmentVariables();

            if (args != null)
            {
                configurationBuilder.AddCommandLine(args);
            }

            if (ApiEnvironment.IsDevelopment())
            {
                var apiAssembly = ApiTypeManager.EndpointTypes.FirstOrDefault()?.Assembly;

                if (apiAssembly != null)
                {
                    configurationBuilder.AddUserSecrets(apiAssembly);
                }
            }

            var startupType = ApiTypeManager.StartupType;

            if (startupType != null)
            {
                var startup = (IApiStartup)Activator.CreateInstance(startupType);
                startup.ConfigureAppConfiguration(configurationBuilder);
            }
        }

        /// <summary>
        /// Configures the services used for dependency injection.
        /// </summary>
        /// <param name="services">Service collection used for dependency injection.</param>
        /// <param name="configuration">Configuration for the API.</param>
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            var startupType = ApiTypeManager.StartupType;

            if (startupType != null)
            {
                var startup = (IApiStartup)Activator.CreateInstance(startupType);
                startup.ConfigureServices(services, configuration);
            }

            services.AddSingleton<IRouter, Router>();

            foreach (var endpointType in ApiTypeManager.EndpointTypes ?? Enumerable.Empty<Type>())
            {
                services.AddTransient(endpointType);
            }
        }
    }
}
