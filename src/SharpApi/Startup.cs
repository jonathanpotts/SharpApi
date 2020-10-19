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
        /// <summary>
        /// Determines if the API-specific startup implementation has been initialized.
        /// </summary>
        private static bool s_apiStartupInitialized;

        /// <summary>
        /// API-specific startup implementation.
        /// </summary>
        private static IApiStartup s_apiStartup;

        /// <summary>
        /// API-specific startup implementation.
        /// </summary>
        /// <exception cref="MissingMemberException">Thrown when the implementation of <see cref="IApiStartup"/> does not contain a default constructor.</exception>
        private static IApiStartup ApiStartup
        {
            get
            {
                if (s_apiStartupInitialized)
                {
                    return s_apiStartup;
                }

                var apiStartupType = ApiTypeManager.StartupType;

                if (apiStartupType != null)
                {
                    try
                    {
                        s_apiStartup = (IApiStartup)Activator.CreateInstance(apiStartupType);
                    }
                    catch (MissingMemberException ex)
                    {
                        throw new MissingMemberException($"The implementation of {nameof(IApiStartup)} must contain a default constructor.", ex);
                    }
                }

                s_apiStartupInitialized = true;

                return s_apiStartup;
            }
        }

        /// <summary>
        /// Configures the app configuration used by services.
        /// </summary>
        /// <param name="configurationBuilder"><see cref="IConfigurationBuilder"/> used to configure the app configuration.</param>
        /// <param name="args">Command line arguments used to configure the app configuration.</param>
        public static void ConfigureAppConfiguration(IConfigurationBuilder configurationBuilder, string[] args = null)
        {
            var location = new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName;

            configurationBuilder.SetBasePath(location);

            configurationBuilder.AddJsonFile("appsettings.json", true);
            configurationBuilder.AddJsonFile($"appsettings.{ApiEnvironment.EnvironmentName}.json", true);
            configurationBuilder.AddJsonFile("local.settings.json", true);

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

            ApiStartup?.ConfigureAppConfiguration(configurationBuilder);
        }

        /// <summary>
        /// Configures the services used for dependency injection.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/> used for dependency injection.</param>
        /// <param name="configuration"><see cref="IConfiguration"/> containing the app configuration.</param>
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            ApiStartup?.ConfigureServices(services, configuration);

            services.AddSingleton<IRouter, Router>();

            foreach (var endpointType in ApiTypeManager.EndpointTypes ?? Enumerable.Empty<Type>())
            {
                services.AddTransient(endpointType);
            }
        }
    }
}
