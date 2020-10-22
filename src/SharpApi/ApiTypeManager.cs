using System;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;

namespace SharpApi
{
    /// <summary>
    /// Utility methods for handling API reflection.
    /// </summary>
    public static class ApiTypeManager
    {
        /// <summary>
        /// Types from assemblies within the executing assembly's directory.
        /// </summary>
        private static Type[] s_types;

        /// <summary>
        /// Types from assemblies within the executing assembly's directory.
        /// </summary>
        public static Type[] Types
        {
            get
            {
                if (s_types != null)
                {
                    return s_types;
                }

                var location = new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName;
                new DirectoryCatalog(location);

                var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                    .Where(a => !a.IsDynamic && new FileInfo(a.Location).DirectoryName == location)
                    .ToArray();

                s_types = assemblies.SelectMany(a => a.GetTypes()).ToArray();

                return s_types;
            }
        }

        /// <summary>
        /// API endpoint types.
        /// </summary>
        private static Type[] s_endpointTypes;

        /// <summary>
        /// API endpoint types.
        /// </summary>
        public static Type[] EndpointTypes
        {
            get
            {
                if (s_endpointTypes != null)
                {
                    return s_endpointTypes;
                }

                s_endpointTypes = GetChildTypes(typeof(ApiEndpoint))
                    .Where(t => t.GetCustomAttribute<ApiEndpointAttribute>() != null)
                    .ToArray();

                return s_endpointTypes;
            }
        }

        /// <summary>
        /// Determines if the API startup type was searched for.
        /// </summary>
        private static bool s_searchedForStartupType;

        /// <summary>
        /// API startup type.
        /// </summary>
        private static Type s_startupType;

        /// <summary>
        /// API startup type.
        /// </summary>
        /// <exception cref="AmbiguousMatchException">Thrown when there is more than one implementation of <see cref="IApiStartup"/>.</exception>
        public static Type StartupType
        {
            get
            {
                if (s_searchedForStartupType)
                {
                    return s_startupType;
                }

                var types = GetChildTypes(typeof(IApiStartup));

                if (types.Length > 1)
                {
                    throw new AmbiguousMatchException($"There can only be one implementation of {nameof(IApiStartup)}.");
                }

                s_startupType = types.FirstOrDefault();
                s_searchedForStartupType = true;

                return s_startupType;
            }
        }

        /// <summary>
        /// Gets the child types of the parent type.
        /// </summary>
        /// <param name="parentType">Parent type.</param>
        /// <returns>Child types.</returns>
        private static Type[] GetChildTypes(Type parentType)
        {
            return Types
                .Where(t => parentType.IsAssignableFrom(t) && t.IsClass && !t.IsAbstract)
                .Distinct()
                .ToArray();
        }
    }
}
