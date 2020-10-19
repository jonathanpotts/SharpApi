namespace SharpApi.Email.Smtp
{
    /// <summary>
    /// Type of encrpytion to use when connecting to a SMTP server.
    /// </summary>
    public enum SmtpEncryption
    {
        /// <summary>
        /// None.
        /// </summary>
        None,

        /// <summary>
        /// SSL or TLS.
        /// </summary>
        SslTls,

        /// <summary>
        /// STARTTLS.
        /// </summary>
        StartTls
    }
}
