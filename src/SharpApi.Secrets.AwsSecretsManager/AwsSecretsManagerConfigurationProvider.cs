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
using System.Threading;
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
        /// Cancellation token used for reloading data on an interval.
        /// </summary>
        private readonly CancellationTokenSource _cancellationToken;

        /// <summary>
        /// Interval used for reloading data.
        /// </summary>
        private readonly TimeSpan? _reloadInterval;

        /// <summary>
        /// Task used for reloading data on an interval.
        /// </summary>
        private Task _reloadTask;

        /// <summary>
        /// Creates an instance of the configuration provider for AWS Secrets Manager.
        /// </summary>
        /// <param name="options">Options used to configure the configuration provider.</param>
        /// <param name="reloadInterval">Interval used for reloading data.</param>
        public AwsSecretsManagerConfigurationProvider(AwsOptions options, TimeSpan? reloadInterval)
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

            if (reloadInterval.HasValue)
            {
                if (reloadInterval.Value <= TimeSpan.Zero)
                {
                    throw new ArgumentOutOfRangeException(nameof(reloadInterval), "The reload interval must be positive.");
                }

                _reloadInterval = reloadInterval;
            }

            _reloadTask = null;
            _cancellationToken = new CancellationTokenSource();
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
            async Task<IEnumerable<string>> GetSecretKeysAsync()
            {
                ListSecretsResponse response = null;

                var secretKeys = new List<string>();

                do
                {
                    var request = new ListSecretsRequest();

                    if (!string.IsNullOrEmpty(response?.NextToken))
                    {
                        request.NextToken = response.NextToken;
                    }

                    response = await _secretsManager.ListSecretsAsync(request);

                    if (response.HttpStatusCode != HttpStatusCode.OK)
                    {
                        throw new HttpRequestException("AWS Secrets Manager returned an error while trying to retrieve the list of secrets.");
                    }

                    foreach (var secretKey in response.SecretList.Select(l => l.Name))
                    {
                        secretKeys.Add(secretKey);
                    }
                }
                while (!string.IsNullOrEmpty(response?.NextToken));

                return secretKeys;
            }

            async Task<Dictionary<string, string>> GetSecretsAsync()
            {
                var secrets = new Dictionary<string, string>();

                foreach (var key in await GetSecretKeysAsync())
                {
                    var request = new GetSecretValueRequest
                    {
                        SecretId = key
                    };

                    var response = await _secretsManager.GetSecretValueAsync(request);

                    if (response.HttpStatusCode != HttpStatusCode.OK)
                    {
                        throw new HttpRequestException("AWS Secrets Manager returned an error while trying to retrieve the value of a secret.");
                    }

                    string value;

                    try
                    {
                        value = response.SecretString;
                    }
                    catch
                    {
                        continue;
                    }

                    secrets.Add(response.Name, value);
                }

                return secrets;
            }

            Data = await GetSecretsAsync();

            if (_reloadInterval != null && _reloadTask == null)
            {
                _reloadTask = Task.Run(async () =>
                {
                    while (!_cancellationToken.IsCancellationRequested)
                    {
                        await Task.Delay(_reloadInterval.Value, _cancellationToken.Token);

                        try
                        {
                            await LoadAsync();
                        }
                        catch
                        {
                            // ignore exceptions
                        }
                    }
                },
                _cancellationToken.Token);
            }
        }

        /// <summary>
        /// Disposes resources used by the configuration provider.
        /// </summary>
        public void Dispose()
        {
            _cancellationToken.Cancel();
            _secretsManager.Dispose();
        }
    }
}
