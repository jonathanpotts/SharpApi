# Sending emails

SharpAPI supports sending emails via SMTP, [SendGrid](https://sendgrid.com/), or [Amazon Simple Email Service](https://aws.amazon.com/ses/).

## Configuring SMTP

For information on configuration details for SMTP relays:

* **Microsoft 365:** [How to set up a multifunction device or application to send email using Microsoft 365 or Office 365](https://docs.microsoft.com/exchange/mail-flow-best-practices/how-to-set-up-a-multifunction-device-or-application-to-send-email-using-microsoft-365-or-office-365)
* **Google Workspace:** [SMTP relay: Route outgoing non-Gmail messages through Google](https://support.google.com/a/answer/2956491/)

Add the @SharpApi.Email.Smtp package to your project.

Then register the service to your application by adding the following to your `Startup.Configure` method:

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

For information on setting up SendGrid:

* [Getting Started with the SendGrid API](https://sendgrid.com/docs/for-developers/sending-email/api-getting-started/)

Add the @SharpApi.Email.SendGrid package to your project.

Then register the service to your application by adding the following to your `Startup.Configure` method:

```cs
services.AddSendGridEmailSender(options =>
{
    // Use your configuration here
    options.ApiKey = configuration.GetValue<string>("SendGridApiKey");
});
```

## Configuring Amazon Simple Email Service

For information on setting up Amazon Simple Email Service:

* [Amazon SES Quick start](https://docs.aws.amazon.com/ses/latest/DeveloperGuide/quick-start.html)

Add the @SharpApi.Email.AmazonSimpleEmailService package to your project.

Then register the service to your application by adding the following to your  `Startup.Configure` method:

```cs
services.AddAmazonSimpleEmailServiceEmailSender(options =>
{
    // Use your configuration here
    options.Profile = configuration.GetValue<string>("AwsProfile");
    options.AwsRegion = configuration.GetValue<string>("AwsRegion");
});
```

## Sending an email

Use dependency injection to request an @SharpApi.Email.IEmailSender in your controller and use it to send the email.

**Example:**

```cs
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharpApi;
using SharpApi.Email;
using System.Net.Mail;
using System.Threading.Tasks;

public class SendEmailController : Controller
{
    private IEmailSender _emailSender;

    public SendEmailController(IEmailSender emailSender)
    {
        _emailSender = emailSender;
    }

    public async Task<IActionResult> PostAsync()
    {
        using var msg = new MailMessage();

        msg.From = new MailAddress("xyzcorp@example.com", "XYZ Corporation");
        msg.To.Add(new MailAddress("janedoe@example.com", "Jane Doe"));
        msg.Subject = "Your order has shipped!";
        msg.Body = "Lorem ipsum dolor sit amet...";

        await _emailSender.SendAsync(msg);

        return Ok();
    }
}
```
