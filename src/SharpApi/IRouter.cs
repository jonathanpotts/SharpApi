using System.Collections.Generic;

namespace SharpApi
{
    /// <summary>
    /// Interface for routers that handle routing to API endpoints.
    /// </summary>
    public interface IRouter
    {
        /// <summary>
        /// Gets the endpoint for the requested method and path.
        /// </summary>
        /// <param name="method">HTTP method of the request.</param>
        /// <param name="path">Requested path.</param>
        /// <param name="routeValues">Route values that were processed from the route.</param>
        /// <returns>An instance of the endpoint that the route resolved to.</returns>
        public ApiEndpoint Route(string method, string path, IDictionary<string, object> routeValues = null);
    }
}
