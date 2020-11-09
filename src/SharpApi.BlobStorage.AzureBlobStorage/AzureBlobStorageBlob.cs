using Azure.Storage.Blobs;
using System.IO;
using System.Threading.Tasks;

namespace SharpApi.BlobStorage.AzureBlobStorage
{
    public class AzureBlobStorageBlob : IBlob
    {
        private readonly BlobClient _blobClient;

        public AzureBlobStorageBlob(BlobClient blobClient)
        {
            _blobClient = blobClient;
        }

        public async Task<bool> ExistsAsync()
        {
            return await _blobClient.ExistsAsync();
        }

        public async Task<Stream> DownloadAsync()
        {
            return (await _blobClient.DownloadAsync()).Value.Content;
        }

        public async Task UploadAsync(Stream stream)
        {
            await _blobClient.UploadAsync(stream);
        }
    }
}
