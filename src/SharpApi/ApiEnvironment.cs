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
            return EnvironmentName == "Production";
        }

        /// <summary>
        /// Checks if running in staging environment.
        /// </summary>
        /// <returns>Boolean indicating if running in staging environment.</returns>
        public static bool IsStaging()
        {
            return EnvironmentName == "Staging";
        }

        /// <summary>
        /// Checks if running in development environment.
        /// </summary>
        /// <returns>Boolean indicating if running in development environment.</returns>
        public static bool IsDevelopment()
        {
            return EnvironmentName == "Development";
        }

        /// <summary>
        /// Gets the name of the current environment.
        /// </summary>
        public static string EnvironmentName => Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT")
            ?? Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
            ?? Environment.GetEnvironmentVariable("AZURE_FUNCTIONS_ENVIRONMENT")
            ?? ((Environment.GetEnvironmentVariable("AWS_EXECUTION_ENV")?.Contains("AWS_DOTNET_LAMDBA_TEST_TOOL") ?? false) ? "Development" : null)
            ?? "Production";
    }
}
