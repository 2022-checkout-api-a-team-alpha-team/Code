using WeatherAPI.DTOs;

namespace WeatherAPI.Services
{
    public interface IAirQualityParticulateMatterService
    {
        Task<GetAirQualityParticulateMatterResponseDTO> GetAirQualityParticulateMatter(double lat, double lon);
        Task<GetAirQualityParticulateMatterResponseDTO> GetAirQualityParticulateMatterByCityName(string cityName);
    }
}
