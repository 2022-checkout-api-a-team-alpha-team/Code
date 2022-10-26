namespace WeatherAPI.Helper
{
    public class WeatherCodesHelper
    {
        public readonly static Dictionary<int, string> WeatherCodes = new Dictionary<int, string> () 
        {
            { 0, "It is clear sky."},
            { 1, "It is mainly clear."},
            { 2, "It is partly cloudy."},
            { 3, "It is overcast."},
            { 45, "It is foggy."},
            { 48, "There is rime fog."},
            { 51, "There is light drizzle."},
            { 53, "There is morderate drizzle."},
            { 55, "There is drizzle with dense intensity."},
            { 56, "There is light freezing drizzle."},
            { 57, "There is freezing drizzle with dense intensity."},
            { 61, "There is slight rain."},
            { 63, "There is morderate rain."},
            { 65, "It is raining with heavy intensity."},
            { 66, "There is light freezing rain."},
            { 67, "There is freezing rain with heavy intensity."},
            { 71, "There is light snow fall."},
            { 73, "There is morderate snow fall."},
            { 75, "It is snowing with heavy intensity."},
            { 77, "There are snow grains."},
            { 80, "There are light rain showers."},
            { 81, "There are morderate rain showers."},
            { 82, "There are violent rain showers."},
            { 85, "There are slight snow showers."},
            { 86, "There are heavy snow showers."},
            { 95, "There is slight or moderate thunderstorm."},
            { 96, "There is thunderstorm with slight hail."},
            { 99, "There is thunderstorm with heavy hail."},
        };
    }
}
