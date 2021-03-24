# SharpAPI

![.NET](https://github.com/jonathanpotts/SharpAPI/workflows/.NET/badge.svg) ![DocFX](https://github.com/jonathanpotts/SharpAPI/workflows/DocFX/badge.svg)

SharpAPI is a collection of [dependency injection services](https://docs.microsoft.com/aspnet/core/fundamentals/dependency-injection/) for [ASP.NET Core](https://docs.microsoft.com/aspnet/core/) applications to consume commonly-used APIs and services from server software and public cloud providers.

## Docs

Docs are available at https://jonathanpotts.com/SharpAPI/.

## Compatibility

|  | Server Software and Service Providers | [Microsoft Azure](https://azure.microsoft.com/) | [Amazon Web Services (AWS)](https://aws.amazon.com/) | [Google Cloud Platform (GCP)](https://cloud.google.com/) |
| :- | :-: | :-: | :-: | :-: |
| NoSQL Database<sup>1</sup> | [MongoDB](https://www.mongodb.com/) | [Azure Cosmos DB](https://azure.microsoft.com/services/cosmos-db/) | [Amazon DynamoDB](https://aws.amazon.com/dynamodb/) | [Cloud Bigtable](https://cloud.google.com/bigtable/) |
| Blobs | File System <br> Database<sup>1</sup> | [Azure Blob Storage](https://azure.microsoft.com/en-us/services/storage/blobs/) | [Amazon S3](https://aws.amazon.com/s3/)<sup>1</sup> | [Cloud Storage](https://cloud.google.com/storage/)<sup>1</sup> |
| Email | SMTP <br> [SendGrid](https://sendgrid.com/) |  | [Amazon Simple Email Service (SES)](https://aws.amazon.com/ses/) |  |
| SMS<sup>1</sup> | [Twilio SMS](https://www.twilio.com/sms/) | [Azure Communication Services](https://azure.microsoft.com/en-us/services/communication-services/) | [Amazon Simple Notification Service (SNS)](https://aws.amazon.com/sns/)  |  |
| Secrets / Configuration |  |  | [AWS Secrets Manager](https://aws.amazon.com/secrets-manager/) | [Secret Manager](https://cloud.google.com/secret-manager/) |

<sup>1</sup> TODO
