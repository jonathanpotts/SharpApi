using System.Net;

namespace SharpApi
{
    /// <summary>
    /// An API result that returns a HTTP status code.
    /// </summary>
    public class StatusCodeResult : ApiResult
    {
        /// <summary>
        /// Creates an API result that returns a HTTP status code.
        /// </summary>
        /// <param name="statusCode">HTTP status code to return.</param>
        public StatusCodeResult(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }
    }
}
