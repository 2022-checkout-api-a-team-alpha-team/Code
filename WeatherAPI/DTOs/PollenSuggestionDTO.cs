namespace WeatherAPI.DTOs
{
    public class PollenSuggestionDTO
    {
        public string Message{ get; }

        public PollenSuggestionDTO(string message)
        {
            Message = message;
        }
    }
}
