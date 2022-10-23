namespace WeatherAPI.Helper
{
    public static class ConstantsHelper
    {
        public const string GEO_API_URL = "https://geocoding-api.open-meteo.com/v1/search?name=[city]&count=1";
        public const string AQ_PM_URL = "https://air-quality-api.open-meteo.com/v1/air-quality?latitude=[lat]&longitude=[lon]&hourly=pm10,pm2_5";
    }
}
