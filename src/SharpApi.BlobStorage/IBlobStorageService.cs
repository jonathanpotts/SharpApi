using System.Collections.Generic;

namespace SharpApi.BlobStorage
{
    public interface IBlobStorageService
    {
        public IAsyncEnumerable<string> ListContainersAsync();
        public IBlobContainer GetContainer(string name);
    }
}
