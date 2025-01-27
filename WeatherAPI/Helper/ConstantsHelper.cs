﻿using static System.Net.WebRequestMethods;

namespace WeatherAPI.Helper
{
    public static class ConstantsHelper
    {
        public const string GEO_API_URL = "https://geocoding-api.open-meteo.com/v1/search?name=[city]&count=1";
        public const string WEATHER_API_URL = "https://api.open-meteo.com/v1/forecast?latitude=[latitude]&longitude=[longitude]&hourly=temperature_2m";
        public const string WEATHER_API_FEELS_LIKE_TEMPERATURE_URL = "https://api.open-meteo.com/v1/forecast?latitude=[latitude]&longitude=[longitude]&hourly=temperature_2m,apparent_temperature";
        public const string AQ_BASE = "https://air-quality-api.open-meteo.com/v1/air-quality";
        public const string WEATHER_API_GET_SUGGESTIONS_BASED_ON_CURRENT_WEATHER_URL = "https://api.open-meteo.com/v1/forecast?latitude=[latitude]&longitude=[longitude]&daily=weathercode,temperature_2m_max,temperature_2m_min&timezone=auto";
    }
}
