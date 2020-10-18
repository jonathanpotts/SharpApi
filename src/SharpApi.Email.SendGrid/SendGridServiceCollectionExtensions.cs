using Microsoft.Extensions.DependencyInjection;

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
        public static void AddSendGridEmailSender(this IServiceCollection services)
        {
            services.AddSingleton<IEmailSender, SendGridEmailSender>();
        }
    }
}
