using Moq;
using WeatherAPI.Services;
using WeatherAPI.Controllers;
using NUnit.Framework;
using System.Threading.Tasks;
using System.Net.Http;
using WeatherAPI.Helper;
using System;

namespace WeatherAPI.Tests.ServicesTests
{
    public class AirQualityPollenServiceTests 
    {
        public AirQualityPollenServiceTests()
        {

        }

        [Test]
        public async Task GetPollenData_Should_Return_DTO() 
        {
            // Arrange
            var client = new HttpClient();
            client.BaseAddress = new Uri(ConstantsHelper.AQ_BASE);
            var geoService = new GeoService();
            var service = new AirQualityPollenService(client, geoService);

            // Act
            var actualResult = await service.GetPollenData("London");
            var hourlyResults = actualResult?.Hourly;

            // Assert
            Assert.NotNull(hourlyResults);
            Assert.AreEqual(DateTime.Today, DateTime.Parse(hourlyResults.Time[0]));
            Assert.Greater(hourlyResults.BirchPollen.Count, 0);
            Assert.Greater(hourlyResults.AlderPollen.Count, 0);
            Assert.Greater(hourlyResults.GrassPollen.Count, 0);
            Assert.Greater(hourlyResults.OlivePollen.Count, 0);
            Assert.Greater(hourlyResults.RagweedPollen.Count, 0);
            Assert.Greater(hourlyResults.MugwortPollen.Count, 0);
        }


    }
}
