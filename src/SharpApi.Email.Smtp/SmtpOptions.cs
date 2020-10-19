namespace SharpApi.Email.Smtp
{
    /// <summary>
    /// SMTP configuration options.
    /// </summary>
    public class SmtpOptions
    {
        /// <summary>
        /// SMTP server host.
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// SMTP server port.
        /// </summary>
        public int Port { get; set; } = 0;

        /// <summary>
        /// SMTP encryption.
        /// </summary>
        public SmtpEncryption Encryption { get; set; }

        /// <summary>
        /// SMTP username.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// SMTP password.
        /// </summary>
        public string Password { get; set; }
    }
}
