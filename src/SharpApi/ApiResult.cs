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
        /// <summary>
        /// HTTP status code to send with the response.
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Headers to send with the response.
        /// </summary>
        public IDictionary<string, IList<string>> Headers { get; set; }

        /// <summary>
        /// Body to send with the response.
        /// </summary>
        public Stream Body { get; set; }

        public void Dispose()
        {
            Body.Dispose();
        }
    }
}
