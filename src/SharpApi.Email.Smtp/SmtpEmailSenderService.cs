using MimeKit;
using System.Net.Mail;
using System.Threading.Tasks;

using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace SharpApi.Email.Smtp
{
    /// <summary>
    /// Sends emails via SMTP.
    /// </summary>
    public class SmtpEmailSenderService : IEmailSenderService
    {
        public async Task SendAsync(MailMessage message)
        {
            var mimeMessage = (MimeMessage)message;

            using var smtpClient = new SmtpClient();
            await smtpClient.SendAsync(mimeMessage);
            await smtpClient.DisconnectAsync(true);
        }
    }
}
