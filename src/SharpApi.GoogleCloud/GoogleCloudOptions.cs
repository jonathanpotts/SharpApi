namespace SharpApi.GoogleCloud
{
    /// <summary>
    /// Google Cloud configuration options.
    /// </summary>
    public class GoogleCloudOptions
    {
        /// <summary>
        /// Project id.
        /// </summary>
        public string ProjectId { get; set; }

        /// <summary>
        /// Credentials JSON file path.
        /// </summary>
        public string CredentialsJsonFilePath { get; set; }
    }
}
