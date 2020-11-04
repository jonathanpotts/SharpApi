using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.ComponentModel.Composition.Hosting;
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
        /// Assembly containing the API parts.
        /// </summary>
        private static Assembly s_apiAssembly;
        
        /// <summary>
        /// Assembly containing the API parts.
        /// </summary>
        private static Assembly ApiAssembly
        {
            get
            {
                var location = new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName;
                new DirectoryCatalog(location);

                if (s_apiAssembly != null)
                {
                    return s_apiAssembly;
                }

                 s_apiAssembly = AppDomain
                    .CurrentDomain
                    .GetAssemblies()
                    .Where(a => !a.IsDynamic && new FileInfo(a.Location).DirectoryName == location)
                    .SelectMany(a => a.GetTypes())
                    .Where(t => t.GetCustomAttribute(typeof(ApiControllerAttribute)) != null)
                    .FirstOrDefault()?
                    .Assembly;

                return s_apiAssembly;
            }
        }

        /// <summary>
        /// Configures the app configuration used by services.
        /// </summary>
        /// <param name="configurationBuilder"><see cref="IConfigurationBuilder"/> used to configure the app configuration.</param>
        /// <param name="args">Command line arguments used to configure the app configuration.</param>
        public static void ConfigureAppConfiguration(IConfigurationBuilder configurationBuilder, string[] args = null)
        {
            if (ApiEnvironment.IsDevelopment())
            {
                if (ApiAssembly != null)
                {
                    configurationBuilder.AddUserSecrets(ApiAssembly);
                }
            }
        }

        /// <summary>
        /// Configures the services used for dependency injection.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/> used for dependency injection.</param>
        /// <param name="configuration"><see cref="IConfiguration"/> containing the app configuration.</param>
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
#if NETSTANDARD2_0
            services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddApplicationPart(ApiAssembly);
#else
            services
                .AddControllers()
                .AddApplicationPart(ApiAssembly);
#endif
        }

        public static void Configure(IApplicationBuilder app)
        {
#if NETSTANDARD2_0
            app.UseMvc();
#else
            app.UseRouting();

            app.UseEndpoints(configure =>
            {
                configure.MapControllers();
            });
#endif
        }
    }
}
