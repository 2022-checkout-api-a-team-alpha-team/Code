using WeatherAPI.DTOs;
using WeatherAPI.Models;

namespace WeatherAPI.Services
{
    public interface IAirQualityService
    {
        Task<GetPollenDTO> GetPollenData(string cityName);
        Task<PollenSuggestion> GetPollenSuggestion(string cityName);

        Task<GetAirQualityParticulateMatterResponseDTO> GetAirQualityParticulateMatterByCityName(string cityName);

        Task<SuggestionsOnAirQualityParticulateMatterDTO> SuggestionsOnAirQualityParticulateMatterByCityName(string cityName);
    }
}
