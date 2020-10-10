using SharpApi.Aws.Lambda;
using System.IO;
using System.Threading.Tasks;

namespace SharpApi.Example.Aws.Lambda
{
    /// <summary>
    /// The root AWS Lambda function handler for the API.
    /// </summary>
    public class Function
    {
        /// <summary>
        /// An AWS Lambda function that calls the SharpAPI AWS Lambda proxy integrated function handler.
        /// </summary>
        /// <param name="input">API Gateway event data.</param>
        /// <returns>API Gateway response data.</returns>
        public async Task<Stream> FunctionHandler(Stream input)
        {
            return await LambdaProxyEndpoint.HandleAsync(input);
        }
    }
}
