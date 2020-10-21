# Sending emails with SharpAPI

SharpAPI supports sending emails via SMTP or [SendGrid](https://sendgrid.com/).

## Configuring SMTP

Add the `SharpApi.Email.Smtp` package to your API project. Then configure the service in your API project's `Startup.cs` by adding the following to your `ConfigureServices` method:

```cs
services.AddSmtpEmailSender(options =>
{
    // Use your configuration here
    options.Host = configuration.GetValue<string>("SmtpHost");
    options.Port = configuration.GetValue<int>("SmtpPort");
    options.Username = configuration.GetValue<string>("SmtpUsername");
    options.Password = configuration.GetValue<string>("SmtpPassword");
    options.Encryption = configuration.GetValue<SmtpEncryption>("SmtpEncryption");
});
```

## Configuring SendGrid

Add the `SharpApi.Email.SendGrid` package to your API project. Then configure the service in your API project's `Startup.cs` by adding the following to your `ConfigureServices` method:

```cs
services.AddSendGridEmailSender(options =>
{
    // Use your configuration here
    options.ApiKey = configuration.GetValue<string>("SendGridApiKey");
});
```

## Sending an email

Use dependency injection to add an `IEmailSender` to your API endpoint and use it to send the email.

Example:

```cs
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

            msg.From = new MailAddress("xyzcorp@example.com", "XYZ Corporation");
            msg.To.Add(new MailAddress("janedoe@example.com", "Jane Doe"));
            msg.Subject = "Your order has shipped!";
            msg.Body = "Lorem ipsum dolor sit amet...";

            await _emailSender.SendAsync(msg);

            return new StatusCodeResult(StatusCodes.Status200OK);
        }
    }
}
```
