using System.Collections.Generic;
using System.Threading.Tasks;

namespace SharpApi.BlobStorage
{
    public interface IBlobContainer
    {
        Task<bool> ExistsAsync();
        Task<IEnumerable<string>> ListBlobsAsync();
        IBlob GetBlob(string name);
    }
}
