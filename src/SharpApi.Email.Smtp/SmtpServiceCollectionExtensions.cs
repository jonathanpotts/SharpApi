using Microsoft.Extensions.DependencyInjection;

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
        public static void AddSmtpEmailSender(this IServiceCollection services)
        {
            services.AddSingleton<IEmailSender, SmtpEmailSender>();
        }
    }
}
