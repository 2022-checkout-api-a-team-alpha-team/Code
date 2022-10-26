using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.Json;
using WeatherAPI.DTOs;
using WeatherAPI.Helper;

namespace WeatherAPI.Services
{
    public class WeatherService : IWeatherService
    {
        private HttpClient _httpClient;
        private GeoService _geoService;
        private const int NO_OF_HOURS_IN_DAY = 24;
        FeelsLikeTempForecastSuggestionsDTO? feelsLikeTemp;
        List<FeelsLikeTempForecastSuggestionsDTO?> feelsLikeTempResult;
        List<HourlyTempForeCastAndSuggestionsDTO> hourlyTemperatureSuggestions;


        public WeatherService()
        {
            _httpClient = new HttpClient();
            _geoService = new GeoService();

            feelsLikeTempResult = new();
            hourlyTemperatureSuggestions = new();
        }

        private readonly JsonSerializerOptions options = new()
        {
            PropertyNameCaseInsensitive = true,
            AllowTrailingCommas = true,
            DictionaryKeyPolicy = JsonNamingPolicy.CamelCase
        };

        public async Task<GetHourlyTemperatureResponseDTO> GetHourlyTemperatureByLatitudeAndLongitude(double latitude, double longitude)
        {
            var result = await _httpClient.GetFromJsonAsync<GetHourlyTemperatureResponseDTO>(ConstantsHelper.WEATHER_API_URL.Replace("[latitude]",latitude.ToString().Trim()).Replace("[longitude]", longitude.ToString().Trim()), options);
            return result!;
        }

        public async Task<string> GetSuggestionsBasedOnCurrentWeather(string cityName)
        {
            var geoCoordinates = await _geoService.GetGeoCoordinatesByCityName(cityName);
            double latitude = geoCoordinates.Results.ToList()[0].Latitude;
            double longitude = geoCoordinates.Results.ToList()[0].Longitude;
            var result = await _httpClient.GetFromJsonAsync<GetSuggestionsBasedOnWeatherDTO>(ConstantsHelper.WEATHER_API_GET_SUGGESTIONS_BASED_ON_CURRENT_WEATHER_URL.Replace("[latitude]", latitude.ToString().Trim()).Replace("[longitude]", longitude.ToString().Trim()), options);

            throw new NotImplementedException();
            return "";
        }

        public async Task<List<HourlyTempForeCastAndSuggestionsDTO>> GetHourlyTemperatureByCity(string cityName)
        {
            var GeoCoordinates = await _geoService.GetGeoCoordinatesByCityName(cityName);
            double latitude = GeoCoordinates!.Results.ToList()[0].Latitude;
            double longitude = GeoCoordinates.Results.ToList()[0].Longitude;

            var result = await _httpClient.GetFromJsonAsync<GetHourlyTemperatureResponseDTO>(ConstantsHelper.WEATHER_API_URL.Replace("[latitude]", latitude.ToString().Trim()).Replace("[longitude]", longitude.ToString().Trim()), options);
            if (result != null)
            {
                if(result.Hourly != null)
                    hourlyTemperatureSuggestions = GetSuggestionsForHourlyTemperature(result);
            }
            return hourlyTemperatureSuggestions;
        }

        public async Task<List<FeelsLikeTempForecastSuggestionsDTO>> GetHourlyFeelsLikeTemperatureByCity(string cityName)
        {  
            //Getting latitude and longitude values for the city name 
            var GeoCoordinates = _geoService.GetGeoCoordinatesByCityName(cityName);
            var latitude = GeoCoordinates.Result.Results.ToList()[0].Latitude;
            var longitude = GeoCoordinates.Result.Results.ToList()[0].Longitude;

            var result = await _httpClient.GetFromJsonAsync<GetHourlyFeelsLikeTempResponseDTO>(ConstantsHelper.WEATHER_API_FEELS_LIKE_TEMPERATURE_URL.Replace("[latitude]", latitude.ToString().Trim()).Replace("[longitude]", longitude.ToString().Trim()), options);

            if (result != null)
            {
                if (result.Hourly != null)
                    feelsLikeTempResult = GetDressingSuggestions(result);
            }
            return feelsLikeTempResult;           
        }

        public List<FeelsLikeTempForecastSuggestionsDTO> GetDressingSuggestions(GetHourlyFeelsLikeTempResponseDTO result)
        {
            List<string> Date = result.Hourly!.Time!.GetRange(0, NO_OF_HOURS_IN_DAY);
            List<double>? Temperature = result.Hourly.Temperature_2m!.GetRange(0, NO_OF_HOURS_IN_DAY);
            List<double>? FeelsLikeTemperature = result.Hourly.Apparent_Temperature!.GetRange(0, NO_OF_HOURS_IN_DAY);

            //Logic to assign suggestions in response
            for (int i = 0; i < Date.Count; i++)
            {
                feelsLikeTemp = new();
                feelsLikeTemp.Date = Date[i];
                feelsLikeTemp.Temperature = Temperature[i];
                feelsLikeTemp.FeelsLikeTemperature = FeelsLikeTemperature[i];
                if (FeelsLikeTemperature[i] < Temperature[i])
                    feelsLikeTemp.Suggestion = FeelsLikeTemperatureSuggestions.FEELS_LIKE_TEMP_COLD;                
                else if (FeelsLikeTemperature[i] > Temperature[i])
                    feelsLikeTemp.Suggestion = FeelsLikeTemperatureSuggestions.FEELS_LIKE_TEMP_HOT;
                else
                    feelsLikeTemp.Suggestion = FeelsLikeTemperatureSuggestions.FEELS_LIKE_TEMP_JUST_RIGHT;
                feelsLikeTempResult.Add(feelsLikeTemp);
            }
            return feelsLikeTempResult;
        }

        public List<HourlyTempForeCastAndSuggestionsDTO> GetSuggestionsForHourlyTemperature(GetHourlyTemperatureResponseDTO HourlyTempRspDToResult)
        {
            double averageTemperature = 0;
            string stringSuggestion = "";
            int TotalHours = HourlyTempRspDToResult!.Hourly!.Time!.Count;
            int NoOfDays = TotalHours / 24;
            int startingHour = 0;
            for (int i = 1; i <= NoOfDays; i++)
            {
                HourlyTempForeCastAndSuggestionsDTO hourlyTemperatureSuggestion = new HourlyTempForeCastAndSuggestionsDTO();
                List<double> Temperature = HourlyTempRspDToResult!.Hourly!.Temperature_2m!.GetRange(startingHour, 24);
                foreach (var tempe in Temperature)
                    averageTemperature += tempe;
                averageTemperature = averageTemperature / 24;

                if (averageTemperature > 23)
                    stringSuggestion = HourlyTemperatureSuggestions.FEELS_HOT;
                else if (averageTemperature < 16)
                    stringSuggestion = HourlyTemperatureSuggestions.FEELS_COLD;
                else
                    stringSuggestion = HourlyTemperatureSuggestions.FEELS_PLEASANT;

                hourlyTemperatureSuggestion.Day = i;
                hourlyTemperatureSuggestion.Date = HourlyTempRspDToResult.Hourly.Time[startingHour].Substring(0,10);
                hourlyTemperatureSuggestion.AverargeTemperature = averageTemperature;
                hourlyTemperatureSuggestion.Suggestion = stringSuggestion;
                hourlyTemperatureSuggestions.Add(hourlyTemperatureSuggestion);

                startingHour = startingHour + 24;
            }

            return hourlyTemperatureSuggestions;
        }
    }
}
