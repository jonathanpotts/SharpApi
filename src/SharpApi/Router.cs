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
        private readonly Dictionary<TemplateMatcher, (IList<string>, Type)> _endpoints = new Dictionary<TemplateMatcher, (IList<string>, Type)>();

        /// <summary>
        /// Creates a router that uses <see cref="ApiEndpoint"/> sub-classes for endpoints.
        /// </summary>
        /// <param name="serviceProvider">Service provider used for dependency injection.</param>
        public Router(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            var endpoints = ApiTypeManager.EndpointTypes
                .Select(t => (Type: t, Attribute: t.GetCustomAttribute<ApiEndpointAttribute>()));

            foreach (var endpoint in endpoints)
            {
                var template = TemplateParser.Parse(endpoint.Attribute.Route);
                var matcher = new TemplateMatcher(template, GetDefaults(template));

                var methods = endpoint.Attribute.Methods.Select(m => m.ToUpper()).Distinct().ToList();

                if (methods.Any())
                {
                    _endpoints.Add(matcher, (methods, endpoint.Type));
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
            List<(TemplateMatcher, IList<string>)> matchedMatchers = new List<(TemplateMatcher, IList<string>)>();
            Type endpointType = null;
            RouteValueDictionary values = null;

            foreach (var (matcher, (methods, type)) in _endpoints)
            {
                var matchValues = new RouteValueDictionary();

                if (matcher.TryMatch(path, matchValues))
                {
                    matchedMatchers.Add((matcher, methods));

                    if (methods.Contains(method))
                    {
                        if (endpointType != null)
                        {
                            throw new AmbiguousMatchException("More than one endpoint was found that matches the requested path and method.");
                        }

                        endpointType = type;
                        values = matchValues;
                    }
                }
            }

            if (endpointType == null)
            {
                if (matchedMatchers.Any())
                {
                    return new MethodNotAllowedEndpoint(matchedMatchers.SelectMany(m => m.Item2).ToArray());
                }

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
