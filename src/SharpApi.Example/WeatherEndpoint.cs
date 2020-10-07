using System.Net;
using System.Threading.Tasks;

namespace SharpApi.Example
{
    [ApiEndpoint("/weather")]
    public class WeatherEndpoint : ApiEndpoint
    {
        public override Task<ApiResult> RunAsync(ApiRequest request)
        {
            return Task.FromResult<ApiResult>(new StatusCodeResult(HttpStatusCode.OK));
        }
    }
}
