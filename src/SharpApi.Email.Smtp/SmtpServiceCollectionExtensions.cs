using Microsoft.Extensions.DependencyInjection;
using System;

namespace SharpApi.Email.Smtp
{
    /// <summary>
    /// Extensions for <see cref="IServiceCollection"/>.
    /// </summary>
    public static class SmtpServiceCollectionExtensions
    {
        /// <summary>
        /// Adds <see cref="SmtpEmailSender"/> to the service collection.
        /// </summary>
        /// <param name="services">Service collection.</param>
        public static void AddSmtpEmailSender(this IServiceCollection services, Action<SmtpOptions> options)
        {
            services.AddOptions<SmtpOptions>().Configure(options);

            services.AddSingleton<IEmailSender, SmtpEmailSender>();
        }
    }
}
