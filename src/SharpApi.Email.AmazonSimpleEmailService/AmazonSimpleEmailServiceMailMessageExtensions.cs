using Amazon.SimpleEmail.Model;
using MimeKit;
using System.IO;
using System.Net.Mail;

namespace SharpApi.Email.AmazonSimpleEmailService
{
    /// <summary>
    /// Amazon Simple Email Service extensions for <see cref="MailMessage"/>.
    /// </summary>
    public static class AmazonSimpleEmailServiceMailMessageExtensions
    {
        /// <summary>
        /// Creates a <see cref="SendRawEmailRequest"/> from a <see cref="MailMessage"/>.
        /// </summary>
        /// <param name="message"><see cref="MailMessage"/> to create a <see cref="SendRawEmailRequest"/> for.</param>
        /// <returns><see cref="SendRawEmailRequest"/> based on provided <see cref="MailMessage"/>.</returns>
        public static SendRawEmailRequest ToSendRawEmailRequest(this MailMessage message)
        {
            var msg = (MimeMessage)message;

            using (var ms = new MemoryStream())
            {
                msg.WriteTo(ms);

                var rawMsg = new RawMessage(ms);
                var req = new SendRawEmailRequest(rawMsg);

                return req;
            }
        }
    }
}
