using Amazon;
using Amazon.SimpleEmail;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;

namespace SharpApi.Email.AmazonSimpleEmailService
{
    /// <summary>
    /// Sends emails via Amazon Simple Email Service.
    /// </summary>
    public class AmazonSimpleEmailServiceEmailSender : IEmailSender, IDisposable
    {
        /// <summary>
        /// Amazon Simple Email Service client.
        /// </summary>
        private readonly AmazonSimpleEmailServiceClient _client;

        /// <summary>
        /// Creates an instance of <see cref="AmazonSimpleEmailServiceEmailSender"/>.
        /// </summary>
        /// <param name="options">Amazon Simple Email Service configuration options.</param>
        public AmazonSimpleEmailServiceEmailSender(IOptions<AmazonSimpleEmailServiceOptions> options)
        {
            _client = new AmazonSimpleEmailServiceClient(
                options.Value.AwsAccessKeyId, 
                options.Value.AwsSecretAccessKey, 
                RegionEndpoint.GetBySystemName(options.Value.AwsRegionSystemName)
            );
        }

        /// <summary>
        /// Sends the provided email message.
        /// </summary>
        /// <param name="message">Email message to send.</param>
        /// <returns>Task representing the status of sending the email.</returns>
        public async Task SendAsync(MailMessage message)
        {
            var req = message.ToSendRawEmailRequest();

            var response = await _client.SendRawEmailAsync(req);

            if (response.HttpStatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                throw new HttpRequestException("The message was not accepted by Amazon Simple Email Service.");
            }
        }

        /// <summary>
        /// Disposes the Amazon Simple Email Service client.
        /// </summary>
        public void Dispose()
        {
            _client.Dispose();
        }
    }
}
