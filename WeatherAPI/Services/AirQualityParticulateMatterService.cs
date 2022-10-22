using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Text.Json;
using WeatherAPI.DTOs;
using WeatherAPI.Helper;

namespace WeatherAPI.Services
{
    public class AirQualityParticulateMatterService : IAirQualityParticulateMatterService
    {
        private HttpClient _httpClient;
        
        public AirQualityParticulateMatterService()
        {
            _httpClient = new HttpClient();
        }
        
        public async Task<GetAirQualityParticulateMatterResponseDTO> GetAirQualityParticulateMatter(double lat, double lon)
        {
            var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
                AllowTrailingCommas = true,
                DictionaryKeyPolicy = JsonNamingPolicy.CamelCase
            };
            var result = await _httpClient.GetFromJsonAsync<GetAirQualityParticulateMatterResponseDTO>(ConstantsHelper.AQ_PM_URL.Replace("[lat]", lat.ToString().Trim()).Replace("[lon]", lon.ToString().Trim()), options);
            return result;
        }

        public async Task<GetAirQualityParticulateMatterResponseDTO> GetAirQualityParticulateMatterByCityName(string cityName)
        {
            var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
                AllowTrailingCommas = true,
                DictionaryKeyPolicy = JsonNamingPolicy.CamelCase
            };
            var GeoCoordOfCity = await _httpClient.GetFromJsonAsync<GetGeoCoordResponseDTO>(ConstantsHelper.GEO_API_URL.Replace("[city]", cityName), options);
            double lat = GeoCoordOfCity.Results.ToList()[0].Latitude;
            double lon = GeoCoordOfCity.Results.ToList()[0].Longitude;
            var result = await _httpClient.GetFromJsonAsync<GetAirQualityParticulateMatterResponseDTO>(ConstantsHelper.AQ_PM_URL.Replace("[lat]", lat.ToString().Trim()).Replace("[lon]", lon.ToString().Trim()), options);
            return result;
        }
    }
}
