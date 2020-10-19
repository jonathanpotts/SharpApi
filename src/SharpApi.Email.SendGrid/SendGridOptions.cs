using System;

namespace SharpApi.Email.SendGrid
{
    public class SendGridOptions
    {
        private string _apiKey;

        public string ApiKey
        {
            get => _apiKey;
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                _apiKey = value;
            }
        }
    }
}
