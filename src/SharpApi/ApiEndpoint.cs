using System.Threading.Tasks;

namespace SharpApi
{
    public abstract class ApiEndpoint
    {
        public ApiEndpoint()
        {
        }

        public abstract Task<ApiResult> RunAsync(ApiRequest request);
    }

    public abstract class ApiEndpoint<T> : ApiEndpoint
    {
        public abstract Task<ApiResult> RunAsync(ApiRequest<T> request);

        public override Task<ApiResult> RunAsync(ApiRequest request) => RunAsync(new ApiRequest<T>(request));
    }
}
