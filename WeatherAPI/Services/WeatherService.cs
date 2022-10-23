using System.Text.Json;
using WeatherAPI.DTOs;
using WeatherAPI.Helper;

namespace WeatherAPI.Services
{
    public class WeatherService : IWeatherService
    {
        private HttpClient _httpClient;
        public WeatherService()
        {
            _httpClient = new HttpClient();
        }
        public async Task<GetHourlyTemperatureResponseDTO> GetHourlyTemperatureByLatitudeAndLongitude(double latitude, double longitude)
        {
            var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
                AllowTrailingCommas = true,
                DictionaryKeyPolicy = JsonNamingPolicy.CamelCase
            };
            var result = await _httpClient.GetFromJsonAsync<GetHourlyTemperatureResponseDTO>(ConstantsHelper.WEATHER_API_URL.Replace("[latitude]",latitude.ToString().Trim()).Replace("[longitude]", longitude.ToString().Trim()), options);
            return result!;
        }

        public async Task<GetHourlyTemperatureResponseDTO> GetHourlyTemperatureByCity(string cityName)
        {
            var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
                AllowTrailingCommas = true,
                DictionaryKeyPolicy = JsonNamingPolicy.CamelCase
            };

            var GeoCoordinates = await _httpClient.GetFromJsonAsync<GetGeoCoordResponseDTO>(ConstantsHelper.GEO_API_URL.Replace("[city]", cityName), options);
            double latitude = GeoCoordinates!.Results.ToList()[0].Latitude;
            double longitude = GeoCoordinates.Results.ToList()[0].Longitude;

            var result = await _httpClient.GetFromJsonAsync<GetHourlyTemperatureResponseDTO>(ConstantsHelper.WEATHER_API_URL.Replace("[latitude]", latitude.ToString().Trim()).Replace("[longitude]", longitude.ToString().Trim()), options);
            return result!;
        }
    }
}
