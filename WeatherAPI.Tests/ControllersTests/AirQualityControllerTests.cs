using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using WeatherAPI.Controllers;
using WeatherAPI.DTOs;
using WeatherAPI.Helper;
using WeatherAPI.Services;

namespace WeatherAPI.Tests.ControllersTests
{
    public class AirQualityControllerTests
    {

        private AirQualityController _airQualityController;
        private Mock<IAirQualityService> _mockAirQualityService;


        [SetUp]
        public void Setup()
        {
            _mockAirQualityService = new Mock<IAirQualityService>();
            _airQualityController = new AirQualityController(_mockAirQualityService.Object);
        }

        [Test]
        public async Task GetPollenData_Should_Return_Result()
        {
            //Arrange
            _mockAirQualityService.Setup(g => g.GetPollenData("London")).ReturnsAsync(new GetPollenDTO());

            //Act
            var response = await _airQualityController.GetPollenData("London");

            //Assert
            var okResult = response as OkObjectResult;
            okResult.Should().NotBeNull();
        }

        [Test]
        public async Task GetPollenData_Should_Return_Bad_Request()
        {
            //Act
            var response = await _airQualityController.GetPollenData(null);

            //Assert
            var badReqObjResult = response as BadRequestObjectResult;
            badReqObjResult.Should().NotBeNull();
            badReqObjResult.Value.Should().Be(ErrorHelper.PARAMETER_CANNOT_BE_NULL_OR_EMPTY);
        }

        [Test]
        public async Task GetPollenData_Should_Return_Service_Error()
        {
            //Arrange
            _mockAirQualityService.Setup(g => g.GetPollenData("London")).Throws<HttpRequestException>();

            //Act
            var response = await _airQualityController.GetPollenData("London");

            //Assert
            var errorResult = response as ObjectResult;
            errorResult.Should().NotBeNull();
            errorResult.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
        }

        //--------------------------------------------
        private static SuggestionsOnAirQualityParticulateMatterDTO SuggestOnAQPMObj()
        {
            JsonSerializerOptions options = new()
            {
                PropertyNameCaseInsensitive = true,
                AllowTrailingCommas = true,
                DictionaryKeyPolicy = JsonNamingPolicy.CamelCase
            };
            string expectedResultJson = @"{
                ""latitude"": 3.2000046,
                ""longitude"": 101.600006,
                ""utC_Offset_Seconds"": 0,
                ""timeZone"": ""GMT"",
                ""timeZone_Abbreviation"": ""GMT"",
                ""daily_Suggestion"": {
                    ""date"": [
                        ""2022-10-27"",
                        ""2022-10-28"",
                        ""2022-10-29"",
                        ""2022-10-30"",
                        ""2022-10-31""
                        ],
                    ""midnight_Suggestion"": [
                        ""The air quality is usual."",
                        ""The air quality is usual."",
                        ""The air quality is usual."",
                        ""The air quality is usual."",
                        ""The air quality is usual.""
                        ],
                    ""morning_Suggestion"": [
                        ""The air quality is usual."",
                        ""The air quality is usual."",
                        ""The air quality is usual."",
                        ""The air quality is usual."",
                        ""The air quality is usual.""
                        ],
                    ""afternoon_Suggestion"": [
                        ""The air quality is usual."",
                        ""The air quality is usual."",
                        ""The air quality is not very good for air-sensitive individuals."",
                        ""The air quality is not very good for air-sensitive individuals."",
                        ""The air quality is not very good for air-sensitive individuals.""
                        ],
                    ""evening_Suggestion"": [
                        ""The air quality is usual."",
                        ""The air quality is usual."",
                        ""The air quality is usual."",
                        ""The air quality is not very good for air-sensitive individuals."",
                        ""The air quality is usual.""
                        ]
                    }
                }";
            return JsonSerializer.Deserialize<SuggestionsOnAirQualityParticulateMatterDTO>(expectedResultJson, options);
        }

        [Test]
        public async Task Suggest_On_Air_Quality_Particulate_Matter_By_City_Name_Should_Response_Via_API()
        {
            // Arrange
            _mockAirQualityService
                .Setup(r => r.SuggestionsOnAirQualityParticulateMatterByCityName("Kuala Lumpur"))
                .ReturnsAsync(SuggestOnAQPMObj());

            // Act
            var result = await _airQualityController.SuggestionsOnAirQualityParticulateMatterByCityName("Kuala Lumpur") as ObjectResult;

            // Assert
            Assert.That(result, Is.TypeOf<OkObjectResult>());
            Assert.AreEqual(HttpStatusCode.OK, (HttpStatusCode)result.StatusCode);
            _mockAirQualityService.Verify(c => c.SuggestionsOnAirQualityParticulateMatterByCityName(It.IsAny<string>()), Times.Once());
        }

        [Test]
        public async Task Suggest_On_Air_Quality_Particulate_Matter_By_City_Name_Should_Return_BadRequestResults_Via_API()
        {
            // Arrange
            _mockAirQualityService
                .Setup(r => r.SuggestionsOnAirQualityParticulateMatterByCityName("Kuala Lumpur"))
                .ThrowsAsync(new Exception("Test throw exception."));

            // Act
            var result = await _airQualityController.SuggestionsOnAirQualityParticulateMatterByCityName("Kuala Lumpur") as ObjectResult;

            // Assert
            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test]//** Kenny Chiang's example moq test with comments explaining functions of each lines
        public async Task Suggest_On_Air_Quality_Particulate_Matter_By_City_Name_Should_Return_Result_Via_API()
        {
            // Arrange
            //Mock data - here is using just the object structure with empty values
            SuggestionsOnAirQualityParticulateMatterDTO expected = new();

            //Setup mock service object to return with mock data upon calling a specific method with specific parameters
            _mockAirQualityService
                .Setup(repo => repo.SuggestionsOnAirQualityParticulateMatterByCityName("Kuala Lumpur"))
                .ReturnsAsync(expected);

            // Act
            //Calling the method in the mock service object via the controller object under test
            var result = await _airQualityController.SuggestionsOnAirQualityParticulateMatterByCityName("Kuala Lumpur") as ObjectResult;

            // Assert
            var actualResult = result.Value;
            Assert.That(actualResult, Is.EqualTo(expected));
            Assert.AreEqual(expected, actualResult);

            //Ensure HttpResponse
            Assert.That(result, Is.TypeOf<OkObjectResult>());
            //Ensure HttpResponse
            Assert.AreEqual(HttpStatusCode.OK, (HttpStatusCode)result.StatusCode);
            //Assert Dependency called
            _mockAirQualityService.Verify(c => c.SuggestionsOnAirQualityParticulateMatterByCityName(It.IsAny<string>()), Times.Once());
        }
    }
}
