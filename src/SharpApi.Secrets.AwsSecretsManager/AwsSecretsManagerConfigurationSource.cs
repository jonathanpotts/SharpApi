using Microsoft.Extensions.Configuration;
using SharpApi.Aws;

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
        /// Creates an instance of the configuration source for AWS Secrets Manager.
        /// </summary>
        /// <param name="options">Options used to configure the configuration provider.</param>
        public AwsSecretsManagerConfigurationSource(AwsOptions options)
        {
            _options = options;
        }

        /// <summary>
        /// Builds the <see cref="IConfigurationProvider"/> for AWS Secrets Manager.
        /// </summary>
        /// <param name="builder">Configuration builder.</param>
        /// <returns>AWS Secrets Manager configuration provider.</returns>
        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new AwsSecretsManagerConfigurationProvider(_options);
        }
    }
}
