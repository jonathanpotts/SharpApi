using System.Text.Json;
using System.Threading.Tasks;

namespace SharpApi
{
    public abstract class ApiEndpoint
    {
        public ApiEndpoint()
        {
        }

        public abstract Task<ApiResult> RunAsync(string input);
    }

    public abstract class ApiEndpoint<T> : ApiEndpoint
    {
        public abstract Task<ApiResult> RunAsync(T input);

        public override Task<ApiResult> RunAsync(string input) => RunAsync(JsonSerializer.Deserialize<T>(input));
    }
}
