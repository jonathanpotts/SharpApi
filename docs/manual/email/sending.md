# Sending emails

SharpAPI supports sending emails via SMTP or [SendGrid](https://sendgrid.com/).

## Configuring SMTP

Add the [`SharpApi.Email.Smtp`](~/obj/api/SharpApi.Email.Smtp.yml) package to your API project.

Then add the service to your API by adding the following to your API's [`ConfigureServices(IServiceCollection, IConfiguration)`](~/obj/api/SharpApi.IApiStartup.yml#SharpApi_IApiStartup_ConfigureServices_Microsoft_Extensions_DependencyInjection_IServiceCollection_Microsoft_Extensions_Configuration_IConfiguration_) method:

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

For more information about adding services to your API, see [Dependency Injection (Services)](~/manual/fundamentals/dependency-injection-services.md).

## Configuring SendGrid

Add the [`SharpApi.Email.SendGrid`](~/obj/api/SharpApi.Email.SendGrid.yml) package to your API project. 

Then add the service to your API by adding the following to your API's [`ConfigureServices(IServiceCollection, IConfiguration)`](~/obj/api/SharpApi.IApiStartup.yml#SharpApi_IApiStartup_ConfigureServices_Microsoft_Extensions_DependencyInjection_IServiceCollection_Microsoft_Extensions_Configuration_IConfiguration_) method:

```cs
services.AddSendGridEmailSender(options =>
{
    // Use your configuration here
    options.ApiKey = configuration.GetValue<string>("SendGridApiKey");
});
```

For more information about adding services to your API, see [Dependency Injection (Services)](~/manual/fundamentals/dependency-injection-services.md).

## Sending an email

Use dependency injection to add an [`IEmailSender`](~/obj/api/SharpApi.Email.IEmailSender.yml) to your API endpoint and use it to send the email.

**Example:**

```cs
using Microsoft.AspNetCore.Http;
using SharpApi;
using SharpApi.Email;
using System.Net.Mail;
using System.Threading.Tasks;

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
```
