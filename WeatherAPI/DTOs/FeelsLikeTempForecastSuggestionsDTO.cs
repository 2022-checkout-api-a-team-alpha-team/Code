namespace WeatherAPI.DTOs
{
    public class FeelsLikeTempForecastSuggestionsDTO
    {
        public string? Date { get; set; }

        public string? Time_24_Hour_Clock { get; set; }

        public double? Temperature { get; set; }

        public double? FeelsLikeTemperature { get; set; }

        public string? Suggestion { get; set; }
    }
}
