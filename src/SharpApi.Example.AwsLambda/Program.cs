using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Amazon.Lambda.Serialization.SystemTextJson;
using SharpApi.Aws.Lambda;
using System.Threading.Tasks;

namespace SharpApi.Example.Aws.Lambda
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
        /// <returns>API Gateway proxy response.</returns>
        [LambdaSerializer(typeof(DefaultLambdaJsonSerializer))]
        public async Task<APIGatewayProxyResponse> Handler(APIGatewayProxyRequest request)
        {
            return await LambdaProxyEndpoint.HandleAsync(request);
        }
    }
}
