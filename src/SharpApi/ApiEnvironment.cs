using System;

namespace SharpApi
{
    /// <summary>
    /// Provides environment information for the API.
    /// </summary>
    public static class ApiEnvironment
    {
        /// <summary>
        /// Checks if running in production environment.
        /// </summary>
        /// <returns>Boolean indicating if running in production environment.</returns>
        public static bool IsProduction()
        {
            var environment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT")
                ?? Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            return environment == null || environment == "Production";
        }

        /// <summary>
        /// Checks if running in staging environment.
        /// </summary>
        /// <returns>Boolean indicating if running in staging environment.</returns>
        public static bool IsStaging()
        {
            var environment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT")
                ?? Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            return environment != null && environment == "Staging";
        }

        /// <summary>
        /// Checks if running in development environment.
        /// </summary>
        /// <returns>Boolean indicating if running in development environment.</returns>
        public static bool IsDevelopment()
        {
            var environment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT")
                ?? Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            return environment != null && environment == "Development";
        }

        /// <summary>
        /// Gets the name of the current environment.
        /// </summary>
        public static string EnvironmentName => Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT")
                ?? Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";
    }
}
