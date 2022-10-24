using WeatherAPI.DTOs;
using WeatherAPI.Models;

namespace WeatherAPI.Services
{
    public interface IWeatherService
    {
        Task<GetHourlyTemperatureResponseDTO> GetHourlyTemperatureByLatitudeAndLongitude(double latitude, double longitude);
        Task<GetHourlyTemperatureResponseDTO> GetHourlyTemperatureByCity(string city);
        Task<List<FeelsLikeTemperatureForecast>> GetHourlyFeelsLikeTemperatureByCity(string city);
    }
}
