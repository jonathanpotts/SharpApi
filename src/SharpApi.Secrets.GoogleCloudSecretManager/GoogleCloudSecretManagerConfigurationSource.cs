using Microsoft.Extensions.Configuration;
using System;

namespace SharpApi.Secrets.GoogleCloudSecretManager
{
    /// <summary>
    /// Builds a configuration provider for Google Cloud Secret Manager.
    /// </summary>
    public class GoogleCloudSecretManagerConfigurationSource : IConfigurationSource
    {
        /// <summary>
        /// Options used to configure the configuration provider.
        /// </summary>
        private readonly GoogleCloudSecretManagerOptions _options;

        /// <summary>
        /// Interval used for reloading data.
        /// </summary>
        private readonly TimeSpan? _reloadInterval;

        /// <summary>
        /// Creates an instance of the configuration source for Google Cloud Secret Manager.
        /// </summary>
        /// <param name="options">Options used to configure the configuration provider.</param>
        /// <param name="reloadInterval">Interval used for reloading data.</param>
        public GoogleCloudSecretManagerConfigurationSource(GoogleCloudSecretManagerOptions options, TimeSpan? reloadInterval)
        {
            _options = options;
            _reloadInterval = reloadInterval;
        }

        /// <summary>
        /// Builds the <see cref="IConfigurationProvider"/> for Google Cloud Secret Manager.
        /// </summary>
        /// <param name="builder">Configuration builder.</param>
        /// <returns>Google Cloud Secret Manager configuration provider.</returns>
        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new GoogleCloudSecretManagerConfigurationProvider(_options, _reloadInterval);
        }
    }
}
