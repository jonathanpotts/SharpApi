using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;

namespace SharpApi.Aws
{
    /// <summary>
    /// AWS configuration options.
    /// </summary>
    public class AwsOptions
    {
        /// <summary>
        /// Profile.
        /// </summary>
        public string Profile { get; set; }

        /// <summary>
        /// Region.
        /// </summary>
        public string Region { get; set; }

        /// <summary>
        /// Credentials loaded from profile.
        /// </summary>
        public AWSCredentials Credentials
        {
            get
            {
                var credentialProfileStoreChain = new CredentialProfileStoreChain();

                if (!credentialProfileStoreChain.TryGetAWSCredentials(Profile, out var credentials))
                {
                    throw new AmazonClientException($"Unable to find profile with name of {Profile}.");
                }

                return credentials;
            }
        }
    }
}
