using SharpApi.Email;
using System;
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

        public override Task<ApiResult> RunAsync(ApiRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
