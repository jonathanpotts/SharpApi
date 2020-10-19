using Microsoft.AspNetCore.Http;
using SharpApi.Email;
using System.Net.Mail;
using System.Threading.Tasks;

namespace SharpApi.Example
{
    [ApiEndpoint("/send-email", "POST")]
    public class SendEmailEndpoint : ApiEndpoint
    {
        private IEmailSender _emailSender;

        public SendEmailEndpoint(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public override async Task<ApiResult> RunAsync(ApiRequest request)
        {
            using var msg = new MailMessage();

            msg.From = new MailAddress("jonathanpotts@outlook.com", "Jonathan Potts");
            msg.To.Add(new MailAddress("jonathanpotts@outlook.com", "Jonathan Potts"));
            msg.Subject = "This is a test.";
            msg.Body = "Lorem ipsum dolor sit amet...";

            await _emailSender.SendAsync(msg);

            return new StatusCodeResult(StatusCodes.Status200OK);
        }
    }
}
