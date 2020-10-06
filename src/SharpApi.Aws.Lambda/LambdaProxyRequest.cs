using System;
using System.Collections.Generic;

namespace SharpApi.Aws.Lambda
{
    public class LambdaProxyRequest
    {
        public class Context
        {
            public DateTime RequestTime { get; set; }
            public string Path { get; set; }
            public string AccountId { get; set; }
            public string Protocol { get; set; }
            public string ResourceId { get; set; }
            public string Stage { get; set; }
            public long RequestTimeEpoch { get; set; }
            public string RequestId { get; set; }
            public CognitoContext Identity { get; set; }
            public string ResourcePath { get; set; }
            public string HttpMethod { get; set; }
            public string ApiId { get; set; }
        }

        public string Resource { get; set; }
        public string Path { get; set; }
        public string HttpMethod { get; set; }
        public Dictionary<string, string> Headers { get; set; }
        public Dictionary<string, List<string>> MultiValueHeaders { get; set; }
        public Dictionary<string, string> QueryStringParameters { get; set; }
        public Dictionary<string, List<string>> MultiValueQueryStringParameters { get; set; }
        public Dictionary<string, string> PathParameters { get; set; }
        public Dictionary<string, string> StageVariables { get; set; }
        public Context RequestContext { get; set; }
        public string Body { get; set; }
        public bool IsBase64Encoded { get; set; }

    }
}
