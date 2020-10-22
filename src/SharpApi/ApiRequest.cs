using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace SharpApi
{
    /// <summary>
    /// An API request.
    /// </summary>
    public class ApiRequest : IDisposable
    {
        /// <summary>
        /// Headers received with the request.
        /// </summary>
        public IDictionary<string, IList<string>> Headers { get; private set; }

        /// <summary>
        /// Query string parameters received with the request.
        /// </summary>
        public IDictionary<string, IList<string>> Query { get; private set; }

        /// <summary>
        /// Route values provided with the request.
        /// </summary>
        public IDictionary<string, object> RouteValues { get; private set; }

        /// <summary>
        /// Body received with the request.
        /// </summary>
        public Stream Body { get; private set; }

        /// <summary>
        /// Creates an API request.
        /// </summary>
        /// <param name="headers">Headers received with the request.</param>
        /// <param name="query">Query string parameters received with the request.</param>
        /// <param name="routeValues">Route values provided with the request.</param>
        /// <param name="body">Body received with the request.</param>
        public ApiRequest(IDictionary<string, IList<string>> headers, IDictionary<string, IList<string>> query, IDictionary<string, object> routeValues, Stream body)
        {
            Headers = headers;
            Query = query;
            RouteValues = routeValues;
            Body = body;
        }

        /// <summary>
        /// Disposes resources used by the request.
        /// </summary>
        public void Dispose()
        {
            Body.Dispose();
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
        public TModel Model { get; private set; }

        /// <summary>
        /// Creates an API request.
        /// </summary>
        /// <param name="headers">Headers received with the request.</param>
        /// <param name="query">Query string parameters received with the request.</param>
        /// <param name="routeValues">Route values provided with the request.</param>
        /// <param name="body">Body received with the request.</param>
        public ApiRequest(IDictionary<string, IList<string>> headers, IDictionary<string, IList<string>> query, IDictionary<string, object> routeValues, Stream body)
            : base(headers, query, routeValues, body)
        {
            using var sr = new StreamReader(body);
            Model = JsonSerializer.Deserialize<TModel>(sr.ReadToEnd());
        }

        /// <summary>
        /// Creates a specialized API request from an existing generic one.
        /// </summary>
        /// <param name="request">Generic API request.</param>
        public ApiRequest(ApiRequest request)
            : this(request.Headers, request.Query, request.RouteValues, request.Body)
        {
        }
    }
}
