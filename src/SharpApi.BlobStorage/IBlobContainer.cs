using System.Collections.Generic;
using System.Threading.Tasks;

namespace SharpApi.BlobStorage
{
    public interface IBlobContainer
    {
        public Task<bool> ExistsAsync();
        public IAsyncEnumerable<string> ListBlobsAsync();
        public IBlob GetBlob(string name);
    }
}
