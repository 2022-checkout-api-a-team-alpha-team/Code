using WeatherAPI.DTOs;

namespace WeatherAPI.Helper
{
    public class DailyWeatherSuggestionsCalculator
    {
        public static List<DailyWeatherSuggestionResponseDTO> GetListOfDailyWeatherSuggestionsForWeek(GetWeatherSuggestionsDailyDTO weeklyData)
        {
            var listToReturn = new List<DailyWeatherSuggestionResponseDTO>();
            for (int i = 0; i < weeklyData.Time?.Count; i++)
            {
                var dailySuggestion = new DailyWeatherSuggestionResponseDTO();
                dailySuggestion.Time = weeklyData.Time[i];
                dailySuggestion.MinTemperature = weeklyData.Temperature_2m_Min?[i];
                dailySuggestion.MaxTemperature = weeklyData.Temperature_2m_Max?[i];

                // If temp is between 10 and 15 degrees wear jumper or jacket
                if (dailySuggestion.MinTemperature <= WeatherTemperatureHelper.TEMP_BELOW_WHICH_SLIGHT_COLD
                                                && dailySuggestion.MinTemperature > WeatherTemperatureHelper.TEMP_BELOW_WHICH_COLD)
                {
                    dailySuggestion.Suggestion = WeatherCodesHelper.WeatherCodes[Convert.ToInt32(weeklyData.Weathercode?[i])]
                                                    + " " + WeatherSuggestionsHelper.WEAR_JACKET;
                }

                // If temp is less than or equal to 10, wear jumper or jacket
                if (dailySuggestion.MinTemperature <= WeatherTemperatureHelper.TEMP_BELOW_WHICH_COLD)
                {
                    dailySuggestion.Suggestion = WeatherCodesHelper.WeatherCodes[Convert.ToInt32(weeklyData.Weathercode?[i])]
                                                    + " " + WeatherSuggestionsHelper.WEAR_JACKET;
                }

                // If temp is greater than 15 but less than 25 degrees, wear as you like
                if (dailySuggestion.MinTemperature <= WeatherTemperatureHelper.TEMP_ABOVE_WHICH_HOT
                    && dailySuggestion.MinTemperature > WeatherTemperatureHelper.TEMP_ABOVE_WHICH_SLIGHT_WARM)
                {
                    dailySuggestion.Suggestion = WeatherCodesHelper.WeatherCodes[Convert.ToInt32(weeklyData.Weathercode?[i])]
                                                    + " " + WeatherSuggestionsHelper.WEAR_AS_YOU_LIKE;
                }

                // If temp is greater than 25 degrees, wear light clothes
                if (dailySuggestion.MinTemperature > WeatherTemperatureHelper.TEMP_ABOVE_WHICH_HOT)
                {
                    dailySuggestion.Suggestion = WeatherCodesHelper.WeatherCodes[Convert.ToInt32(weeklyData.Weathercode?[i])]
                                                    + " " + WeatherSuggestionsHelper.WEAR_LIGHT_CLOTHES;
                }

                listToReturn.Add(dailySuggestion);
            }

            return listToReturn;
        }
    }
}
