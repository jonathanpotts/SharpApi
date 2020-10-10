using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace SharpApi
{
    /// <summary>
    /// An API result containing a body and metadata.
    /// </summary>
    public abstract class ApiResult : IDisposable
    {
        public HttpStatusCode StatusCode { get; set; }

        public Dictionary<string, List<string>> Headers { get; set; }

        public Stream Body { get; set; }

        public void Dispose()
        {
            Body.Dispose();
        }
    }
}
