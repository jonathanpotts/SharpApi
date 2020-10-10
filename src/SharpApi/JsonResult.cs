using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.Json;

namespace SharpApi
{
    /// <summary>
    /// An API result that returns an object as JSON.
    /// </summary>
    public class JsonResult : ApiResult
    {
        /// <summary>
        /// Creates an API result that returns an object as JSON.
        /// </summary>
        /// <param name="obj">Object to return.</param>
        /// <param name="statusCode">HTTP status code to return.</param>
        public JsonResult(object obj, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            Body = new MemoryStream(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(obj, obj.GetType())));

            Headers = new Dictionary<string, List<string>>
            {
                { "Content-Type", new List<string> { "application/json; charset=utf-8" } }
            };

            StatusCode = statusCode;
        }
    }
}
