using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Amazon.Lambda.Serialization.SystemTextJson;
using SharpApi.AwsLambda;
using System.Threading.Tasks;

[assembly: LambdaSerializer(typeof(DefaultLambdaJsonSerializer))]

namespace SharpApi.Example.AwsLambda
{
    /// <summary>
    /// The program used for handling AWS Lambda requests through API Gateway proxy integration.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// An AWS Lambda function that calls the SharpAPI AWS Lambda proxy integrated function handler.
        /// </summary>
        /// <param name="request">API Gateway proxy request.</param>
        /// <param name="context">Lambda conext.</param>
        /// <returns>API Gateway proxy response.</returns>
        public async Task<APIGatewayProxyResponse> Handler(APIGatewayProxyRequest request, ILambdaContext context)
        {
            return await AwsLambdaProxyEndpoint.HandleAsync(request, context);
        }
    }
}
