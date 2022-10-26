namespace WeatherAPI.Models
{
    public class PollenSuggestion
    {
        public string Message { get; }

        public PollenSuggestion(string message)
        {
            Message = message;
        }
    }
}
