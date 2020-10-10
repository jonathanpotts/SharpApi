using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace SharpApi.Aws.Lambda
{
    /// <summary>
    /// A model representing outgoing event data that responds to an API Gateway request.
    /// </summary>
    public class LambdaProxyResponse
    {
        /// <summary>
        /// Determines if the body is base64 encoded.
        /// </summary>
        public bool IsBase64Encoded { get; set; }

        /// <summary>
        /// HTTP status code for the response.
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Single-value headers for the response.
        /// </summary>
        public Dictionary<string, string> Headers { get; set; }

        /// <summary>
        /// Multi-value headers for the response.
        /// </summary>
        public Dictionary<string, List<string>> MultiValueHeaders { get; set; }

        /// <summary>
        /// Body of the response.
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Creates a response for an API Gateway request.
        /// </summary>
        /// <param name="statusCode">HTTP status code for response.</param>
        /// <param name="body">Body of the response.</param>
        /// <param name="bodyIsBase64Encoded">Determines if the body is base64 encoded.</param>
        public LambdaProxyResponse(int statusCode, string body = null, bool bodyIsBase64Encoded = false)
        {
            StatusCode = statusCode;
            Body = body;
            IsBase64Encoded = bodyIsBase64Encoded;
        }

        /// <summary>
        /// Converts the response to a stream in JSON format.
        /// </summary>
        /// <returns>Response as a JSON-formatted stream.</returns>
        public async Task<Stream> ToJsonStreamAsync()
        {
            var stream = new MemoryStream();

            await JsonSerializer.SerializeAsync(stream, this);

            return stream;
        }
    }
}
