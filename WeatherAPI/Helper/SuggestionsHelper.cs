namespace WeatherAPI.Helper
{
    public static class WeatherSuggestionsHelper
    {
        public const string WEAR_JACKET = "Better to wear a jumper/ a jacket to avoid any chills.";
        public const string WEAR_LIGHT_CLOTHES = "Better to wear light cotton clothes.";
        public const string WEAR_AS_YOU_LIKE = "Wear as you like.";
    }

    public static class FeelsLikeTemperatureSuggestions
    {
        public const string FEELS_LIKE_TEMP_COLD = "You'll feel colder than outside - " + WeatherSuggestionsHelper.WEAR_JACKET;
        public const string FEELS_LIKE_TEMP_HOT = "You'll feel hotter than outside - " + WeatherSuggestionsHelper.WEAR_LIGHT_CLOTHES;
        public const string FEELS_LIKE_TEMP_JUST_RIGHT = "You'll feel just the right temperature as in air when you go out. " + WeatherSuggestionsHelper.WEAR_AS_YOU_LIKE;
    }

    public static class HourlyTemperatureSuggestions
    {
        public const string FEELS_HOT = "It is a Hot day. Please plan your days/trip with proper Accessories and Appropriate Activities.";
        public const string FEELS_COLD = "Its is a Cold day. Please plan your days/trip with proper Accessories and Appropriate Activities.";
        public const string FEELS_PLEASANT = "It is a Pleasant day. Enjoy your day!!!";
    }

    public static class RangeOfHour
    {
        public static readonly int[,] DAY_DURATONS = new int[,] { { 0, 6 }, { 6, 6 }, { 12, 6 }, { 18, 6 } };
    }

    public static class AQPMThreshold
    {
        public const double PM_25_GOOD = 12;
        public const double PM_25_MODERATE = 35.4;
        public const double PM_25_UNHEALTHY_FOR_SENSITIVE_INDIVIDUALS = 55.4;
        public const double PM_25_UNHEALTHY = 150.4;
        public const double PM_25_VERY_UNHEALTHY = 250.4;
        public const double PM_25_HAZARDOUS = 350.4;
        public const double PM_25_VERY_HAZARDOUS = 500.4;
        public const double PM_25_EXTREMELY_HAZARDOUS = 99999.9;

        public const double PM_10_GOOD = 54;
        public const double PM_10_MODERATE = 154;
        public const double PM_10_UNHEALTHY_FOR_SENSITIVE_INDIVIDUALS = 254;
        public const double PM_10_UNHEALTHY = 354;
        public const double PM_10_VERY_UNHEALTHY = 424;
        public const double PM_10_HAZARDOUS = 504;
        public const double PM_10_VERY_HAZARDOUS = 604;
        public const double PM_10_EXTREMELY_HAZARDOUS = 9999;
    }

    public static class AQPMSuggestionMessages
    {
        public const string MSG_GOOD = "Great air quality, go outside and enjoy it!";
        public const string MSG_MODERATE = "The air quality is usual.";
        public const string MSG_UNHEALTHY_FOR_SENSITIVE_INDIVIDUALS = "The air quality is not very good for air-sensitive individuals.";
        public const string MSG_UNHEALTHY = "The air quality is generally not very good";
        public const string MSG_VERY_UNHEALTHY = "The air is generally dirty and better wear a mask.";
        public const string MSG_HAZARDOUS = "Avoid breathing air from outside and a mask is required.";
        public const string MSG_VERY_HAZARDOUS = "Do not breathe air from outside and wear a better mask!";
        public const string MSG_EXTREMELY_HAZARDOUS = "Air is too dirty to breathe in and out. Put on an individual air supply, or stay in a place with special air-processing facility.";
        public const string MSG_INSUFFICIENT_DATA = "Period data is insufficient and unable to provide suggestions.";
    }
}
