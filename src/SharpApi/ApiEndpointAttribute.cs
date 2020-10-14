using System;

namespace SharpApi
{
    /// <summary>
    /// An attribute containing metadata used for routing requests to an endpoint.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ApiEndpointAttribute : Attribute
    {
        /// <summary>
        /// Endpoint handles requests to this path.
        /// </summary>
        public string Route { get; set; }

        /// <summary>
        /// Endpoint handles requests using these HTTP methods.
        /// </summary>
        public string[] Methods { get; set; }

        /// <summary>
        /// An attribute containing metadata used for routing requests to an endpoint.
        /// </summary>
        /// <param name="route">Endpoint handles requests for this route.</param>
        /// <param name="methods">Endpoint handles requests using these HTTP methods. Uses GET if none are provided.</param>
        public ApiEndpointAttribute(string route, params string[] methods)
        {
            Route = route;
            Methods = methods?.Length > 0 ? methods : new string[] { "GET" };
        }
    }
}
