using Azure.Storage.Blobs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SharpApi.BlobStorage.AzureBlobStorage
{
    public class AzureBlobStorageBlobContainer : IBlobContainer
    {
        private readonly BlobContainerClient _blobContainerClient;

        public AzureBlobStorageBlobContainer(BlobContainerClient blobContainerClient)
        {
            _blobContainerClient = blobContainerClient;
        }

        public async Task<bool> ExistsAsync()
        {
            return await _blobContainerClient.ExistsAsync();
        }

        public async IAsyncEnumerable<string> ListBlobsAsync()
        {
            await foreach (var blob in _blobContainerClient.GetBlobsAsync())
            {
                yield return blob.Name;
            }
        }

        public IBlob GetBlob(string name)
        {
            return new AzureBlobStorageBlob(_blobContainerClient.GetBlobClient(name));
        }
    }
}
