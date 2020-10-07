using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SharpApi
{
    /// <summary>
    /// Provides access to API endpoints.
    /// </summary>
    public static class ApiEndpointManager
    {
        /// <summary>
        /// Map of endpoint method and path to endpoint type.
        /// </summary>
        private static readonly Dictionary<string, Type> s_endpointTypes = new Dictionary<string, Type>();

        /// <summary>
        /// Gets an API endpoint.
        /// </summary>
        /// <param name="method">HTTP method for the endpoint.</param>
        /// <param name="path">Path of the endpoint.</param>
        /// <returns>API endpoint.</returns>
        public static ApiEndpoint GetApiEndpoint(string method, string path)
        {
            var endpointKey = $"{method} {path}";

            if (!s_endpointTypes.TryGetValue(endpointKey, out var endpointType))
            {
                var routeEndpoints = AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(a => a.GetTypes())
                    .Where(t => typeof(ApiEndpoint).IsAssignableFrom(t))
                    .Select(t => (Type: t, Attribute: t.GetCustomAttribute<ApiEndpointAttribute>()))
                    .Where(t => t.Attribute?.Path == path);

                endpointType = routeEndpoints.FirstOrDefault(t => t.Attribute.Method == method).Type;

                // Use GET endpoint for HEAD requests when applicable
                if (endpointType == null && method == "HEAD")
                {
                    endpointType = routeEndpoints.FirstOrDefault(t => t.Attribute.Method == "GET" && t.Attribute.UseGetForHead).Type;
                }

                if (endpointType == null)
                {
                    return null;
                }

                s_endpointTypes.Add(endpointKey, endpointType);
            }

            return (ApiEndpoint)Activator.CreateInstance(endpointType);
        }
    }
}
