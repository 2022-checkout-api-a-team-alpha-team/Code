using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using System.Text.Json;
using WeatherAPI.DTOs;
using WeatherAPI.Helper;

namespace WeatherAPI.Services
{
    public class GeoService: IGeoService
    {
        private HttpClient _httpClient;
        public GeoService()
        {
            _httpClient = new HttpClient();
        }
        public async Task<GetGeoCoordResponseDTO> GetGeoCoordinatesByCityName(string cityName)
        {
            var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
                AllowTrailingCommas = true,
                DictionaryKeyPolicy = JsonNamingPolicy.CamelCase
            }; 
            var result = await _httpClient.GetFromJsonAsync<GetGeoCoordResponseDTO>(ConstantsHelper.GEO_API_URL.Replace("[city]", cityName), options);
            return result;
        }
    }
}
