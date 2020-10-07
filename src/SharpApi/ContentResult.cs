using System.Collections.Generic;
using System.Net;

namespace SharpApi
{
    /// <summary>
    /// An API result that returns raw data.
    /// </summary>
    public class ContentResult : ApiResult
    {
        /// <summary>
        /// Creates an API result that returns raw data.
        /// </summary>
        /// <param name="content">Raw data to return.</param>
        /// <param name="statusCode">HTTP status code to return.</param>
        /// <param name="contentType">Content MIME type for raw data.</param>
        /// <param name="isBase64Encoded">Determines if the raw data is base64 encoded.</param>
        public ContentResult(string content, HttpStatusCode statusCode = HttpStatusCode.OK, string contentType = "text/plain", bool isBase64Encoded = false)
        {
            StatusCode = statusCode;

            Headers = new Dictionary<string, List<string>>
            {
                { "Content-Type", new List<string> { contentType } }
            };

            Body = content;

            BodyIsBase64Encoded = isBase64Encoded;
        }
    }
}
