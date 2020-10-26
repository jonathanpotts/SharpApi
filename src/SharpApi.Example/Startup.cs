using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharpApi.Email.AmazonSimpleEmailService;

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
            services.AddAmazonSimpleEmailServiceEmailSender(options =>
            {
                options.AwsAccessKeyId = configuration.GetValue<string>("AwsAccessKeyId");
                options.AwsSecretAccessKey = configuration.GetValue<string>("AwsSecretAccessKey");
                options.AwsRegionSystemName = configuration.GetValue<string>("AwsRegionSystemName");
            });
        }
    }
}
