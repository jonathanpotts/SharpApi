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
            var services = new ServiceCollection();
            services.AddLogging(configure =>
            {
                configure.AddConsole();
            });

            Startup.ConfigureServices(services);

            ServiceProvider = services.BuildServiceProvider();
        }
    }
}
