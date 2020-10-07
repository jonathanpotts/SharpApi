using System;
using System.Collections.Generic;

namespace SharpApi.Aws.Lambda
{
    /// <summary>
    /// A model representing incoming event data from an API Gateway request.
    /// </summary>
    public class LambdaProxyRequest
    {
        /// <summary>
        /// AWS context data for the request.
        /// </summary>
        public class Context
        {
            /// <summary>
            /// Time the request was received.
            /// </summary>
            public DateTime RequestTime { get; set; }

            /// <summary>
            /// Requested path.
            /// </summary>
            public string Path { get; set; }

            /// <summary>
            /// AWS account for the request.
            /// </summary>
            public string AccountId { get; set; }

            /// <summary>
            /// Protocol used for the request.
            /// </summary>
            public string Protocol { get; set; }

            /// <summary>
            /// Resource id of the requested resource.
            /// </summary>
            public string ResourceId { get; set; }

            /// <summary>
            /// Stage of the request.
            /// </summary>
            public string Stage { get; set; }

            /// <summary>
            /// Time of the request in Unix epoch format.
            /// </summary>
            public long RequestTimeEpoch { get; set; }

            /// <summary>
            /// Request id.
            /// </summary>
            public string RequestId { get; set; }

            /// <summary>
            /// Identity values provided with the request.
            /// </summary>
            public Dictionary<string, string> Identity { get; set; }

            /// <summary>
            /// Path mapping used to get the requested resource.
            /// </summary>
            public string ResourcePath { get; set; }

            /// <summary>
            /// HTTP method used for the request.
            /// </summary>
            public string HttpMethod { get; set; }

            /// <summary>
            /// API Gateway id.
            /// </summary>
            public string ApiId { get; set; }
        }

        /// <summary>
        /// Path mapping used to get the requested resource.
        /// </summary>
        public string Resource { get; set; }

        /// <summary>
        /// Requested path.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// HTTP method used for the request.
        /// </summary>
        public string HttpMethod { get; set; }

        /// <summary>
        /// Single-value headers for the request.
        /// </summary>
        public Dictionary<string, string> Headers { get; set; }

        /// <summary>
        /// Multi-value headers of the request.
        /// </summary>
        public Dictionary<string, List<string>> MultiValueHeaders { get; set; }

        /// <summary>
        /// Single-value query string parameters provided with the request.
        /// </summary>
        public Dictionary<string, string> QueryStringParameters { get; set; }

        /// <summary>
        /// Multi-value query string parameters provided with the request.
        /// </summary>
        public Dictionary<string, List<string>> MultiValueQueryStringParameters { get; set; }

        /// <summary>
        /// Path parameters used for the request.
        /// </summary>
        public Dictionary<string, string> PathParameters { get; set; }

        /// <summary>
        /// Stage variables used for the request.
        /// </summary>
        public Dictionary<string, string> StageVariables { get; set; }

        /// <summary>
        /// AWS context data provided with the request.
        /// </summary>
        public Context RequestContext { get; set; }

        /// <summary>
        /// Body of the request.
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Specifies if the body is base64 encoded.
        /// </summary>
        public bool IsBase64Encoded { get; set; }

    }
}
