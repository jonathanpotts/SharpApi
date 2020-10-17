using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Net.Mail;
using System.Threading.Tasks;

using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace SharpApi.Email.Smtp
{
    /// <summary>
    /// Sends emails via SMTP.
    /// </summary>
    public class SmtpEmailSender : IEmailSender, IDisposable
    {
        /// <summary>
        /// SMTP client.
        /// </summary>
        private readonly ISmtpClient _smtpClient;

        /// <summary>
        /// Initializes the SMTP email sender service.
        /// </summary>
        public SmtpEmailSender()
        {
            var smtpClient = new SmtpClient();

            _smtpClient = smtpClient;
        }

        public async Task SendAsync(MailMessage message)
        {
            var mimeMessage = (MimeMessage)message;

            await _smtpClient.SendAsync(mimeMessage);
        }

        public void Dispose()
        {
            if (_smtpClient?.IsConnected ?? false)
            {
                _smtpClient.Disconnect(true);
            }

            _smtpClient?.Dispose();
        }
    }
}
