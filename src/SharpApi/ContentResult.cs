using System.Collections.Generic;
using System.IO;
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
        /// <param name="contentType">Content MIME type for raw data.</param>
        /// <param name="statusCode">HTTP status code to return.</param>
        public ContentResult(Stream content, string contentType, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            Body = content;

            Headers = new Dictionary<string, List<string>>
            {
                { "Content-Type", new List<string> { contentType } }
            };

            StatusCode = statusCode;
        }
    }
}
