using WeatherAPI.DTOs;
using WeatherAPI.DTOs.Pollen;

namespace WeatherAPI.Services
{
    public interface IAirQualityService
    {
        Task<GetPollenDTO> GetPollenData(string cityName);
        Task<PollenAggregatedDTO> GetPollenSuggestion(string cityName);

        Task<GetAirQualityParticulateMatterResponseDTO> GetAirQualityParticulateMatterByCityName(string cityName);

        Task<SuggestionsOnAirQualityParticulateMatterDTO> SuggestionsOnAirQualityParticulateMatterByCityName(string cityName);
    }
}
