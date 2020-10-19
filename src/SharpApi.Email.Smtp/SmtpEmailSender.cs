using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
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
        /// SMTP configuration options.
        /// </summary>
        private readonly IOptions<SmtpOptions> _options;

        /// <summary>
        /// SMTP client.
        /// </summary>
        private readonly ISmtpClient _smtpClient;

        /// <summary>
        /// Creates an instance of <see cref="SmtpEmailSender"/>.
        /// </summary>
        /// <param name="options">SMTP configuration options.</param>
        public SmtpEmailSender(IOptions<SmtpOptions> options)
        {
            _options = options;

            var smtpClient = new SmtpClient();

            _smtpClient = smtpClient;
        }

        public async Task SendAsync(MailMessage message)
        {
            var mimeMessage = (MimeMessage)message;

            if (!_smtpClient.IsConnected)
            {
                var secureSocketOptions = _options.Value.Encryption switch
                {
                    SmtpEncryption.None => SecureSocketOptions.None,
                    SmtpEncryption.SslTls => SecureSocketOptions.SslOnConnect,
                    SmtpEncryption.StartTls => SecureSocketOptions.StartTls,
                    _ => SecureSocketOptions.Auto
                };

                await _smtpClient.ConnectAsync(_options.Value.Host, _options.Value.Port, secureSocketOptions);
            }

            if (!_smtpClient.IsAuthenticated && 
                (!string.IsNullOrEmpty(_options.Value.Username) || !string.IsNullOrEmpty(_options.Value.Password)))
            {
                await _smtpClient.AuthenticateAsync(_options.Value.Username, _options.Value.Password);
            }
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
