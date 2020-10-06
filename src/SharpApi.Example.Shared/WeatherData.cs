using System;

namespace SharpApi.Example.Shared
{
    public class WeatherData
    {
        public int CurrentTemperature { get; set; }
        public int HighTemperature { get; set; }
        public int LowTemperature { get; set; }
        public DateTime Sunrise { get; set; }
        public DateTime Sunset { get; set; }
    }
}
