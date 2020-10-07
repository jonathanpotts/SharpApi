using System.Collections.Generic;
using System.Net;

namespace SharpApi
{
    /// <summary>
    /// An API result containing a body and metadata.
    /// </summary>
    public abstract class ApiResult
    {
        public HttpStatusCode StatusCode { get; set; }

        public Dictionary<string, List<string>> Headers { get; set; }

        public string Body { get; set; }

        public bool BodyIsBase64Encoded { get; set; }
    }
}
