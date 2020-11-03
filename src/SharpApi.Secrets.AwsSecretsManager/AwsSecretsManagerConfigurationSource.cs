using Microsoft.Extensions.Configuration;
using SharpApi.Aws;
using System;

namespace SharpApi.Secrets.AwsSecretsManager
{
    /// <summary>
    /// Builds a configuration provider for AWS Secrets Manager.
    /// </summary>
    public class AwsSecretsManagerConfigurationSource : IConfigurationSource
    {
        /// <summary>
        /// Options used to configure the configuration provider.
        /// </summary>
        private readonly AwsOptions _options;

        /// <summary>
        /// Interval used for reloading data.
        /// </summary>
        private readonly TimeSpan? _reloadInterval;

        /// <summary>
        /// Creates an instance of the configuration source for AWS Secrets Manager.
        /// </summary>
        /// <param name="options">Options used to configure the configuration provider.</param>
        /// <param name="reloadInterval">Interval used for reloading data.</param>
        public AwsSecretsManagerConfigurationSource(AwsOptions options, TimeSpan? reloadInterval)
        {
            _options = options;
            _reloadInterval = reloadInterval;
        }

        /// <summary>
        /// Builds the <see cref="IConfigurationProvider"/> for AWS Secrets Manager.
        /// </summary>
        /// <param name="builder">Configuration builder.</param>
        /// <returns>AWS Secrets Manager configuration provider.</returns>
        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new AwsSecretsManagerConfigurationProvider(_options, _reloadInterval);
        }
    }
}
