using SharpApi.Example.Shared;
using System;
using System.Threading.Tasks;

namespace SharpApi.Example
{
    [ApiEndpoint("weather")]
    public class WeatherEndpoint : ApiEndpoint
    {
        public override Task<ApiResult> RunAsync(ApiRequest request)
        {
            var random = new Random(unchecked((int)DateTime.Now.Date.Ticks));

            var low = random.Next(60, 72);
            var high = random.Next(78, 90);
            var current = random.Next(low, high + 1);

            var sunriseMins = random.Next(330, 436);
            var sunsetMins = random.Next(990, 1156);

            var sunrise = DateTime.Today.AddMinutes(sunriseMins);
            var sunset = DateTime.Today.AddMinutes(sunsetMins);

            var weather = new WeatherData
            {
                LowTemperature = low,
                HighTemperature = high,
                CurrentTemperature = current,
                Sunrise = sunrise,
                Sunset = sunset
            };

            return Task.FromResult<ApiResult>(new JsonResult(weather));
        }
    }
}
