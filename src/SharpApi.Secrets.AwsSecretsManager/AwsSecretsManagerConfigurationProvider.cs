using Amazon;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using Microsoft.Extensions.Configuration;
using SharpApi.Aws;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace SharpApi.Secrets.AwsSecretsManager
{
    /// <summary>
    /// Provides configuration key-value pairs from AWS Secrets Manager.
    /// </summary>
    public class AwsSecretsManagerConfigurationProvider : ConfigurationProvider, IDisposable
    {
        /// <summary>
        /// Client used to access AWS Secrets Manager.
        /// </summary>
        private readonly IAmazonSecretsManager _secretsManager;

        /// <summary>
        /// Creates an instance of the configuration provider for AWS Secrets Manager.
        /// </summary>
        /// <param name="options">Options used to configure the configuration provider.</param>
        public AwsSecretsManagerConfigurationProvider(AwsOptions options)
        {
            if (options == null)
            {
                _secretsManager = new AmazonSecretsManagerClient();
            }
            else if (!string.IsNullOrEmpty(options.AwsRegionSystemName))
            {
                _secretsManager = new AmazonSecretsManagerClient(options.AwsAccessKeyId, options.AwsSecretAccessKey, RegionEndpoint.GetBySystemName(options.AwsRegionSystemName));
            }
            else
            {
                _secretsManager = new AmazonSecretsManagerClient(options.AwsAccessKeyId, options.AwsSecretAccessKey);
            }
        }

        /// <summary>
        /// Loads (or reloads) the data from AWS Secrets Manager.
        /// </summary>
        public override void Load() => LoadAsync().ConfigureAwait(false).GetAwaiter().GetResult();

        /// <summary>
        /// Loads (or reloads) the data from AWS Secrets Manager.
        /// </summary>
        private async Task LoadAsync()
        {
            ListSecretsResponse listSecretsResponse = null;
            var secretKeys = new List<string>();

            do
            {
                ListSecretsRequest listSecretsRequest;

                if (!string.IsNullOrEmpty(listSecretsResponse?.NextToken))
                {
                    listSecretsRequest = new ListSecretsRequest
                    {
                        NextToken = listSecretsResponse.NextToken
                    };
                }
                else
                {
                    listSecretsRequest = new ListSecretsRequest();
                }

                listSecretsResponse = await _secretsManager.ListSecretsAsync(listSecretsRequest);

                if (listSecretsResponse.HttpStatusCode != HttpStatusCode.OK)
                {
                    throw new HttpRequestException("AWS Secrets Manager returned an error while trying to retrieve the list of secrets.");
                }

                secretKeys.AddRange(listSecretsResponse.SecretList.Select(l => l.Name));
            }
            while (!string.IsNullOrEmpty(listSecretsResponse?.NextToken));

            var secrets = new Dictionary<string, string>();

            var tasks = new List<Task<GetSecretValueResponse>>();

            foreach (var key in secretKeys)
            {
                var getSecretValueRequest = new GetSecretValueRequest
                {
                    SecretId = key
                };

                tasks.Add(_secretsManager.GetSecretValueAsync(getSecretValueRequest));
            }

            await Task.WhenAll(tasks).ConfigureAwait(false);

            foreach (var task in tasks)
            {
                var getSecretValueResponse = task.Result;

                if (getSecretValueResponse.HttpStatusCode != HttpStatusCode.OK)
                {
                    throw new HttpRequestException("AWS Secrets Manager returned an error while trying to retrieve the value of a secret.");
                }

                secrets.Add(getSecretValueResponse.Name, getSecretValueResponse.SecretString);
            }

            Data = secrets;
        }

        /// <summary>
        /// Disposes resources used by the configuration provider.
        /// </summary>
        public void Dispose()
        {
            _secretsManager.Dispose();
        }
    }
}
