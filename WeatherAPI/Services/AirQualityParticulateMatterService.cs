using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Text.Json;
using WeatherAPI.DTOs;
using WeatherAPI.Helper;

namespace WeatherAPI.Services
{
    public class AirQualityParticulateMatterService : IAirQualityParticulateMatterService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions options = new()
        {
            PropertyNameCaseInsensitive = true,
            AllowTrailingCommas = true,
            DictionaryKeyPolicy = JsonNamingPolicy.CamelCase
        };
        
        public AirQualityParticulateMatterService()
        {
            _httpClient = new HttpClient();
        }
        
        public async Task<GetAirQualityParticulateMatterResponseDTO> GetAirQualityParticulateMatter(double lat, double lon)
        {
            var result = await _httpClient.GetFromJsonAsync<GetAirQualityParticulateMatterResponseDTO>(ConstantsHelper.AQ_PM_URL.Replace("[lat]", lat.ToString().Trim()).Replace("[lon]", lon.ToString().Trim()), options);
            return result;
        }

        public async Task<GetAirQualityParticulateMatterResponseDTO> GetAirQualityParticulateMatterByCityName(string cityName)
        {
            GeoService _geoService = new();
            var GeoCoordOfCity = _geoService.GetGeoCoordinatesByCityName(cityName);
            double lat = GeoCoordOfCity.Result.Results.ToList()[0].Latitude;
            double lon = GeoCoordOfCity.Result.Results.ToList()[0].Longitude;

            var result = await _httpClient.GetFromJsonAsync<GetAirQualityParticulateMatterResponseDTO>(ConstantsHelper.AQ_PM_URL.Replace("[lat]", lat.ToString().Trim()).Replace("[lon]", lon.ToString().Trim()), options);
            return result;
        }
    }
}
