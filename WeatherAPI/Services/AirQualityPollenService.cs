﻿using System.Globalization;
using System.Text.Json;
using WeatherAPI.DTOs;

namespace WeatherAPI.Services
{
    public class AirQualityPollenService : IAirQualityPollenService
    {
        private readonly HttpClient _httpClient;
        private readonly IGeoService _geoService;

        public AirQualityPollenService(HttpClient httpClient, IGeoService geoService)
        {
            _httpClient = httpClient;
            _geoService = geoService;
        }

        public async Task<GetPollenDTO> GetPollenData(string cityName)
        {
            var coordinates = await _geoService.GetGeoCoordinatesByCityName(cityName);

            if (coordinates.Results != null && coordinates.Results.Count > 0)
            {
                GeoCoordDTO coordDTO = coordinates.Results[0];
                var latitude = coordDTO.Latitude.ToString(CultureInfo.InvariantCulture);
                var longitude = coordDTO.Longitude.ToString(CultureInfo.InvariantCulture);
                var query = $"?latitude={latitude}&longitude={longitude}&hourly=alder_pollen,birch_pollen,grass_pollen,mugwort_pollen,olive_pollen,ragweed_pollen";

                var options = new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true,
                    AllowTrailingCommas = true,
                    DictionaryKeyPolicy = JsonNamingPolicy.CamelCase
                };

                var result = await _httpClient.GetFromJsonAsync<GetPollenDTO>(query , options);
                return result;
            }
            return null;
        }
    }
}