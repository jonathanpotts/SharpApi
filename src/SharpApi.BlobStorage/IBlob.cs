using System.IO;
using System.Threading.Tasks;

namespace SharpApi.BlobStorage
{
    public interface IBlob
    {
        Task<bool> ExistsAsync();
        Task<Stream> DownloadAsync();
        Task UploadAsync(Stream stream);
    }
}
