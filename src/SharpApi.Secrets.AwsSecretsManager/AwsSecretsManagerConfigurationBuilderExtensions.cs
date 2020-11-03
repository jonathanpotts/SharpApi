using Microsoft.Extensions.Configuration;
using SharpApi.Aws;

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
        public static void AddAwsSecretsManager(this IConfigurationBuilder configurationBuilder, AwsOptions options = null)
        {
            configurationBuilder.Add(new AwsSecretsManagerConfigurationSource(options));
        }
    }
}
