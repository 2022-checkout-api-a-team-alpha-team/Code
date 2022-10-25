using Newtonsoft.Json;

namespace WeatherAPI.DTOs
{
    public class GetSuggestionsBasedOnWeatherDTO
    {
        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [JsonProperty("longitude")]
        public double Longitude { get; set; }

        [JsonProperty("elevation")]
        public double Elevation { get; set; }

        [JsonProperty("generationtime_ms")]
        public double GenerationtimeMs { get; set; }

        [JsonProperty("utc_offset_seconds")]
        public int UtcOffsetSeconds { get; set; }

        [JsonProperty("timezone")]
        public string? Timezone { get; set; }

        [JsonProperty("timezone_abbreviation")]
        public string? TimezoneAbbreviation { get; set; }

        [JsonProperty("hourly")]
        public CurrentWeatherHourlyDTO Hourly { get; set; }

        [JsonProperty("hourly_units")]
        public CurrentWeatherHourlyUnitsDTO HourlyUnits { get; set; }

        [JsonProperty("current_weather")]
        public CurrentWeather? CurrentWeather { get; set; }
    }

    public class CurrentWeather
    {
        [JsonProperty("time")]
        public string? Time { get; set; }

        [JsonProperty("temperature")]
        public double Temperature { get; set; }

        [JsonProperty("weathercode")]
        public int Weathercode { get; set; }

        [JsonProperty("windspeed")]
        public double Windspeed { get; set; }

        [JsonProperty("winddirection")]
        public int Winddirection { get; set; }
    }

    public class CurrentWeatherHourlyDTO
    {
        [JsonProperty("time")]
        public List<string>? Time { get; set; }

        [JsonProperty("temperature_2m")]
        public List<double>? Temperature2m { get; set; }
    }

    public class CurrentWeatherHourlyUnitsDTO
    {
        [JsonProperty("temperature_2m")]
        public string? Temperature2m { get; set; }
    }
}
