using System.Net.Mail;
using System.Threading.Tasks;

namespace SharpApi.Email
{
    /// <summary>
    /// Interface used to implement email sender services.
    /// </summary>
    public interface IEmailSender
    {
        /// <summary>
        /// Sends the provided email message.
        /// </summary>
        /// <param name="message">Email message to send.</param>
        /// <returns>Task representing the status of sending the email.</returns>
        public Task SendAsync(MailMessage message);
    }
}
