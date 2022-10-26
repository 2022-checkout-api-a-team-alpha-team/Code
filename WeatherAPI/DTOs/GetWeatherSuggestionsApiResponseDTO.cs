using Newtonsoft.Json;

namespace WeatherAPI.DTOs
{
    public class GetWeatherSuggestionsApiResponseDTO
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public double Elevation { get; set; }

        public int UtcOffsetSeconds { get; set; }

        public string? Timezone { get; set; }

        public string? TimezoneAbbreviation { get; set; }

        public List<DailyWeatherSuggestionResponseDTO>? WeatherSuggestions { get; set; }
    }

    public class DailyWeatherSuggestionResponseDTO
    {
        public string? Time { get; set; }
        public double? MaxTemperature { get; set; }
        public double? MinTemperature { get; set; }
        public string? Suggestion { get; set; }
    }
}
