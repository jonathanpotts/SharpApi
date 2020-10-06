using SharpApi.Example.Shared;
using System.Net;
using System.Threading.Tasks;

namespace SharpApi.Example
{
    [ApiEndpoint("/weather")]
    public class WeatherEndpoint : ApiEndpoint<WeatherData>
    {
        public override Task<ApiResult> RunAsync(WeatherData input)
        {
            return new Task<ApiResult>(() => new StatusCodeResult(HttpStatusCode.OK));
        }
    }
}
