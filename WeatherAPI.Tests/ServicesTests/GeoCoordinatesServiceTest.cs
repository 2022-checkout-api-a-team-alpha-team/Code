using FluentAssertions;
using NUnit.Framework;
using System.Text.Json;
using System.Threading.Tasks;
using WeatherAPI.DTOs;
using WeatherAPI.Services;

namespace WeatherAPI.Tests.ServicesTests
{
    public class GeoCoordinatesServiceTest
    {
        private GeoService _geoService;
        [SetUp]
        public void Setup()
        {
            _geoService = new GeoService();
        }

        [Test]
        public void Get_Coord_By_City_Name_Should_Return_Result()
        {
            var result = _geoService.GetGeoCoordinatesByCityName("London").Result;
            result.Should().BeOfType(typeof(GetGeoCoordResponseDTO));

            string expectedResultJson = @"{
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
            var expected = JsonSerializer.Deserialize<GetGeoCoordResponseDTO>(expectedResultJson, options);

            result.Should().BeEquivalentTo(expected);
        }
    }
}
