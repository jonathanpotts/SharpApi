namespace SharpApi.Aws
{
    /// <summary>
    /// AWS configuration options.
    /// </summary>
    public class AwsOptions
    {
        /// <summary>
        /// AWS Access Key Id.
        /// </summary>
        public string AwsAccessKeyId { get; set; }

        /// <summary>
        /// AWS Secret Access Key.
        /// </summary>
        public string AwsSecretAccessKey { get; set; }

        /// <summary>
        /// AWS Region System Name.
        /// </summary>
        public string AwsRegionSystemName { get; set; }
    }
}
