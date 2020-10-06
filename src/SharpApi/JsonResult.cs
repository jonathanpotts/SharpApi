using System.Collections.Generic;
using System.Net;
using System.Text.Json;

namespace SharpApi
{
    public class JsonResult : ApiResult
    {
        public JsonResult(object obj, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            StatusCode = statusCode;

            Headers = new Dictionary<string, List<string>>
            {
                { "Content-Type", new List<string> { "application/json" } }
            };

            Body = JsonSerializer.Serialize(obj, obj.GetType());
        }
    }
}
