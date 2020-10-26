using Microsoft.Extensions.DependencyInjection;
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
        public static void AddAmazonSimpleEmailServiceEmailSender(this IServiceCollection services, Action<AmazonSimpleEmailServiceOptions> options)
        {
            services.AddOptions<AmazonSimpleEmailServiceOptions>().Configure(options);

            services.AddSingleton<IEmailSender, AmazonSimpleEmailServiceEmailSender>();
        }
    }
}
