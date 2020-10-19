using System;

namespace SharpApi.Email.SendGrid
{
    /// <summary>
    /// SendGrid configuration options.
    /// </summary>
    public class SendGridOptions
    {
        /// <summary>
        /// SendGrid API key.
        /// </summary>
        public string ApiKey { get; set; }
    }
}
