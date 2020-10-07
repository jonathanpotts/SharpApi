using System.Collections.Generic;
using System.Text.Json;

namespace SharpApi
{
    /// <summary>
    /// An API request.
    /// </summary>
    public class ApiRequest
    {
        /// <summary>
        /// Headers received with the request.
        /// </summary>
        public Dictionary<string, List<string>> Headers { get; private set; }

        /// <summary>
        /// Query string parameters received with the request.
        /// </summary>
        public Dictionary<string, List<string>> Query { get; private set; }

        /// <summary>
        /// Body received with the request in JSON format.
        /// </summary>
        public string BodyJson { get; private set; }

        /// <summary>
        /// Creates an API request.
        /// </summary>
        /// <param name="headers">Headers received with the request.</param>
        /// <param name="query">Query string parameters received with the request.</param>
        /// <param name="bodyJson">Body received with the request in JSON format.</param>
        public ApiRequest(Dictionary<string, List<string>> headers, Dictionary<string, List<string>> query, string bodyJson)
        {
            Headers = headers;
            Query = query;
            BodyJson = bodyJson;
        }
    }

    /// <summary>
    /// An API request that converts the body JSON into an object.
    /// </summary>
    /// <typeparam name="T">Type of object to deserialize body into.</typeparam>
    public class ApiRequest<T> : ApiRequest
    {
        /// <summary>
        /// Body received with the request.
        /// </summary>
        public T Body { get; private set; }

        /// <summary>
        /// Creates an API request.
        /// </summary>
        /// <param name="headers">Headers received with the request.</param>
        /// <param name="query">Query string parameters received with the request.</param>
        /// <param name="bodyJson">Body received with the request in JSON format.</param>
        public ApiRequest(Dictionary<string, List<string>> headers, Dictionary<string, List<string>> query, string bodyJson)
            : base(headers, query, bodyJson)
        {
            Body = JsonSerializer.Deserialize<T>(bodyJson);
        }

        /// <summary>
        /// Creates a specialized API request from an existing generic one.
        /// </summary>
        /// <param name="request">Generic API request.</param>
        public ApiRequest(ApiRequest request)
            : this(request.Headers, request.Query, request.BodyJson)
        {
        }
    }
}
