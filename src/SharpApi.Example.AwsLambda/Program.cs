using Amazon.Lambda.AspNetCoreServer;
using Microsoft.AspNetCore.Hosting;

namespace SharpApi.Example.AwsLambda
{
    public class Program : APIGatewayProxyFunction
    {
        protected override void Init(IWebHostBuilder builder)
        {
            builder
                .UseStartup<AspNetCoreStartup>();
        }
    }
}
