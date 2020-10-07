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
        public string Path { get; set; }

        /// <summary>
        /// Endpoint handles requests using this HTTP method.
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// Determines if the endpoint should handle HEAD requests if it also handles GET requests.
        /// </summary>
        public bool UseGetForHead { get; set; }

        /// <summary>
        /// An attribute containing metadata used for routing requests to an endpoint.
        /// </summary>
        /// <param name="path">Endpoint handles requests to this path.</param>
        /// <param name="method">Endpoint handles requests using this HTTP method.</param>
        /// <param name="useGetForHead">Determines if the endpoint should handle HEAD requests if it also handles GET requests.</param>
        public ApiEndpointAttribute(string path, string method = "GET", bool useGetForHead = false)
        {
            Path = path;
            Method = method.ToUpper();
            UseGetForHead = useGetForHead;
        }
    }
}
