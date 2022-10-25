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
using WeatherAPI.Models;
using System.Web.Http;

namespace WeatherAPI.Tests.ControllersTests
{    
    public class WeatherControllerTest
    {
        private WeatherController _weatherController;
        private Mock<IWeatherService> _mockWeatherService;

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
            _mockWeatherService.Setup(g => g.GetHourlyFeelsLikeTemperatureByCity("London")).ReturnsAsync(GetFeelsLikeTemperatureObj());

            //Act
            var response = _weatherController.GetHourlyFeelsLikeTemperatureByCity("London").Result;

            //Assert
            var okResult = response as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult?.StatusCode.Should().Be(200);
        }

        [Test]
        public void Get_Hourly_Feels_Like_Temperature_By_City_Should_Return_BadRequest_When_Given_Wrong_Input()
        {
            //Arange
            _mockWeatherService.Setup(g => g.GetHourlyFeelsLikeTemperatureByCity("London")).ReturnsAsync(GetFeelsLikeTemperatureObj());

            //Act
            var response = _weatherController.GetHourlyFeelsLikeTemperatureByCity("").Result;

            //Assert
            var badResult = response as BadRequestObjectResult;
            badResult.Should().BeNull();
            badResult?.StatusCode.Should().Be(400);
            badResult?.Value.ToString().Equals(ErrorHelper.PARAMETER_CANNOT_BE_NULL_OR_EMPTY).Should().BeTrue();
        }
      
        //Dressing suggestions for Feels Like Temperature
        private class feelLikeTemperatureSuggestions 
        {
            public static string FEELS_LIKE_TEMP_COLD = "You'll feel colder than outside - Better to wear a jumper/ a jacket to avoid any chills.";
            public static string FEELS_LIKE_TEMP_HOT = "You'll feel hotter than outside - Better to wear light cotton clothes.";
            public static string FEELS_LIKE_TEMP_JUST_RIGHT = "You'll feel just the right temperature as in air when you go out. Wear as you like.";
        }

        private List<FeelsLikeTemperatureForecast> GetFeelsLikeTemperatureObj()
        {
            List<string>? dateList = new();
            List<double>? temperatureList = new();
            List<double>? feelsLikeTempList = new();
            FeelsLikeTemperatureForecast feelsLikeTemp;
            List<FeelsLikeTemperatureForecast> feelsLikeTempResult = new();

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
                feelsLikeTemp.Date = dateList[i];
                feelsLikeTemp.Temperature = temperatureList[i];
                feelsLikeTemp.FeelsLikeTemperature = feelsLikeTempList[i];
                if (feelsLikeTempList[i] < temperatureList[i])
                {
                    feelsLikeTemp.Suggestion = feelLikeTemperatureSuggestions.FEELS_LIKE_TEMP_COLD;
                }
                else if (feelsLikeTempList[i] > temperatureList[i])
                {
                    feelsLikeTemp.Suggestion = feelLikeTemperatureSuggestions.FEELS_LIKE_TEMP_HOT;
                }
                else
                {
                    feelsLikeTemp.Suggestion = feelLikeTemperatureSuggestions.FEELS_LIKE_TEMP_JUST_RIGHT;
                }
                feelsLikeTempResult.Add(feelsLikeTemp);
            }
            return feelsLikeTempResult;
        }
    }
}
