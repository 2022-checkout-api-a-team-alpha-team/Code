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

        //Dressing suggestions for Feels Like Temperature
        private class feelLikeTemperatureSuggestions 
        {
            public static string FEELS_LIKE_TEMP_COLD = "You'll feel colder than outside - Better to wear a jumper/ a jacket.";
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

/*

        [
          {
    "date": "2022-10-24T00:00",
            "temperature": 14.7,
            "feelsLikeTemperature": 13.3,
            "suggestion": "You'll feel colder than outside - Better to wear a jumper/ a jacket."
  },
  {
    "date": "2022-10-24T01:00",
    "temperature": 14.9,
    "feelsLikeTemperature": 12.5,
    "suggestion": "You'll feel colder than outside - Better to wear a jumper/ a jacket."
  },
  {
    "date": "2022-10-24T02:00",
    "temperature": 14.6,
    "feelsLikeTemperature": 12.2,
    "suggestion": "You'll feel colder than outside - Better to wear a jumper/ a jacket."
  },
  {
    "date": "2022-10-24T03:00",
    "temperature": 13.6,
    "feelsLikeTemperature": 11.1,
    "suggestion": "You'll feel colder than outside - Better to wear a jumper/ a jacket."
  },
  {
    "date": "2022-10-24T04:00",
    "temperature": 13.3,
    "feelsLikeTemperature": 11,
    "suggestion": "You'll feel colder than outside - Better to wear a jumper/ a jacket."
  },
  {
    "date": "2022-10-24T05:00",
    "temperature": 13.1,
    "feelsLikeTemperature": 10.7,
    "suggestion": "You'll feel colder than outside - Better to wear a jumper/ a jacket."
  },
  {
    "date": "2022-10-24T06:00",
    "temperature": 12.4,
    "feelsLikeTemperature": 10.1,
    "suggestion": "You'll feel colder than outside - Better to wear a jumper/ a jacket."
  },
  {
    "date": "2022-10-24T07:00",
    "temperature": 12.2,
    "feelsLikeTemperature": 10.1,
    "suggestion": "You'll feel colder than outside - Better to wear a jumper/ a jacket."
  },
  {
    "date": "2022-10-24T08:00",
    "temperature": 12.6,
    "feelsLikeTemperature": 10.7,
    "suggestion": "You'll feel colder than outside - Better to wear a jumper/ a jacket."
  },
  {
    "date": "2022-10-24T09:00",
    "temperature": 13.4,
    "feelsLikeTemperature": 10.9,
    "suggestion": "You'll feel colder than outside - Better to wear a jumper/ a jacket."
  },
  {
    "date": "2022-10-24T10:00",
    "temperature": 13.8,
    "feelsLikeTemperature": 11.9,
    "suggestion": "You'll feel colder than outside - Better to wear a jumper/ a jacket."
  },
  {
    "date": "2022-10-24T11:00",
    "temperature": 15.1,
    "feelsLikeTemperature": 12.7,
    "suggestion": "You'll feel colder than outside - Better to wear a jumper/ a jacket."
  },
  {
    "date": "2022-10-24T12:00",
    "temperature": 16.5,
    "feelsLikeTemperature": 14.5,
    "suggestion": "You'll feel colder than outside - Better to wear a jumper/ a jacket."
  },
  {
    "date": "2022-10-24T13:00",
    "temperature": 17.1,
    "feelsLikeTemperature": 15.1,
    "suggestion": "You'll feel colder than outside - Better to wear a jumper/ a jacket."
  },
  {
    "date": "2022-10-24T14:00",
    "temperature": 16.9,
    "feelsLikeTemperature": 14.9,
    "suggestion": "You'll feel colder than outside - Better to wear a jumper/ a jacket."
  },
  {
    "date": "2022-10-24T15:00",
    "temperature": 16.7,
    "feelsLikeTemperature": 14.4,
    "suggestion": "You'll feel colder than outside - Better to wear a jumper/ a jacket."
  },
  {
    "date": "2022-10-24T16:00",
    "temperature": 16,
    "feelsLikeTemperature": 13.9,
    "suggestion": "You'll feel colder than outside - Better to wear a jumper/ a jacket."
  },
  {
    "date": "2022-10-24T17:00",
    "temperature": 15,
    "feelsLikeTemperature": 13.6,
    "suggestion": "You'll feel colder than outside - Better to wear a jumper/ a jacket."
  },
  {
    "date": "2022-10-24T18:00",
    "temperature": 14.3,
    "feelsLikeTemperature": 13.2,
    "suggestion": "You'll feel colder than outside - Better to wear a jumper/ a jacket."
  },
  {
    "date": "2022-10-24T19:00",
    "temperature": 13.7,
    "feelsLikeTemperature": 13.1,
    "suggestion": "You'll feel colder than outside - Better to wear a jumper/ a jacket."
  },
  {
    "date": "2022-10-24T20:00",
    "temperature": 13.2,
    "feelsLikeTemperature": 12.2,
    "suggestion": "You'll feel colder than outside - Better to wear a jumper/ a jacket."
  },
  {
    "date": "2022-10-24T21:00",
    "temperature": 12.9,
    "feelsLikeTemperature": 11.8,
    "suggestion": "You'll feel colder than outside - Better to wear a jumper/ a jacket."
  },
  {
    "date": "2022-10-24T22:00",
    "temperature": 12.8,
    "feelsLikeTemperature": 11.7,
    "suggestion": "You'll feel colder than outside - Better to wear a jumper/ a jacket."
  },
  {
    "date": "2022-10-24T23:00",
    "temperature": 12.4,
    "feelsLikeTemperature": 11.4,
    "suggestion": "You'll feel colder than outside - Better to wear a jumper/ a jacket."
  }
]
*/
    }
}
