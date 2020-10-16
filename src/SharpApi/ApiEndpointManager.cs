using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;

namespace SharpApi
{
    public static class ApiEndpointManager
    {
        private static IList<Type> s_endpointTypes;

        public static IList<Type> EndpointTypes
        {
            get
            {
                if (s_endpointTypes != null)
                {
                    return s_endpointTypes;
                }

                var location = new FileInfo(Assembly.GetExecutingAssembly().Location).Directory.FullName;
                new DirectoryCatalog(location);

                var assemblies = AppDomain.CurrentDomain.GetAssemblies();

                s_endpointTypes = assemblies.SelectMany(a => a.GetTypes())
                    .Where(t => typeof(ApiEndpoint).IsAssignableFrom(t))
                    .Where(t => t.GetCustomAttribute<ApiEndpointAttribute>() != null)
                    .Distinct()
                    .ToList();

                return s_endpointTypes;
            }
        }
    }
}
