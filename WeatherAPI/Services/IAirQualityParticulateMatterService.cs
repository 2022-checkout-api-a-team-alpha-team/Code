using WeatherAPI.DTOs;

namespace WeatherAPI.Services
{
    public interface IAirQualityParticulateMatterService
    {
        Task<GetAirQualityParticulateMatterResponseDTO> GetAirQualityParticulateMatterByCityName(string cityName);

        Task<SuggestionsOnAirQualityParticulateMatterDTO> SuggestionsOnAirQualityParticulateMatterByCityName(string cityName);
    }
}
