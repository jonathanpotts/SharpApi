using Microsoft.Extensions.Configuration;

namespace SharpApi.Secrets.GoogleCloudSecretManager
{
    /// <summary>
    /// Builds a configuration provider for Google Cloud Secret Manager.
    /// </summary>
    public class GoogleCloudSecretManagerConfigurationSource : IConfigurationSource
    {
        /// <summary>
        /// Builds the <see cref="IConfigurationProvider"/> for Google Cloud Secret Manager.
        /// </summary>
        /// <param name="builder">Configuration builder.</param>
        /// <returns>Google Cloud Secret Manager configuration provider.</returns>
        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new GoogleCloudSecretManagerConfigurationProvider();
        }
    }
}
