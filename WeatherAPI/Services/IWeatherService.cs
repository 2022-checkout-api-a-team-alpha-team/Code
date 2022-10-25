using WeatherAPI.DTOs;

namespace WeatherAPI.Services
{
    public interface IWeatherService
    {
        Task<GetHourlyTemperatureResponseDTO> GetHourlyTemperatureByLatitudeAndLongitude(double latitude, double longitude);
        Task<List<HourlyTempForeCastAndSuggestions>> GetHourlyTemperatureByCity(string cityName);
        Task<List<FeelsLikeTempForecastSuggestionsDTO>> GetHourlyFeelsLikeTemperatureByCity(string city);
        Task<string> GetSuggestionsBasedOnCurrentWeather(string cityName);
    }
}
