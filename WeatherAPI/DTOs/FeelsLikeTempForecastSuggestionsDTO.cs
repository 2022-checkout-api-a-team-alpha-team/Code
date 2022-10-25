namespace WeatherAPI.DTOs
{
    public class FeelsLikeTempForecastSuggestionsDTO
    {
        public string? Date { get; set; }

        public double? Temperature { get; set; }

        public double? FeelsLikeTemperature { get; set; }

        public string? Suggestion { get; set; }
    }
}
