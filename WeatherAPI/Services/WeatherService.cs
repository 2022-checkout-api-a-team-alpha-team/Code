using System;
using System.Collections.Generic;
using System.Text.Json;
using WeatherAPI.DTOs;
using WeatherAPI.Helper;
using WeatherAPI.Models;

namespace WeatherAPI.Services
{
    public class WeatherService : IWeatherService
    {
        private HttpClient _httpClient;
        private readonly IGeoService _geoService;
        private const int FEELS_LIKE_TEMP_NO_OF_HOURS = 24;
        FeelsLikeTemperatureForecast feelsLikeTemp;
        List<FeelsLikeTemperatureForecast> feelsLikeTempResult = new();

        private readonly JsonSerializerOptions options = new()
        {
            PropertyNameCaseInsensitive = true,
            AllowTrailingCommas = true,
            DictionaryKeyPolicy = JsonNamingPolicy.CamelCase
        };

        //Dressing suggestions for Feels Like Temperature
        private static readonly string[] feelLikeTemperatureSuggestions = new[]
        {
            "You'll feel colder than outside - Better to wear a jumper/ a jacket.",
            "You'll feel hotter than outside - Better to wear light cotton clothes.",
            "You'll feel just the right temperature as in air when you go out. Wear as you like."
        };

        public WeatherService(IGeoService geoService)
        {
            _httpClient = new HttpClient();
            _geoService = geoService;
        }
        public async Task<GetHourlyTemperatureResponseDTO> GetHourlyTemperatureByLatitudeAndLongitude(double latitude, double longitude)
        {
            var result = await _httpClient.GetFromJsonAsync<GetHourlyTemperatureResponseDTO>(ConstantsHelper.WEATHER_API_URL.Replace("[latitude]",latitude.ToString().Trim()).Replace("[longitude]", longitude.ToString().Trim()), options);
            return result!;
        }

        public async Task<GetHourlyTemperatureResponseDTO> GetHourlyTemperatureByCity(string cityName)
        {
            var GeoCoordinates = await _geoService.GetGeoCoordinatesByCityName(cityName);
            double latitude = GeoCoordinates!.Results.ToList()[0].Latitude;
            double longitude = GeoCoordinates.Results.ToList()[0].Longitude;

            var result = await _httpClient.GetFromJsonAsync<GetHourlyTemperatureResponseDTO>(ConstantsHelper.WEATHER_API_URL.Replace("[latitude]", latitude.ToString().Trim()).Replace("[longitude]", longitude.ToString().Trim()), options);
            return result!;
        }

        public async Task<List<FeelsLikeTemperatureForecast>> GetHourlyFeelsLikeTemperatureByCity(string cityName)
        {  
            //Getting latitude and longitude values for the city name 
            var GeoCoordinates = _geoService.GetGeoCoordinatesByCityName(cityName);
            var latitude = GeoCoordinates.Result.Results.ToList()[0].Latitude;
            var longitude = GeoCoordinates.Result.Results.ToList()[0].Longitude;

            var result = await _httpClient.GetFromJsonAsync<GetHourlyFeelsLikeTemperatureResponseDTO>(ConstantsHelper.WEATHER_API_FEELS_LIKE_TEMPERATURE_URL.Replace("[latitude]", latitude.ToString().Trim()).Replace("[longitude]", longitude.ToString().Trim()), options);

            if (result != null)
            {
                if (result.Hourly != null)
                {
                    feelsLikeTempResult = GetDressingSuggestions(result);
                }
            }
            return feelsLikeTempResult;           
        }

        public List<FeelsLikeTemperatureForecast> GetDressingSuggestions(GetHourlyFeelsLikeTemperatureResponseDTO result)
        {
            List<string>? Date = result.Hourly.Time.GetRange(0, FEELS_LIKE_TEMP_NO_OF_HOURS);
            List<double>? Temperature = result.Hourly.Temperature_2m.GetRange(0, FEELS_LIKE_TEMP_NO_OF_HOURS);
            List<double>? FeelsLikeTemperature = result.Hourly.Apparent_Temperature.GetRange(0, FEELS_LIKE_TEMP_NO_OF_HOURS);

            //Logic to assign suggestions in response
            for (int i = 0; i < Date.Count; i++)
            {
                feelsLikeTemp = new();
                feelsLikeTemp.Date = Date[i];
                feelsLikeTemp.Temperature = Temperature[i];
                feelsLikeTemp.FeelsLikeTemperature = FeelsLikeTemperature[i];
                if (FeelsLikeTemperature[i] < Temperature[i])
                {
                    feelsLikeTemp.Suggestion = feelLikeTemperatureSuggestions[0];
                }
                else if (FeelsLikeTemperature[i] > Temperature[i])
                {
                    feelsLikeTemp.Suggestion = feelLikeTemperatureSuggestions[1];
                }
                else
                {
                    feelsLikeTemp.Suggestion = feelLikeTemperatureSuggestions[2];
                }
                feelsLikeTempResult.Add(feelsLikeTemp);
            }
            return feelsLikeTempResult;
        }
    }
}
