using System;
using System.Collections.Generic;
using System.Reflection;

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
        /// <exception cref="AmbiguousMatchException">Thrown when more than one endpoint was found that matches the requested path and method.</exception>
        /// <exception cref="NotSupportedException">Thrown when the requested path is valid but there is no handler for the specified method.</exception>
        public ApiEndpoint Route(string method, string path, IDictionary<string, object> routeValues = null);
    }
}
