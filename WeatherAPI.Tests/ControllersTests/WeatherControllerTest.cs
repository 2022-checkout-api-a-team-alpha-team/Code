using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Text.Json;
using WeatherAPI.Controllers;
using WeatherAPI.DTOs;
using WeatherAPI.Helper;
using WeatherAPI.Services;

namespace WeatherAPI.Tests.ControllersTests
{
    public class WeatherControllerTest
    {
        private WeatherController? _weatherController;
        private Mock<IWeatherService>? _mockWeatherService;

        private const int FEELS_LIKE_TEMP_NO_OF_HOURS = 3;

        [SetUp]
        public void Setup()
        {
            //Arrange           
            _mockWeatherService = new Mock<IWeatherService>();
            _weatherController = new WeatherController(_mockWeatherService.Object);
        }

        [Test]
        public void Get_Hourly_Feels_Like_Temperature_By_City_Should_Return_Ok_When_Given_Right_Input()
        {
            //Arange
            _mockWeatherService!.Setup(g => g.GetHourlyFeelsLikeTemperatureByCity("London")).ReturnsAsync(GetFeelsLikeTemperatureObj());

            //Act
            var response = _weatherController!.GetHourlyFeelsLikeTemperatureByCity("London").Result;

            //Assert
            var okResult = response as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult?.StatusCode.Should().Be(200);
        }

        [Test]
        public void Get_Hourly_Feels_Like_Temperature_By_City_Should_Return_BadRequest_When_Given_Wrong_Input()
        {
            //Act
            var response = _weatherController!.GetHourlyFeelsLikeTemperatureByCity(null!).Result;

            //Assert
            var badResult = response as BadRequestObjectResult;
            badResult.Should().NotBeNull();
            badResult?.StatusCode.Should().Be(400);
            badResult?.Value!.ToString()!.Equals(ErrorHelper.PARAMETER_CANNOT_BE_NULL_OR_EMPTY).Should().BeTrue();

            //Act
            response = _weatherController!.GetHourlyFeelsLikeTemperatureByCity("").Result;

            //Assert
            badResult = response as BadRequestObjectResult;
            badResult.Should().NotBeNull();
            badResult?.StatusCode.Should().Be(400);
            badResult?.Value!.ToString()!.Equals(ErrorHelper.PARAMETER_CANNOT_BE_NULL_OR_EMPTY).Should().BeTrue();
        }      
       
        /// <summary>
        /// Method to prepare dummy data object for feels like temperature forcast suggestions for mock service
        /// </summary>
        /// <returns>List of suggestions and data based on feels like temperature forecast</returns>
        private List<FeelsLikeTempForecastSuggestionsDTO> GetFeelsLikeTemperatureObj()
        {
            List<string>? dateList = new();
            List<double>? temperatureList = new();
            List<double>? feelsLikeTempList = new();
            FeelsLikeTempForecastSuggestionsDTO feelsLikeTemp;
            List<FeelsLikeTempForecastSuggestionsDTO> feelsLikeTempResult = new();

            DateTime date = DateTime.Now;
            dateList.Add(date.ToString());

            for (int i = 0; i < FEELS_LIKE_TEMP_NO_OF_HOURS - 1; i++)
            {
                dateList.Add(date.AddHours(1).ToString());
            }

            for (int i = 0; i < FEELS_LIKE_TEMP_NO_OF_HOURS; i++)
            {
                temperatureList.Add(Random.Shared.Next(10, 20));
                feelsLikeTempList.Add(Random.Shared.Next(10, 20));
            }
            for (int i = 0; i < dateList.Count; i++)
            {
                feelsLikeTemp = new();
                feelsLikeTemp.Date = dateList[i].Substring(0,11);
                feelsLikeTemp.Time_24_Hour_Clock = dateList[i].Substring(11,5);
                feelsLikeTemp.Temperature = temperatureList[i];
                feelsLikeTemp.FeelsLikeTemperature = feelsLikeTempList[i];
                if (feelsLikeTempList[i] < temperatureList[i])
                    feelsLikeTemp.Suggestion = FeelsLikeTemperatureSuggestions.FEELS_LIKE_TEMP_COLD;
                else if (feelsLikeTempList[i] > temperatureList[i])
                    feelsLikeTemp.Suggestion = FeelsLikeTemperatureSuggestions.FEELS_LIKE_TEMP_HOT;
                else
                    feelsLikeTemp.Suggestion = FeelsLikeTemperatureSuggestions.FEELS_LIKE_TEMP_JUST_RIGHT;

                feelsLikeTempResult.Add(feelsLikeTemp);
            }
            return feelsLikeTempResult;
        }

        [Test]
        public void Get_Hourly_Temperature_By_City_Name_Should_Return_BadRequest_When_Input_Is_Null()
        {
            //Act
            var response = _weatherController!.GetHourlyTemperatureByCity(null!).Result;

            //Assert
            var badResult = response as BadRequestObjectResult;
            badResult.Should().NotBeNull();
            badResult?.StatusCode.Should().Be(400);
            badResult?.Value?.Equals(ErrorHelper.PARAMETER_CANNOT_BE_NULL_OR_EMPTY).Should().BeTrue();
        }

        [Test]
        public void Get_Hourly_Temperature_By_City_Name_Should_Return_BadRequest_When_Input_Is_Empty()
        {
            //Act
            var response = _weatherController!.GetHourlyTemperatureByCity("").Result;

            //Assert
            var badResult = response as BadRequestObjectResult;
            badResult.Should().NotBeNull();
            badResult?.StatusCode.Should().Be(400);
            badResult?.Value?.Equals(ErrorHelper.PARAMETER_CANNOT_BE_NULL_OR_EMPTY).Should().BeTrue();
        }

        [Test]
        public void Get_Hourly_Temperature_By_City_Name_Should_Return_Ok_When_Given_Right_Input()
        {
            //Arange
            _mockWeatherService!.Setup(g => g.GetHourlyTemperatureByCity("London")).ReturnsAsync(GetHourlyTempForeCastAndSuggestions());

            //Act
            var response = _weatherController!.GetHourlyTemperatureByCity("London").Result;

            //Assert
            var okResult = response as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult?.StatusCode.Should().Be(200);
        }

        private List<HourlyTempForeCastAndSuggestionsDTO> GetHourlyTempForeCastAndSuggestions()
        {
            List<HourlyTempForeCastAndSuggestionsDTO> ListHourlyTempForeCastAndSuggestions = new();

            HourlyTempForeCastAndSuggestionsDTO hourlyTempForeCastAndSuggestions = new();

            for (var i = 1; i < 3; i++)
            {
                hourlyTempForeCastAndSuggestions.Day = i;
                hourlyTempForeCastAndSuggestions.Date = DateTime.Now.ToString();
                hourlyTempForeCastAndSuggestions.AverageTemperature = "15.1";
                hourlyTempForeCastAndSuggestions.Suggestion = HourlyTemperatureSuggestions.FEELS_PLEASANT;
            }

            return ListHourlyTempForeCastAndSuggestions;
        }

        [Test]
        public void Get_Seggestions_Based_On_Weather_Should_Return_Ok_When_Given_Right_Input()
        {
            //Arange
            _mockWeatherService?.Setup(g => g.GetSuggestionsBasedOnCurrentWeather("London")).ReturnsAsync(GetWeatherObj());

            //Act
            var response = _weatherController!.GetSuggestionsBasedOnWeather("London").Result;

            //Assert
            var okResult = response as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult?.StatusCode.Should().Be(200);
        }

        private GetWeatherSuggestionsApiResponseDTO GetWeatherObj()
        {
            string jsonOfObjectToReturn = @"{
                                                ""latitude"": 51.5,
                                                ""longitude"": -0.120000124,
                                                ""generationtime_ms"": 0.9189844131469727,
                                                ""utc_offset_seconds"": 3600,
                                                ""timezone"": ""Europe/London"",
                                                ""timezone_abbreviation"": ""BST"",
                                                ""elevation"": 23.0,
                                                ""daily_units"": {
                                                    ""time"": ""iso8601"",
                                                    ""weathercode"": ""wmo code"",
                                                    ""temperature_2m_max"": ""°C"",
                                                    ""temperature_2m_min"": ""°C""
                                                },
                                                ""daily"": {
                                                    ""time"": [
                                                        ""2022-10-26"",
                                                        ""2022-10-27"",
                                                        ""2022-10-28"",
                                                        ""2022-10-29"",
                                                        ""2022-10-30"",
                                                        ""2022-10-31"",
                                                        ""2022-11-01""
                                                    ],
                                                    ""weathercode"": [
                                                        3,
                                                        80,
                                                        61,
                                                        63,
                                                        3,
                                                        3,
                                                        61
                                                    ],
                                                    ""temperature_2m_max"": [
                                                        18.8,
                                                        19.8,
                                                        17.8,
                                                        18.5,
                                                        17.8,
                                                        17.1,
                                                        17.4
                                                    ],
                                                    ""temperature_2m_min"": [
                                                        14.4,
                                                        13.9,
                                                        12.6,
                                                        12.6,
                                                        12.8,
                                                        12.6,
                                                        12.0
                                                    ]
                                                }
                                            }";

            var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
                AllowTrailingCommas = true,
                DictionaryKeyPolicy = JsonNamingPolicy.CamelCase
            };
            return JsonSerializer.Deserialize<GetWeatherSuggestionsApiResponseDTO>(jsonOfObjectToReturn, options)!;
        }

        [Test]
        public void Get_Seggestions_Based_On_Weather_Should_Return_BadRequest_When_Given_Wrong_Input()
        {

            //Act
            var response = _weatherController!.GetSuggestionsBasedOnWeather(null).Result;

            //Assert
            var badResult = response as BadRequestObjectResult;
            badResult.Should().NotBeNull();
            badResult?.StatusCode.Should().Be(400);
            badResult?.Value?.ToString()?.Equals(ErrorHelper.PARAMETER_CANNOT_BE_NULL_OR_EMPTY).Should().BeTrue();

            //Act
            response = _weatherController!.GetSuggestionsBasedOnWeather("").Result;

            //Assert
            badResult = response as BadRequestObjectResult;
            badResult.Should().NotBeNull();
            badResult?.StatusCode.Should().Be(400);
            badResult?.Value?.ToString()?.Equals(ErrorHelper.PARAMETER_CANNOT_BE_NULL_OR_EMPTY).Should().BeTrue();
        }
    }
}
