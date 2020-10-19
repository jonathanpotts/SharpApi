using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
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
        private readonly IOptions<SendGridOptions> _options;
        private readonly HttpClient _httpClient;

        public SendGridEmailSender(IOptions<SendGridOptions> options, HttpClient httpClient)
        {
            _options = options;
            _httpClient = httpClient;
        }

        public Task SendAsync(MailMessage message)
        {
            var client = new SendGridClient(_httpClient, _options.Value.ApiKey);

            return Task.CompletedTask;
        }
    }
}
