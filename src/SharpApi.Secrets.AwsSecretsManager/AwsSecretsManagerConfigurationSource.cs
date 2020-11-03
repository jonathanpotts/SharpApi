using Microsoft.Extensions.Configuration;

namespace SharpApi.Secrets.AwsSecretsManager
{
    /// <summary>
    /// Builds a configuration provider for AWS Secrets Manager.
    /// </summary>
    public class AwsSecretsManagerConfigurationSource : IConfigurationSource
    {
        /// <summary>
        /// Builds the <see cref="IConfigurationProvider"/> for AWS Secrets Manager.
        /// </summary>
        /// <param name="builder">Configuration builder.</param>
        /// <returns>AWS Secrets Manager configuration provider.</returns>
        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new AwsSecretsManagerConfigurationProvider();
        }
    }
}
