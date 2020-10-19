using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharpApi.Email.SendGrid;

namespace SharpApi.Example
{
    /// <summary>
    /// Implements API-specific startup.
    /// </summary>
    public class Startup : IApiStartup
    {
        public void ConfigureAppConfiguration(IConfigurationBuilder configurationBuilder)
        {
        }

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSendGridEmailSender(options =>
            {
                options.ApiKey = configuration["SendGridApiKey"];
            });
        }
    }
}
