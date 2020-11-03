namespace SharpApi.Secrets.GoogleCloudSecretManager
{
    /// <summary>
    /// Google Cloud Secret Manager configuration options.
    /// </summary>
    public class GoogleCloudSecretManagerOptions
    {
        /// <summary>
        /// Google Cloud project id.
        /// </summary>
        public string GoogleCloudProjectId { get; set; }

        /// <summary>
        /// Google Cloud credentials JSON file path.
        /// </summary>
        public string GoogleCloudCredentialsJsonFilePath { get; set; }
    }
}
