using Azure.Core.Pipeline;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SharpApi.BlobStorage.AzureBlobStorage
{
    public class AzureBlobStorageService : IBlobStorageService
    {
        private readonly BlobServiceClient _blobServiceClient;

        public AzureBlobStorageService(IOptions<AzureBlobStorageOptions> options, HttpClient httpClient)
        {
            _blobServiceClient = new BlobServiceClient(options.Value.ConnectionString, new BlobClientOptions { Transport = new HttpClientTransport(httpClient) });
        }

        public async Task<IEnumerable<string>> ListContainersAsync()
        {
            var containersEnumerator = _blobServiceClient.GetBlobContainersAsync().GetAsyncEnumerator();

            var containers = new List<string>();

            try
            {
                while (await containersEnumerator.MoveNextAsync())
                {
                    var container = containersEnumerator.Current;
                    containers.Add(container.Name);
                }
            }
            finally
            {
                await containersEnumerator.DisposeAsync();
            }

            return containers;
        }

        public IBlobContainer GetContainer(string name)
        {
            return new AzureBlobStorageBlobContainer(_blobServiceClient.GetBlobContainerClient(name));
        }
    }
}
