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

        public async Task<IEnumerable<string>> ListBlobsAsync()
        {
            var blobsEnumerator = _blobContainerClient.GetBlobsAsync().ConfigureAwait(false).GetAsyncEnumerator();

            var blobs = new List<string>();

            try
            {
                while (await blobsEnumerator.MoveNextAsync())
                {
                    var blob = blobsEnumerator.Current;
                    blobs.Add(blob.Name);
                }
            }
            finally
            {
                await blobsEnumerator.DisposeAsync();
            }

            return blobs;
        }

        public IBlob GetBlob(string name)
        {
            return new AzureBlobStorageBlob(_blobContainerClient.GetBlobClient(name));
        }
    }
}
