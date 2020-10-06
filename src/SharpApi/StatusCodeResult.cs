using System.Net;

namespace SharpApi
{
    public class StatusCodeResult : ApiResult
    {
        public StatusCodeResult(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }
    }
}
