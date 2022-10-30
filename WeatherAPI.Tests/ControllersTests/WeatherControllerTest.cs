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
using IdentityModel.OidcClient;

namespace WeatherAPI.Tests.ControllersTests
{
    public class WeatherControllerTest
    {
        private WeatherController? _weatherController;
        private Mock<IWeatherService>? _mockWeatherService;

        private const int FEELS_LIKE_TEMP_NO_OF_HOURS = 2;

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
        public void Get_Hourly_Feels_Like_Temperature_By_City_Should_Return_The_Expected_Response()
        {
            List<FeelsLikeTempForecastSuggestionsDTO> expectedValue = new() { new FeelsLikeTempForecastSuggestionsDTO() };
            //Arange
            _mockWeatherService!.Setup(g => g.GetHourlyFeelsLikeTemperatureByCity("London")).ReturnsAsync(expectedValue);

            //Act
            var response = _weatherController!.GetHourlyFeelsLikeTemperatureByCity("London").Result;

            //Assert
            var actualResult = response as OkObjectResult;
            var responseValue = actualResult!.Value;
            responseValue.Should().NotBeNull();
            _mockWeatherService.Verify(c => c.GetHourlyFeelsLikeTemperatureByCity(It.IsAny<string>()), Times.Once());
            Assert.AreEqual(200, actualResult.StatusCode);
            Assert.That(actualResult, Is.TypeOf<OkObjectResult>());
            Assert.That(responseValue, Is.EqualTo(expectedValue));
            Assert.AreEqual(responseValue, expectedValue);
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
            FeelsLikeTempForecastSuggestionsDTO feelsLikeTemp;
            List<FeelsLikeTempForecastSuggestionsDTO> feelsLikeTempResult = new();
            DateTime date = DateTime.Now;
            for (int i = 0; i < FEELS_LIKE_TEMP_NO_OF_HOURS; i++)
            { 
                feelsLikeTemp = new();
                feelsLikeTemp.Date = date.ToString().Substring(0, 11);
                feelsLikeTemp.Time_24_Hour_Clock = date.AddHours(i).ToString().Substring(11, 5);
                feelsLikeTemp.Temperature = 15.5;
                feelsLikeTemp.FeelsLikeTemperature = 16;
                feelsLikeTemp.Suggestion = FeelsLikeTemperatureSuggestions.FEELS_LIKE_TEMP_COLD;
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
        public async Task Get_Hourly_Temperature_By_City_Name_Should_Return()
        {
            List<HourlyTempForeCastAndSuggestionsDTO> hourlyTempForecastAndSuggestions = new() { new HourlyTempForeCastAndSuggestionsDTO(), new HourlyTempForeCastAndSuggestionsDTO(), new HourlyTempForeCastAndSuggestionsDTO() };
            //Arange
            _mockWeatherService!.Setup(g => g.GetHourlyTemperatureByCity("London")).ReturnsAsync(hourlyTempForecastAndSuggestions);

            // Act
            var response = await _weatherController!.GetHourlyTemperatureByCity("London");

            //Assert
            var okResult = response as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult!.StatusCode.Should().Be(200);
            var responseValue = okResult.Value;
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(3, hourlyTempForecastAndSuggestions.Count);
            Assert.That(responseValue, Is.EqualTo(hourlyTempForecastAndSuggestions));
            Assert.AreEqual(responseValue, hourlyTempForecastAndSuggestions);
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
            var response = _weatherController!.GetSuggestionsBasedOnWeather(null!).Result;

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
