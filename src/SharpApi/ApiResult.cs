using System.Collections.Generic;
using System.Net;

namespace SharpApi
{
    public abstract class ApiResult
    {
        public HttpStatusCode StatusCode { get; set; }

        public Dictionary<string, List<string>> Headers { get; set; }

        public string Body { get; set; }

        public bool BodyIsBase64Encoded { get; set; }
    }
}
