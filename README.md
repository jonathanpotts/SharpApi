# SharpAPI

![.NET](https://github.com/jonathanpotts/SharpAPI/workflows/.NET/badge.svg)

SharpAPI is an API toolset for creating REST APIs that can be deployed serverless or hosted on cloud or on-premise infrastructure from a shared [.NET](https://dot.net/) codebase.

## Compatibility

|  | On-Premise, VM, or Platform Independent | [Microsoft Azure](https://azure.microsoft.com/) | [Amazon Web Services (AWS)](https://aws.amazon.com/) | [Google Cloud Platform (GCP)](https://cloud.google.com/) |
| :- | :-: | :-: | :-: | :-: |
| Compute | [ASP.NET Core](https://dotnet.microsoft.com/apps/aspnet/) 3.1 <br> [Docker](https://www.docker.com/) | [Azure Functions](https://azure.microsoft.com/services/functions/) <br> [App Service](https://azure.microsoft.com/services/app-service/) <br> [Container Instances](https://azure.microsoft.com/services/container-instances/) <br> [Virtual Machines](https://azure.microsoft.com/services/virtual-machines/) | [AWS Lambda](https://aws.amazon.com/lambda/) <br> [AWS Elastic Beanstalk](https://aws.amazon.com/elasticbeanstalk/) <br> [Amazon Elastic Container Service (ECS)](https://aws.amazon.com/ecs/) <br> [Amazon EC2](https://aws.amazon.com/ec2/) | [App Engine](https://cloud.google.com/appengine/) <br> [Cloud Run](https://cloud.google.com/run/) <br> [Compute Engine](https://cloud.google.com/compute/) |
| Database | [SQL Server](https://www.microsoft.com/sql-server/) 2012+ <br> [MySQL](https://www.mysql.com/) 5.6+ <br> [PostgreSQL](https://www.postgresql.org/) 9.5+ <br> [Oracle DB](https://www.oracle.com/database/) 11.2+ | [Azure SQL Database](https://azure.microsoft.com/services/sql-database/) <br> [Azure Database for MySQL](https://azure.microsoft.com/services/mysql/) <br> [Azure Database for PostgreSQL](https://azure.microsoft.com/services/postgresql/) <br>  | [Amazon Aurora](https://aws.amazon.com/rds/aurora/) <br> [Amazon RDS](https://aws.amazon.com/rds/) | [Cloud SQL](https://cloud.google.com/sql/) |
| Blobs | File System <br> Database | [Azure Blob Storage](https://azure.microsoft.com/en-us/services/storage/blobs/) | [Amazon S3](https://aws.amazon.com/s3/) | [Cloud Storage](https://cloud.google.com/storage/) |
| Email | SMTP <br> [SendGrid](https://sendgrid.com/) <br> [Mailjet](https://www.mailjet.com/) | [Microsoft 365 SMTP Relay](https://docs.microsoft.com/en-us/exchange/mail-flow-best-practices/how-to-set-up-a-multifunction-device-or-application-to-send-email-using-microsoft-365-or-office-365/) | [Amazon Simple Email Service (SES)](https://aws.amazon.com/ses/) | [Google Workspace SMTP Relay](https://support.google.com/a/answer/2956491/) |
| SMS | [Twilio SMS](https://www.twilio.com/sms/) | [Azure Communication Services](https://azure.microsoft.com/en-us/services/communication-services/) | [Amazon Simple Notification Service (SNS)](https://aws.amazon.com/sns/) |  |
| Secrets / Configuration | User Secrets <br> Environment Variables <br> JSON Files <br> XML Files <br> INI Files <br> Key Per File | [Key Vault](https://azure.microsoft.com/services/key-vault/) | [AWS Secrets Manager](https://aws.amazon.com/secrets-manager/) | [Secret Manager](https://cloud.google.com/secret-manager/) |
