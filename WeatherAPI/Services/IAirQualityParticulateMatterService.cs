﻿using WeatherAPI.DTOs;

namespace WeatherAPI.Services
{
    public interface IAirQualityParticulateMatterService
    {
        Task<GetAirQualityParticulateMatterResponseDTO> GetAirQualityParticulateMatterByCityName(string cityName);
    }
}
