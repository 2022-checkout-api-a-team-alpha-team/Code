using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using WeatherAPI.DTOs;
using WeatherAPI.Helper;
using WeatherAPI.Services;

namespace WeatherAPI.Tests.ServicesTests
{
    public class WeatherServiceTest
    {
        private WeatherService? _weatherService;

        [SetUp]
        public void Setup()
        {
            _weatherService = new WeatherService();
        }

        [Test]
        public void Get_Hourly_Temperature_By_Latitude_And_Longitude_Return_Type_Is_GetHourlyTemperatureResponseDTO()
        {
            var result = _weatherService!.GetHourlyTemperatureByLatitudeAndLongitude(51.5, -0.1262).Result;
            result.Should().BeOfType(typeof(GetHourlyTemperatureResponseDTO));
        }

        [Test]
        public void Get_Hourly_Temperature_By_CityName_Should_Return_NO_OF_RECORDS_AS_7()
        {
            var result = _weatherService!.GetHourlyTemperatureByCity("London").Result;
            result.Count.Should().Be(7);
        }

        [Test]
        public void Get_Hourly_Temperature_By_CityName_Should_Return_Type_List_Of_HourlyTempForeCastAndSuggestions()
        {
            var result = _weatherService.GetHourlyTemperatureByCity("London").Result;
            result.Should().BeOfType(typeof(List<HourlyTempForeCastAndSuggestionsDTO>));
        }

        [Test]
        public void Get_Hourly_Temperature_By_CityName_Should_Give_Suggestions_Based_On_Hourly_Temperature()
        {
            var result = _weatherService!.GetHourlyTemperatureByCity("London").Result;
            foreach (var record in result)
            {
                if (record.AverargeTemperature > 23)
                {
                    record.Suggestion.Should().Be(HourlyTemperatureSuggestions.FEELS_HOT);
                }
                else if (record.AverargeTemperature < 16)
                {
                    record.Suggestion.Should().Be(HourlyTemperatureSuggestions.FEELS_COLD);
                }
                else
                    record.Suggestion.Should().Be(HourlyTemperatureSuggestions.FEELS_PLEASANT);
            }
        }

        [Test]
        public void Get_Hourly_Feels_Like_Temperature_By_CityName_Response_Should_Return_Expected_No_Of_Records()
        {
            var result = _weatherService!.GetHourlyFeelsLikeTemperatureByCity("London").Result;
            result.Count.Should().Be(24);
        }

        [Test]
        public void Get_Hourly_Feels_Like_Temperature_By_CityName_Should_Return_Type_List_Of_FeelsLikeTempForecastSuggestions()
        {
            var result = _weatherService!.GetHourlyFeelsLikeTemperatureByCity("London").Result;
            result.Should().BeOfType(typeof(List<FeelsLikeTempForecastSuggestionsDTO>));
        }

        [Test]
        public void Get_Hourly_Feels_Like_Temperature_By_CityName_Should_Give_Dressing_Suggestion_Based_On_Feels_Like_Temperature()
        {
            var result = _weatherService!.GetHourlyFeelsLikeTemperatureByCity("London").Result;
            foreach(var record in result)
            {
                if (record.Temperature < record.FeelsLikeTemperature)
                {
                    record.Suggestion.Should().Be(FeelsLikeTemperatureSuggestions.FEELS_LIKE_TEMP_HOT);
                }
                else if (record.Temperature > record.FeelsLikeTemperature)
                {
                    record.Suggestion.Should().Be(FeelsLikeTemperatureSuggestions.FEELS_LIKE_TEMP_COLD);
                }
                else
                    record.Suggestion.Should().Be(FeelsLikeTemperatureSuggestions.FEELS_LIKE_TEMP_JUST_RIGHT);
            }
        }

        [Test]
        public void Get_Suggestions_Based_On_Current_Weather_Should_Return_Correct_Response()
        {
            var result = _weatherService!.GetSuggestionsBasedOnCurrentWeather("London").Result;
            result.Should().BeOfType<GetSuggestionsBasedOnWeatherDTO>();
        }
    }
}
