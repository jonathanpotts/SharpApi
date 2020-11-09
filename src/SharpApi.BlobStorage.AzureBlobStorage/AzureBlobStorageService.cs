using Azure.Core.Pipeline;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Net.Http;

namespace SharpApi.BlobStorage.AzureBlobStorage
{
    public class AzureBlobStorageService : IBlobStorageService
    {
        private readonly BlobServiceClient _blobServiceClient;

        public AzureBlobStorageService(IOptions<AzureBlobStorageOptions> options, HttpClient httpClient)
        {
            _blobServiceClient = new BlobServiceClient(options.Value.ConnectionString, new BlobClientOptions { Transport = new HttpClientTransport(httpClient) });
        }

        public async IAsyncEnumerable<string> ListContainersAsync()
        {
            await foreach (var container in _blobServiceClient.GetBlobContainersAsync())
            {
                yield return container.Name;
            }
        }

        public IBlobContainer GetContainer(string name)
        {
            return new AzureBlobStorageBlobContainer(_blobServiceClient.GetBlobContainerClient(name));
        }
    }
}
