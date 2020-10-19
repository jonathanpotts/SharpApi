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
        private string _apiKey;

        /// <summary>
        /// SendGrid API key.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown when the value provided is null.</exception>
        public string ApiKey
        {
            get => _apiKey;
            set
            {
                _apiKey = value ?? throw new ArgumentNullException(nameof(value));
            }
        }
    }
}
