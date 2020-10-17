using Microsoft.Extensions.DependencyInjection;

namespace SharpApi.Email.Smtp
{
    public static class SmtpServiceCollectionExtensions
    {
        public static void AddSmtpEmailSender(this IServiceCollection services)
        {
            services.AddSingleton<IEmailSender, SmtpEmailSender>();
        }
    }
}
