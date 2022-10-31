using Newtonsoft.Json;
using System.Text.Json;

namespace WeatherAPI.DTOs
{
    public class SuggestionsOnAirQualityParticulateMatterDTO
    {
        [JsonProperty("latitude")]
        public double? Latitude { get; set; }

        [JsonProperty("longitude")]
        public double? Longitude { get; set; }

        [JsonProperty("utc_offset_seconds")]
        public int? UTC_Offset_Seconds { get; set; }

        [JsonProperty("timezone")]
        public string? TimeZone { get; set; }

        [JsonProperty("timezone_abbreviation")]
        public string? TimeZone_Abbreviation { get; set; }

        [JsonProperty("daily_suggestion")]
        public AQPMDailySuggestionDTO? Daily_Suggestion { get; set; }
    }

    public class AQPMDailySuggestionDTO
    {
        [JsonProperty("date")]
        public List<string>? Date { get; set; }

        [JsonProperty("midnight_suggestion")]
        public List<string>? Midnight_Suggestion { get; set; }

        [JsonProperty("morning_suggestion")]
        public List<string>? Morning_Suggestion { get; set; }

        [JsonProperty("afternoon_suggestion")]
        public List<string>? Afternoon_Suggestion { get; set; }

        [JsonProperty("evening_suggestion")]
        public List<string>? Evening_Suggestion { get; set; }
    }
}
