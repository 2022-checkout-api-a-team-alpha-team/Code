using static System.Net.WebRequestMethods;

namespace WeatherAPI.Helper
{
    public static class ConstantsHelper
    {
        public const string GEO_API_URL = "https://geocoding-api.open-meteo.com/v1/search?name=[city]&count=1";
        public const string WEATHER_API_URL = "https://api.open-meteo.com/v1/forecast?latitude=[latitude]&longitude=[longitude]&hourly=temperature_2m";
    }
}
