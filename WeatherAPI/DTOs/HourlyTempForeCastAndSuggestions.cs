namespace WeatherAPI.DTOs
{
    public class HourlyTempForeCastAndSuggestions
    {
        public int Day { get; set; }
        public string? Date { get; set; }
        public double? AverargeTemperature { get; set; }

        public string? Suggestion { get; set; }
    }
}
