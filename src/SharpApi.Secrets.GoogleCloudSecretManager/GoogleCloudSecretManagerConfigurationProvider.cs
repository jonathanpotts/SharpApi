using Google.Api.Gax.ResourceNames;
using Google.Cloud.SecretManager.V1;
using Microsoft.Extensions.Configuration;
using SharpApi.GoogleCloud;
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
        private readonly GoogleCloudOptions _options;

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
        public GoogleCloudSecretManagerConfigurationProvider(GoogleCloudOptions options, TimeSpan? reloadInterval)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));

            if (!string.IsNullOrEmpty(_options.CredentialsJsonFilePath))
            {
                var builder = new SecretManagerServiceClientBuilder
                {
                    CredentialsPath = _options.CredentialsJsonFilePath
                };

                _secretManager = builder.Build();
            }
            else
            {
                _secretManager = SecretManagerServiceClient.Create();
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
        /// Loads (or reloads) the data from Google Cloud Secret Manager.
        /// </summary>
        public override void Load() => LoadAsync().ConfigureAwait(false).GetAwaiter().GetResult();

        /// <summary>
        /// Loads (or reloads) the data from Google Cloud Secret Manager.
        /// </summary>
        private async Task LoadAsync()
        {
            async Task<IEnumerable<string>> GetSecretIdsAsync()
            {
                var request = new ListSecretsRequest
                {
                    ParentAsProjectName = new ProjectName(_options.ProjectId)
                };

                var secretIds = new List<string>();

                var secretsEnumerator = _secretManager.ListSecretsAsync(request).ConfigureAwait(false).GetAsyncEnumerator();

                try
                {
                    while (await secretsEnumerator.MoveNextAsync())
                    {
                        var secret = secretsEnumerator.Current;
                        secretIds.Add(secret.SecretName.SecretId);
                    }
                }
                finally
                {
                    await secretsEnumerator.DisposeAsync();
                }

                return secretIds;
            }

            async Task<Dictionary<string, string>> GetSecretsAsync()
            {
                var secrets = new Dictionary<string, string>();

                foreach (var secretId in await GetSecretIdsAsync())
                {
                    var request = new AccessSecretVersionRequest
                    {
                        SecretVersionName = new SecretVersionName(_options.ProjectId, secretId, "latest")
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

                    secrets.Add(response.SecretVersionName.SecretId, value);
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
        }
    }
}
