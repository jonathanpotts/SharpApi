﻿using System.IO;
using System.Net;
using System.Text;

namespace SharpApi
{
    /// <summary>
    /// An API result that returns a string.
    /// </summary>
    public class StringResult : ContentResult
    {
        /// <summary>
        /// Creates an API result that returns a string.
        /// </summary>
        /// <param name="content">String to return.</param>
        /// <param name="statusCode">HTTP status code to return.</param>
        public StringResult(string content, HttpStatusCode statusCode = HttpStatusCode.OK)
            : base(new MemoryStream(Encoding.UTF8.GetBytes(content)), "text/plain; charset=utf-8", statusCode)
        {
        }
    }
}