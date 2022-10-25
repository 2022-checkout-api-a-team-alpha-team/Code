namespace WeatherAPI.Helper
{
    public static class WeatherSuggestionsHelper
    {
        public const string WEAR_JACKET = "Better to wear a jumper/ a jacket to avoid any chills.";
        public const string WEAR_LIGHT_CLOTHES = "Better to wear light cotton clothes.";
        public const string WEAR_AS_YOU_LIKE = "Wear as you like.";
    }

    public static class FeelLikeTemperatureSuggestions
    {
        public const string FEELS_LIKE_TEMP_COLD = "You'll feel colder than outside - " + WeatherSuggestionsHelper.WEAR_JACKET;
        public const string FEELS_LIKE_TEMP_HOT = "You'll feel hotter than outside - " + WeatherSuggestionsHelper.WEAR_LIGHT_CLOTHES;
        public const string FEELS_LIKE_TEMP_JUST_RIGHT = "You'll feel just the right temperature as in air when you go out. " + WeatherSuggestionsHelper.WEAR_AS_YOU_LIKE;
    }
}
