using System;
using System.Net.Mail;
using System.Threading.Tasks;

namespace SharpApi.Email.SendGrid
{
    /// <summary>
    /// Sends emails via SendGrid.
    /// </summary>
    public class SendGridEmailSender : IEmailSender
    {
        public Task SendAsync(MailMessage message)
        {
            throw new NotImplementedException();
        }
    }
}
