using Microsoft.Extensions.DependencyInjection;
using SharpApi.Aws;
using System;

namespace SharpApi.Email.AmazonSimpleEmailService
{
    /// <summary>
    /// Extensions for <see cref="IServiceCollection"/>.
    /// </summary>
    public static class AmazonSimpleEmailServiceServiceCollectionExtensions
    {
        /// <summary>
        /// Adds <see cref="AmazonSimpleEmailServiceEmailSender"/> to the service collection.
        /// </summary>
        /// <param name="services">Service collection.</param>
        /// <param name="options">Action to configure Amazon Simple Email Service options.</param>
        public static void AddAmazonSimpleEmailServiceEmailSender(this IServiceCollection services, Action<AwsOptions> options)
        {
            services.AddOptions<AwsOptions>().Configure(options);

            services.AddSingleton<IEmailSender, AmazonSimpleEmailServiceEmailSender>();
        }
    }
}
