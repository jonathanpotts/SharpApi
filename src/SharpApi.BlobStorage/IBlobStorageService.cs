using System.Collections.Generic;
using System.Threading.Tasks;

namespace SharpApi.BlobStorage
{
    public interface IBlobStorageService
    {
        Task<IEnumerable<string>> ListContainersAsync();
        IBlobContainer GetContainer(string name);
    }
}
