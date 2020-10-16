using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace SharpApi
{
    /// <summary>
    /// Handles startup and dependency injection.
    /// </summary>
    public static class Startup
    {
        /// <summary>
        /// Configures the services used for dependency injection.
        /// </summary>
        /// <param name="services">Service collection used for dependency injection.</param>
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddSharpApi();
            services.AddHttpClient();
        }

        /// <summary>
        /// Adds SharpAPI services to the service collection.
        /// </summary>
        /// <param name="services">Service collection used for dependency injection.</param>
        private static void AddSharpApi(this IServiceCollection services)
        {
            services.AddSingleton<IRouter, Router>();

            foreach (var endpointType in ApiEndpointManager.EndpointTypes ?? Enumerable.Empty<Type>())
            {
                services.AddTransient(endpointType);
            }
        }
    }
}
