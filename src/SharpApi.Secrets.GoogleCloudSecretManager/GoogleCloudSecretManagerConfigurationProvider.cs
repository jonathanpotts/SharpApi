using Google.Api.Gax.ResourceNames;
using Google.Cloud.SecretManager.V1;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SharpApi.Secrets.GoogleCloudSecretManager
{
    /// <summary>
    /// Provides configuration key-value pairs from Google Cloud Secret Manager.
    /// </summary>
    public class GoogleCloudSecretManagerConfigurationProvider : ConfigurationProvider, IDisposable
    {
        /// <summary>
        /// Options used to configure the configuration provider.
        /// </summary>
        private readonly GoogleCloudSecretManagerOptions _options;

        /// <summary>
        /// Client used to access Google Client Secret Manager.
        /// </summary>
        private readonly SecretManagerServiceClient _secretManager;

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
        /// Creates an instance of the configuration provider for Google Cloud Secret Manager.
        /// </summary>
        /// <param name="options">Options used to configure the configuration provider.</param>
        /// <param name="reloadInterval">Interval used for reloading data.</param>
        public GoogleCloudSecretManagerConfigurationProvider(GoogleCloudSecretManagerOptions options, TimeSpan? reloadInterval)
        {
            _options = options;

            var builder = new SecretManagerServiceClientBuilder();

            _secretManager = builder.Build();

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
        /// Loads (or reloads) the data from Google Cloud Secret Manager.
        /// </summary>
        public override void Load() => LoadAsync().ConfigureAwait(false).GetAwaiter().GetResult();

        /// <summary>
        /// Loads (or reloads) the data from Google Cloud Secret Manager.
        /// </summary>
        private async Task LoadAsync()
        {
            async IAsyncEnumerable<string> GetSecretIdsAsync()
            {
                var request = new ListSecretsRequest
                {
                    ParentAsProjectName = new ProjectName(_options.GoogleCloudProjectId)
                };

                await foreach (var secret in _secretManager.ListSecretsAsync(request).ConfigureAwait(false))
                {
                    yield return secret.SecretName.SecretId;
                }
            }

            async IAsyncEnumerable<KeyValuePair<string, string>> GetSecretsAsync()
            {
                await foreach (var secretId in GetSecretIdsAsync().ConfigureAwait(false))
                {
                    var request = new AccessSecretVersionRequest
                    {
                        SecretVersionName = new SecretVersionName(_options.GoogleCloudProjectId, secretId, "latest")
                    };

                    var response = await _secretManager.AccessSecretVersionAsync(request);

                    string value;

                    try
                    {
                        value = response.Payload.Data.ToStringUtf8();
                    }
                    catch
                    {
                        continue;
                    }

                    yield return new KeyValuePair<string, string>(response.SecretVersionName.SecretId, value);
                }
            }

            var secrets = new Dictionary<string, string>();

            await foreach (var secret in GetSecretsAsync().ConfigureAwait(false))
            {
                secrets.Add(secret.Key, secret.Value);
            }

            Data = secrets;

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
        }
    }
}
