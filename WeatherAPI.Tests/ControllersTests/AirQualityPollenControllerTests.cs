using System.Threading.Tasks;
using WeatherAPI.Controllers;
using WeatherAPI.Services;
using Moq;
using NUnit.Framework;
using WeatherAPI.DTOs;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;
using WeatherAPI.Helper;
using System.Net.Http;
using Microsoft.AspNetCore.Http;

namespace WeatherAPI.Tests.ControllersTests
{
    public class AirQualityPollenControllerTests
    {

        private AirQualityPollenController _aqPollenController;
        private Mock<IAirQualityPollenService> _mockAQPollenService;


        [SetUp]
        public void Setup()
        {
            _mockAQPollenService = new Mock<IAirQualityPollenService>();
            _aqPollenController = new AirQualityPollenController(_mockAQPollenService.Object);
        }

        [Test]
        public async Task GetPollenData_Should_Return_Result()
        {
            //Arrange
            _mockAQPollenService.Setup(g => g.GetPollenData("London")).ReturnsAsync(new GetPollenDTO());

            //Act
            var response = await _aqPollenController.GetPollenData("London");

            //Assert
            var okResult = response as OkObjectResult;
            okResult.Should().NotBeNull();
        }

        [Test]
        public async Task GetPollenData_Should_Return_Bad_Request()
        {
            //Act
            var response = await _aqPollenController.GetPollenData(null);

            //Assert
            var badReqObjResult = response as BadRequestObjectResult;
            badReqObjResult.Should().NotBeNull();
            badReqObjResult.Value.Should().Be(ErrorHelper.PARAMETER_CANNOT_BE_NULL_OR_EMPTY);
        }

        [Test]
        public async Task GetPollenData_Should_Return_Service_Error()
        {
            //Arrange
            _mockAQPollenService.Setup(g => g.GetPollenData("London")).Throws<HttpRequestException>();

            //Act
            var response = await _aqPollenController.GetPollenData("London");

            //Assert
            var errorResult = response as ObjectResult;
            errorResult.Should().NotBeNull();
            errorResult.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
        }
    }
}
