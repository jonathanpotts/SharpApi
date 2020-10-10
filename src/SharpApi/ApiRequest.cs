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
        /// Body received with the request as a string.
        /// </summary>
        public string Body { get; private set; }

        /// <summary>
        /// Determines if the body is base64 encoded.
        /// </summary>
        public bool BodyIsBase64Encoded { get; private set; }

        /// <summary>
        /// Creates an API request.
        /// </summary>
        /// <param name="headers">Headers received with the request.</param>
        /// <param name="query">Query string parameters received with the request.</param>
        /// <param name="body">Body received with the request as a string.</param>
        /// <param name="bodyIsBase64Encoded">Determines if the body is base64 encoded.</param>
        public ApiRequest(Dictionary<string, List<string>> headers, Dictionary<string, List<string>> query, string body, bool bodyIsBase64Encoded = false)
        {
            Headers = headers;
            Query = query;
            Body = body;
            BodyIsBase64Encoded = bodyIsBase64Encoded;
        }
    }

    /// <summary>
    /// An API request that converts the body JSON into an object.
    /// </summary>
    /// <typeparam name="TModel">Type of object to deserialize body into.</typeparam>
    public class ApiRequest<TModel> : ApiRequest
    {
        /// <summary>
        /// Body received with the request.
        /// </summary>
        public TModel BodyFromJson { get; private set; }

        /// <summary>
        /// Creates an API request.
        /// </summary>
        /// <param name="headers">Headers received with the request.</param>
        /// <param name="query">Query string parameters received with the request.</param>
        /// <param name="bodyJson">Body received with the request in JSON format.</param>
        /// <param name="bodyIsBase64Encoded">Determines if the body is base64 encoded.</param>
        public ApiRequest(Dictionary<string, List<string>> headers, Dictionary<string, List<string>> query, string bodyJson, bool bodyIsBase64Encoded = false)
            : base(headers, query, bodyJson, bodyIsBase64Encoded)
        {
            BodyFromJson = JsonSerializer.Deserialize<TModel>(bodyJson);
        }

        /// <summary>
        /// Creates a specialized API request from an existing generic one.
        /// </summary>
        /// <param name="request">Generic API request.</param>
        public ApiRequest(ApiRequest request)
            : this(request.Headers, request.Query, request.Body)
        {
        }
    }
}
