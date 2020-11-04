using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SharpApi
{
    /// <summary>
    /// Handles startup for the ASP.NET Core pipeline.
    /// </summary>
    public class AspNetCoreStartup
    {
        /// <summary>
        /// Configuration.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Creates an instance of <see cref="AspNetCoreStartup"/> with dependencies injected.
        /// </summary>
        /// <param name="configuration">Configuration.</param>
        public AspNetCoreStartup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Configures services used by the ASP.NET Core pipeline.
        /// </summary>
        /// <param name="services">Service collection.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            Startup.ConfigureServices(services, Configuration);
        }

        /// <summary>
        /// Configures the ASP.NET Core pipeline.
        /// </summary>
        /// <param name="app">Builder for the application pipeline.</param>
        /// <param name="env">Hosting environment.</param>
#if NETSTANDARD2_0
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            Startup.Configure(app);
        }
#else
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            Startup.Configure(app);
        }
#endif
    }
}
