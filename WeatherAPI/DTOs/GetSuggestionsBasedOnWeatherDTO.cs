using Newtonsoft.Json;

namespace WeatherAPI.DTOs
{
    public class GetSuggestionsBasedOnWeatherDTO
    {
        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [JsonProperty("longitude")]
        public double Longitude { get; set; }

        [JsonProperty("generationtime_ms")]
        public double Generationtime_Ms { get; set; }

        [JsonProperty("utc_offset_seconds")]
        public int Utc_Offset_Seconds { get; set; }

        [JsonProperty("timezone")]
        public string? Timezone { get; set; }

        [JsonProperty("timezone_abbreviation")]
        public string? Timezone_Abbreviation { get; set; }

        [JsonProperty("elevation")]
        public double Elevation { get; set; }

        [JsonProperty("daily_units")]
        public GetWeatherSuggestionsDailyUnitsDTO? Daily_Units { get; set; }

        [JsonProperty("daily")]
        public GetWeatherSuggestionsDailyDTO? Daily { get; set; }
    }

    public class GetWeatherSuggestionsDailyDTO
    {
        [JsonProperty("time")]
        public List<string>? Time { get; set; }

        [JsonProperty("weathercode")]
        public List<int>? Weathercode { get; set; }

        [JsonProperty("temperature_2m_max")]
        public List<double>? Temperature_2m_Max { get; set; }

        [JsonProperty("temperature_2m_min")]
        public List<double>? Temperature_2m_Min { get; set; }
    }

    public class GetWeatherSuggestionsDailyUnitsDTO
    {
        [JsonProperty("time")]
        public string? Time { get; set; }

        [JsonProperty("weathercode")]
        public string? Weathercode { get; set; }

        [JsonProperty("temperature_2m_max")]
        public string? Temperature_2m_Max { get; set; }

        [JsonProperty("temperature_2m_min")]
        public string? Temperature_2m_Min { get; set; }
    }
}
