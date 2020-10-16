using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SharpApi
{
    /// <summary>
    /// Handles routing to API endpoints.
    /// </summary>
    // Based on: https://blog.markvincze.com/matching-route-templates-manually-in-asp-net-core/
    public class Router : IRouter
    {
        /// <summary>
        /// Service provider used for dependency injection.
        /// </summary>
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Collection that maps route template matchers to endpoints.
        /// </summary>
        private readonly Dictionary<string, Dictionary<TemplateMatcher, Type>> _endpoints = new Dictionary<string, Dictionary<TemplateMatcher, Type>>();

        /// <summary>
        /// Creates a router that uses <see cref="ApiEndpoint"/> sub-classes for endpoints.
        /// </summary>
        /// <param name="serviceProvider">Service provider used for dependency injection.</param>
        public Router(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            var endpoints = ApiEndpointManager.EndpointTypes
                .Select(t => (Type: t, Attribute: t.GetCustomAttribute<ApiEndpointAttribute>()));

            foreach (var endpoint in endpoints)
            {
                var template = TemplateParser.Parse(endpoint.Attribute.Route);
                var matcher = new TemplateMatcher(template, GetDefaults(template));

                foreach (var method in endpoint.Attribute.Methods.Select(m => m.ToUpper()).Distinct())
                {
                    if (!_endpoints.TryGetValue(method, out var dictionary))
                    {
                        dictionary = new Dictionary<TemplateMatcher, Type>();
                        _endpoints.Add(method, dictionary);
                    }

                    dictionary.Add(matcher, endpoint.Type);
                }
            }
        }

        /// <summary>
        /// Gets the default route values for a route template.
        /// </summary>
        /// <param name="parsedTemplate">Route template to get the route values for.</param>
        /// <returns>Default route values for the provided route template.</returns>
        private RouteValueDictionary GetDefaults(RouteTemplate parsedTemplate)
        {
            var result = new RouteValueDictionary();

            foreach (var parameter in parsedTemplate.Parameters)
            {
                if (parameter.DefaultValue != null)
                {
                    result.Add(parameter.Name, parameter.DefaultValue);
                }
            }

            return result;
        }

        public ApiEndpoint Route(string method, string path, IDictionary<string, object> routeValues = null)
        {
            if (!_endpoints.TryGetValue(method.ToUpper(), out var dictionary))
            {
                return null;
            }

            Type endpointType = null;
            RouteValueDictionary values = null;

            foreach (var (matcher, type) in dictionary)
            {
                var matchValues = new RouteValueDictionary();

                if (matcher.TryMatch(path, matchValues))
                {
                    if (endpointType != null)
                    {
                        throw new AmbiguousMatchException("More than one endpoint was found that matches the requested path.");
                    }

                    endpointType = type;
                    values = matchValues;
                }
            }

            if (endpointType == null)
            {
                return null;
            }

            if (routeValues != null)
            {
                foreach (var value in values ?? Enumerable.Empty<KeyValuePair<string, object>>())
                {
                    routeValues.Add(value.Key, value.Value);
                }
            }

            return (ApiEndpoint)_serviceProvider.GetService(endpointType);
        }
    }
}
