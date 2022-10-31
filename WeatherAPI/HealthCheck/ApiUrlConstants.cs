namespace WeatherAPI.HealthCheck
{
    public static class ApiUrlConstants
    {
        public const string GEO = "https://geocoding-api.open-meteo.com/v1/search?name=a";
        public const string WEATHER = "https://api.open-meteo.com/v1/forecast?latitude=0&longitude=0";
        public const string AQ = "https://air-quality-api.open-meteo.com/v1/air-quality?latitude=0&longitude=0";
    }
}
