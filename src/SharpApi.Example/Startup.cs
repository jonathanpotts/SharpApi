using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharpApi.Email.Smtp;

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
            services.AddSmtpEmailSender(options =>
            {
                options.Host = configuration.GetValue<string>("SmtpHost");
                options.Port = configuration.GetValue<int>("SmtpPort");
                options.Username = configuration.GetValue<string>("SmtpUsername");
                options.Password = configuration.GetValue<string>("SmtpPassword");
                options.Encryption = SmtpEncryption.StartTls;
            });
        }
    }
}
