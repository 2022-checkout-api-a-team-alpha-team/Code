using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using WeatherAPI.Controllers;
using WeatherAPI.DTOs;
using WeatherAPI.Helper;
using WeatherAPI.Services;

namespace WeatherAPI.Tests.ControllersTests
{
    public class GeoControllerTest
    {
        private GeoController _geoController;
        private Mock<IGeoService> _mockGeoService;

        [SetUp]
        public void Setup()
        {
            //Arrange
            _mockGeoService = new Mock<IGeoService>();
            _geoController = new GeoController(_mockGeoService.Object);
        }

        [Test]
        public void Get_Coord_By_City_Name_Should_Return_Ok_When_Given_Right_Input()
        {
            //Arange
            _mockGeoService.Setup(g => g.GetGeoCoordinatesByCityName("London")).ReturnsAsync(GetGeoCoordObj());

            //Act
            var response = _geoController.GetGeoCoordinatesByCityName("London").Result;

            //Assert
            var okResult = response as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult?.StatusCode.Should().Be(200);
        }

        [Test]
        public void Get_Coord_By_City_Name_Should_Return_BadRequest_When_Given_Wrong_Input()
        {

            //Act
            var response = _geoController.GetGeoCoordinatesByCityName(null).Result;

            //Assert
            var badResult = response as BadRequestObjectResult;
            badResult.Should().NotBeNull();
            badResult?.StatusCode.Should().Be(400);
            badResult?.Value.ToString().Equals(ErrorHelper.PARAMETER_CANNOT_BE_NULL_OR_EMPTY).Should().BeTrue();

            //Act
            response = _geoController.GetGeoCoordinatesByCityName("").Result;

            //Assert
            badResult = response as BadRequestObjectResult;
            badResult.Should().NotBeNull();
            badResult?.StatusCode.Should().Be(400);
            badResult?.Value.ToString().Equals(ErrorHelper.PARAMETER_CANNOT_BE_NULL_OR_EMPTY).Should().BeTrue();
        }

        private GetGeoCoordResponseDTO GetGeoCoordObj()
        {
            string jsonOfObjectToReturn = @"{
                                      ""results"": [
                                        {
                                          ""id"": 2643743,
                                          ""name"": ""London"",
                                          ""latitude"": 51.50853,
                                          ""longitude"": -0.12574,
                                          ""elevation"": 25.0,
                                          ""feature_code"": ""PPLC"",
                                          ""country_code"": ""GB"",
                                          ""admin1_id"": 6269131,
                                          ""admin2_id"": 2648110,
                                          ""timezone"": ""Europe/London"",
                                          ""population"": 7556900,
                                          ""country_id"": 2635167,
                                          ""country"": ""United Kingdom"",
                                          ""admin1"": ""England"",
                                          ""admin2"": ""Greater London""
                                        }
                                      ],
                                      ""generationtime_ms"": 1.0870695
                                    }";

            var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
                AllowTrailingCommas = true,
                DictionaryKeyPolicy = JsonNamingPolicy.CamelCase
            };
            return JsonSerializer.Deserialize<GetGeoCoordResponseDTO>(jsonOfObjectToReturn, options);
        }
    }
}