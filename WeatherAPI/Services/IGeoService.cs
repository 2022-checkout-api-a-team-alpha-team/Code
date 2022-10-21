using WeatherAPI.DTOs;

namespace WeatherAPI.Services
{
    public interface IGeoService
    {
        Task<GetGeoCoordResponseDTO> GetGeoCoordinatesByCityName(string cityName);
    }
}
