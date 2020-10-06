using System.Collections.Generic;
using System.Net;

namespace SharpApi
{
    public class ContentResult : ApiResult
    {
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
