using Newtonsoft.Json;

namespace WeatherAPI.DTOs
{
    public class GetHourlyFeelsLikeTempResponseDTO
    {
        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [JsonProperty("longitude")]
        public double Longitude { get; set; }

        [JsonProperty("generationtime_ms")]
        public double GenerationtimeMs { get; set; }

        [JsonProperty("utc_offset_seconds")]
        public int UtcOffsetSeconds { get; set; }

        [JsonProperty("timezone")]
        public string? Timezone { get; set; }

        [JsonProperty("timezone_abbreviation")]
        public string? TimezoneAbbreviation { get; set; }

        [JsonProperty("elevation")]
        public double Elevation { get; set; }

        [JsonProperty("hourly_units")]
        public FeelsLikeTemperatureHourlyUnitsDTO? Hourly_Units { get; set; }

        [JsonProperty("hourly")]
        public FeelsLikeTemperatureHourlyDTO? Hourly { get; set; }
    }

    public class FeelsLikeTemperatureHourlyDTO
    {
        [JsonProperty("time")]
        public List<string>? Time { get; set; }

        [JsonProperty("temperature_2m")]
        public List<double>? Temperature_2m { get; set; }

        [JsonProperty("apparent_temperature")]
        public List<double>? Apparent_Temperature { get; set; }
    }

    public class FeelsLikeTemperatureHourlyUnitsDTO
    {
        [JsonProperty("time")]
        public string? Time { get; set; }

        [JsonProperty("temperature_2m")]
        public string? Temperature_2m { get; set; }

        [JsonProperty("apparent_temperature")]
        public string? Apparent_Temperature { get; set; }
    }
}