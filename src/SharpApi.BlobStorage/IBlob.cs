using System.IO;
using System.Threading.Tasks;

namespace SharpApi.BlobStorage
{
    public interface IBlob
    {
        public Task<bool> ExistsAsync();
        public Task<Stream> DownloadAsync();
        public Task UploadAsync(Stream stream);
    }
}
