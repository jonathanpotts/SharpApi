using Microsoft.Extensions.DependencyInjection;

namespace SharpApi.Email
{
    /// <summary>
    /// Extensions for <see cref="IServiceCollection"/>.
    /// </summary>
    public static class NullEmailServiceCollectionExtensions
    {
        /// <summary>
        /// Adds <see cref="NullEmailSender"/> to the service collection.
        /// </summary>
        /// <param name="services">Service collection.</param>
        public static void AddNullEmailSender(this IServiceCollection services)
        {
            services.AddSingleton<IEmailSender, NullEmailSender>();
        }
    }
}
