using WeatherAPI.DTOs;
using WeatherAPI.Models;

namespace WeatherAPI.Services
{
    public interface IWeatherService
    {
        Task<GetHourlyTemperatureResponseDTO> GetHourlyTemperatureByLatitudeAndLongitude(double latitude, double longitude);
        Task<List<HourlyTempForeCastAndSuggestions>> GetHourlyTemperatureByCity(string cityName);
        Task<List<FeelsLikeTemperatureForecast>> GetHourlyFeelsLikeTemperatureByCity(string city);
        Task<string> GetSuggestionsBasedOnCurrentWeather(string cityName);
    }
}
