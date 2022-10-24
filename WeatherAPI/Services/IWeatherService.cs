using WeatherAPI.DTOs;

namespace WeatherAPI.Services
{
    public interface IWeatherService
    {
        Task<GetHourlyTemperatureResponseDTO> GetHourlyTemperatureByLatitudeAndLongitude(double latitude, double longitude);
        Task<GetHourlyTemperatureResponseDTO> GetHourlyTemperatureByCity(string city);
    }
}
