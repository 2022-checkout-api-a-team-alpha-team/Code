﻿namespace WeatherAPI.DTOs
{
    public class HourlyTempForeCastAndSuggestionsDTO
    {
        public int Day { get; set; }
        public string? Date { get; set; }
        public string? AverageTemperature { get; set; }

        public string? Suggestion { get; set; }
    }
}
