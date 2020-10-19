using Microsoft.Extensions.Options;
using SendGrid;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;

namespace SharpApi.Email.SendGrid
{
    /// <summary>
    /// Sends emails via SendGrid.
    /// </summary>
    public class SendGridEmailSender : IEmailSender
    {
        /// <summary>
        /// SendGrid configuration options.
        /// </summary>
        private readonly IOptions<SendGridOptions> _options;

        /// <summary>
        /// HTTP client to access SendGrid with.
        /// </summary>
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Creates an instance of <see cref="SendGridEmailSender"/>.
        /// </summary>
        /// <param name="options">SendGrid configuration options.</param>
        /// <param name="httpClient">HTTP client to access SendGrid with.</param>
        public SendGridEmailSender(IOptions<SendGridOptions> options, HttpClient httpClient)
        {
            _options = options;
            _httpClient = httpClient;
        }

        public async Task SendAsync(MailMessage message)
        {
            var client = new SendGridClient(_httpClient, _options.Value.ApiKey);
            var msg = message.ToSendGridMessage();

            var response = await client.SendEmailAsync(msg);

            if (response.StatusCode != System.Net.HttpStatusCode.Accepted)
            {
                throw new HttpRequestException("The message was not accepted by SendGrid.");
            }
        }
    }
}
