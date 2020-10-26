using Microsoft.Extensions.DependencyInjection;
using System;

namespace SharpApi.Email.SendGrid
{
    /// <summary>
    /// Extensions for <see cref="IServiceCollection"/>.
    /// </summary>
    public static class SendGridServiceCollectionExtensions
    {
        /// <summary>
        /// Adds <see cref="SendGridEmailSender"/> to the service collection.
        /// </summary>
        /// <param name="services">Service collection.</param>
        /// <param name="options">Action to configure SendGrid options.</param>
        public static void AddSendGridEmailSender(this IServiceCollection services, Action<SendGridOptions> options)
        {
            services.AddOptions<SendGridOptions>().Configure(options);

            services.AddHttpClient<IEmailSender, SendGridEmailSender>();
        }
    }
}
