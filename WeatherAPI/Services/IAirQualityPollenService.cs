﻿using WeatherAPI.DTOs;

namespace WeatherAPI.Services
{
    public interface IAirQualityPollenService
    {
        Task<GetPollenDTO> GetPollenData(string cityName);
    }
}
