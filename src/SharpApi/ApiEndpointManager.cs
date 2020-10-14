using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.IO;
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

        public static ILogger Logger { get; set; }

        public static IServiceProvider ServiceProvider { get; private set; }

        /// <summary>
        /// Router for endpoints.
        /// </summary>
        private static IRouter s_router;

        /// <summary>
        /// Router for endpoints.
        /// </summary>
        public static IRouter Router
        {
            get
            {
                if (s_router != null)
                {
                    return s_router;
                }

                var location = new FileInfo(Assembly.GetExecutingAssembly().Location).Directory.FullName;
                new DirectoryCatalog(location);

                var assemblies = AppDomain.CurrentDomain.GetAssemblies();

                var endpoints = assemblies.SelectMany(a => a.GetTypes())
                    .Where(t => typeof(ApiEndpoint).IsAssignableFrom(t))
                    .Select(t => (Type: t, Attribute: t.GetCustomAttribute<ApiEndpointAttribute>()))
                    .Where(t => t.Attribute != null);

                var serviceCollection = new ServiceCollection()
                    .AddRouting(options => { });

                foreach (var endpoint in endpoints)
                {
                    serviceCollection.AddTransient(endpoint.Type);
                }

                ServiceProvider = serviceCollection
                    .BuildServiceProvider();

                var appBuilder = new ApplicationBuilder(ServiceProvider);

                var routeBuilder = new RouteBuilder(appBuilder);

                foreach (var endpoint in endpoints)
                {
                    foreach (var verb in endpoint.Attribute.Methods ?? Enumerable.Empty<string>())
                    {
                        var endpointType = endpoint.Type;

                        routeBuilder.MapVerb(verb.ToUpper(), endpoint.Attribute.Route, async (context) =>
                        {
                            var instance = (ApiEndpoint)ServiceProvider.GetService(endpointType);
                            await instance.HandleAsync(context);
                        });
                    }
                }

                s_router = routeBuilder.Build();
                return s_router;
            }
        }

        /// <summary>
        /// Gets an API endpoint.
        /// </summary>
        /// <param name="method">HTTP method for the endpoint.</param>
        /// <param name="path">Path of the endpoint.</param>
        /// <returns>API endpoint.</returns>
        public static ApiEndpoint GetApiEndpoint(string method, string path)
        {
            method = method.ToUpper();
            path = path.TrimEnd('/');

            var endpointKey = $"{method} {path}";

            if (!s_endpointTypes.TryGetValue(endpointKey, out var endpointType))
            {
                var location = new FileInfo(Assembly.GetExecutingAssembly().Location).Directory.FullName;

                // Load all assemblies bundled with the API runtime
                var catalog = new DirectoryCatalog(location);
                var assemblies = AppDomain.CurrentDomain.GetAssemblies();

                var routeEndpoints = AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(a => a.GetTypes())
                    .Where(t => typeof(ApiEndpoint).IsAssignableFrom(t))
                    .Select(t => (Type: t, Attribute: t.GetCustomAttribute<ApiEndpointAttribute>()))
                    .Where(t => t.Attribute?.Route.TrimEnd('/') == path);

                endpointType = routeEndpoints.FirstOrDefault().Type;

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
