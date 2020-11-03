using Microsoft.Extensions.Configuration;
using System;

namespace SharpApi.Secrets.GoogleCloudSecretManager
{
    /// <summary>
    /// Google Cloud Secret Manager extensions for <see cref="IConfigurationBuilder"/>.
    /// </summary>
    public static class AwsSecretsManagerConfigurationBuilderExtensions
    {
        /// <summary>
        /// Adds Google Cloud Secret Manager to the configuration builder.
        /// </summary>
        /// <param name="configurationBuilder">Configuration builder.</param>
        /// <param name="options">Options used to configure the configuration provider.</param>
        /// <param name="reloadInterval">Interval used for reloading data.</param>
        public static void AddGoogleCloudSecretManager(this IConfigurationBuilder configurationBuilder, GoogleCloudSecretManagerOptions options = null, TimeSpan? reloadInterval = null)
        {
            configurationBuilder.Add(new GoogleCloudSecretManagerConfigurationSource(options, reloadInterval));
        }
    }
}
