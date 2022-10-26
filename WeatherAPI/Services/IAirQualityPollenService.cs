using WeatherAPI.DTOs;
using WeatherAPI.Models;

namespace WeatherAPI.Services
{
    public interface IAirQualityPollenService
    {
        Task<GetPollenDTO> GetPollenData(string cityName);
        Task<PollenSuggestion> GetPollenSuggestion(string cityName);
    }
}
