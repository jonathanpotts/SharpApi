using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace SharpApi.AwsLambda
{
    /// <summary>
    /// Handles startup of an AWS Lambda host.
    /// </summary>
    public class AwsLambdaProgram
    {
        /// <summary>
        /// Service provider for accessing dependency injection features.
        /// </summary>
        public static IServiceProvider ServiceProvider { get; private set; }

        /// <summary>
        /// Initializes the AWS Lambda host.
        /// </summary>
        public AwsLambdaProgram()
        {
            var configBuilder = new ConfigurationBuilder();
            Startup.ConfigureAppConfiguration(configBuilder);
            var configuration = (IConfiguration)configBuilder.Build();

            var services = new ServiceCollection();

            services.AddSingleton(configuration);

            services.AddLogging(configure =>
            {
                configure.AddConsole();
            });

            Startup.ConfigureServices(services, configuration);

            ServiceProvider = services.BuildServiceProvider();
        }
    }
}
