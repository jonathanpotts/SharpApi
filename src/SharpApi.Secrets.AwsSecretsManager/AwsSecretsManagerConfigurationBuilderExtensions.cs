using Microsoft.Extensions.Configuration;
using SharpApi.Aws;
using System;

namespace SharpApi.Secrets.AwsSecretsManager
{
    /// <summary>
    /// AWS Secrets Manager extensions for <see cref="IConfigurationBuilder"/>.
    /// </summary>
    public static class AwsSecretsManagerConfigurationBuilderExtensions
    {
        /// <summary>
        /// Adds AWS Secrets Manager to the configuration builder.
        /// </summary>
        /// <param name="configurationBuilder">Configuration builder.</param>
        /// <param name="options">Options used to configure the configuration provider.</param>
        /// <param name="reloadInterval">Interval used for reloading data.</param>
        public static void AddAwsSecretsManager(this IConfigurationBuilder configurationBuilder, AwsOptions options = null, TimeSpan? reloadInterval = null)
        {
            configurationBuilder.Add(new AwsSecretsManagerConfigurationSource(options, reloadInterval));
        }
    }
}
