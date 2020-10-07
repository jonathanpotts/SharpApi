using System.Collections.Generic;
using System.Text.Json;

namespace SharpApi
{
    public class ApiRequest
    {
        public Dictionary<string, List<string>> Headers { get; private set; }
        public Dictionary<string, List<string>> Query { get; private set; }
        public string BodyJson { get; private set; }

        public ApiRequest(Dictionary<string, List<string>> headers, Dictionary<string, List<string>> query, string bodyJson)
        {
            Headers = headers;
            Query = query;
            BodyJson = bodyJson;
        }
    }

    public class ApiRequest<T> : ApiRequest
    {
        public T Body { get; private set; }

        public ApiRequest(Dictionary<string, List<string>> headers, Dictionary<string, List<string>> query, string bodyJson)
            : base(headers, query, bodyJson)
        {
            Body = JsonSerializer.Deserialize<T>(bodyJson);
        }

        public ApiRequest(ApiRequest request)
            : this(request.Headers, request.Query, request.BodyJson)
        {
        }
    }
}
