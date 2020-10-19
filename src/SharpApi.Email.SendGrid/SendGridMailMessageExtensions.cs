using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;

namespace SharpApi.Email.SendGrid
{
    /// <summary>
    /// SendGrid extensions for <see cref="MailMessage"/>.
    /// </summary>
    public static class SendGridMailMessageExtensions
    {
        /// <summary>
        /// Creates a <see cref="SendGridMessage"/> from a <see cref="MailMessage"/>.
        /// </summary>
        /// <param name="message"><see cref="MailMessage"/> to create a <see cref="SendGridMessage"/> for.</param>
        /// <returns><see cref="SendGridMessage"/> based on provided <see cref="MailMessage"/>.</returns>
        public static SendGridMessage ToSendGridMessage(this MailMessage message)
        {
            var msg = new SendGridMessage();

            if (message.From != null)
            {
                msg.SetFrom(message.From.Address, message.From.DisplayName);
            }

            var replyTo = message.ReplyToList?.FirstOrDefault();

            if (replyTo != null)
            {
                msg.SetReplyTo(new EmailAddress(replyTo.Address, replyTo.DisplayName));
            }

            foreach (var to in message.To ?? Enumerable.Empty<MailAddress>())
            {
                msg.AddTo(to.Address, to.DisplayName);
            }

            foreach (var cc in message.CC ?? Enumerable.Empty<MailAddress>())
            {
                msg.AddCc(cc.Address, cc.DisplayName);
            }

            foreach (var bcc in message.Bcc ?? Enumerable.Empty<MailAddress>())
            {
                msg.AddBcc(bcc.Address, bcc.DisplayName);
            }

            msg.SetSubject(message.Subject);

            foreach (var key in message.Headers.AllKeys ?? Enumerable.Empty<string>())
            {
                var values = message.Headers.GetValues(key);

                foreach (var value in values ?? Enumerable.Empty<string>())
                {
                    msg.AddHeader(key, value);
                }
            }

            if (message.Priority == MailPriority.High)
            {
                msg.AddHeader("X-Priority", "1");
                msg.AddHeader("Importance", "high");
            }
            else if (message.Priority == MailPriority.Low)
            {
                msg.AddHeader("X-Priority", "5");
                msg.AddHeader("Importance", "low");
            }

            foreach (var attachment in message.Attachments ?? Enumerable.Empty<System.Net.Mail.Attachment>())
            {
                using var ms = new MemoryStream();
                attachment.ContentStream.CopyTo(ms);
                var base64 = Convert.ToBase64String(ms.ToArray());

                msg.AddAttachment(attachment.ContentDisposition.FileName, base64, attachment.ContentType.MediaType, attachment.ContentDisposition.DispositionType, attachment.ContentId);
            }

            var content = new Dictionary<string, string>()
            {
                { message.IsBodyHtml ? "text/html" : "text/plain", message.Body }
            };

            foreach (var alternateView in message.AlternateViews ?? Enumerable.Empty<AlternateView>())
            {
                using var sr = new StreamReader(alternateView.ContentStream);
                content.Add(alternateView.ContentType.MediaType, sr.ReadToEnd());
            }

            if (content.TryGetValue("text/plain", out var text))
            {
                msg.AddContent("text/plain", text);
                content.Remove("text/plain");
            }

            if (content.TryGetValue("text/html", out var html))
            {
                msg.AddContent("text/html", html);
                content.Remove("text/html");
            }

            foreach (var contentData in content)
            {
                msg.AddContent(contentData.Key, contentData.Value);
            }

            return msg;
        }
    }
}
