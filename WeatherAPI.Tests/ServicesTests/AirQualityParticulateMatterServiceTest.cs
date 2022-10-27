using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WeatherAPI.Controllers;
using WeatherAPI.DTOs;
using WeatherAPI.Services;

namespace WeatherAPI.Tests.ServicesTests
{
    public class AirQualityParticulateMatterServiceTest
    {
        private AirQualityParticulateMatterService _airQualityParticulateMatterService;
        private readonly JsonSerializerOptions options = new()
        {
            PropertyNameCaseInsensitive = true,
            AllowTrailingCommas = true,
            DictionaryKeyPolicy = JsonNamingPolicy.CamelCase
        };
        private readonly string expectedGetResultJson = @"{
            ""latitude"": 3.2000046,
            ""longitude"": 101.600006,
            ""generationtime_ms"": 5.517005920410156,
            ""utc_offset_seconds"": 0,
            ""timezone"": ""GMT"",
            ""timezone_abbreviation"": ""GMT"",
            ""hourly_units"": {
                ""time"": ""iso8601"",
                ""pm10"": ""μg/m³"",
                ""pm2_5"": ""μg/m³""
                },
            ""hourly"": {
                ""time"": [
                    ""2022-10-22T00:00"", ""2022-10-22T01:00"", ""2022-10-22T02:00"", ""2022-10-22T03:00"", ""2022-10-22T04:00"", ""2022-10-22T05:00"", ""2022-10-22T06:00"", ""2022-10-22T07:00"", ""2022-10-22T08:00"", ""2022-10-22T09:00"",
                    ""2022-10-22T10:00"", ""2022-10-22T11:00"", ""2022-10-22T12:00"", ""2022-10-22T13:00"", ""2022-10-22T14:00"", ""2022-10-22T15:00"", ""2022-10-22T16:00"", ""2022-10-22T17:00"", ""2022-10-22T18:00"", ""2022-10-22T19:00"",
                    ""2022-10-22T20:00"", ""2022-10-22T21:00"", ""2022-10-22T22:00"", ""2022-10-22T23:00"", ""2022-10-23T00:00"", ""2022-10-23T01:00"", ""2022-10-23T02:00"", ""2022-10-23T03:00"", ""2022-10-23T04:00"", ""2022-10-23T05:00"",
                    ""2022-10-23T06:00"", ""2022-10-23T07:00"", ""2022-10-23T08:00"", ""2022-10-23T09:00"", ""2022-10-23T10:00"", ""2022-10-23T11:00"", ""2022-10-23T12:00"", ""2022-10-23T13:00"", ""2022-10-23T14:00"", ""2022-10-23T15:00"",
                    ""2022-10-23T16:00"", ""2022-10-23T17:00"", ""2022-10-23T18:00"", ""2022-10-23T19:00"", ""2022-10-23T20:00"", ""2022-10-23T21:00"", ""2022-10-23T22:00"", ""2022-10-23T23:00"", ""2022-10-24T00:00"", ""2022-10-24T01:00"",
                    ""2022-10-24T02:00"", ""2022-10-24T03:00"", ""2022-10-24T04:00"", ""2022-10-24T05:00"", ""2022-10-24T06:00"", ""2022-10-24T07:00"", ""2022-10-24T08:00"", ""2022-10-24T09:00"", ""2022-10-24T10:00"", ""2022-10-24T11:00"",
                    ""2022-10-24T12:00"", ""2022-10-24T13:00"", ""2022-10-24T14:00"", ""2022-10-24T15:00"", ""2022-10-24T16:00"", ""2022-10-24T17:00"", ""2022-10-24T18:00"", ""2022-10-24T19:00"", ""2022-10-24T20:00"", ""2022-10-24T21:00"",
                    ""2022-10-24T22:00"", ""2022-10-24T23:00"", ""2022-10-25T00:00"", ""2022-10-25T01:00"", ""2022-10-25T02:00"", ""2022-10-25T03:00"", ""2022-10-25T04:00"", ""2022-10-25T05:00"", ""2022-10-25T06:00"", ""2022-10-25T07:00"",
                    ""2022-10-25T08:00"", ""2022-10-25T09:00"", ""2022-10-25T10:00"", ""2022-10-25T11:00"", ""2022-10-25T12:00"", ""2022-10-25T13:00"", ""2022-10-25T14:00"", ""2022-10-25T15:00"", ""2022-10-25T16:00"", ""2022-10-25T17:00"",
                    ""2022-10-25T18:00"", ""2022-10-25T19:00"", ""2022-10-25T20:00"", ""2022-10-25T21:00"", ""2022-10-25T22:00"", ""2022-10-25T23:00"", ""2022-10-26T00:00"", ""2022-10-26T01:00"", ""2022-10-26T02:00"", ""2022-10-26T03:00"",
                    ""2022-10-26T04:00"", ""2022-10-26T05:00"", ""2022-10-26T06:00"", ""2022-10-26T07:00"", ""2022-10-26T08:00"", ""2022-10-26T09:00"", ""2022-10-26T10:00"", ""2022-10-26T11:00"", ""2022-10-26T12:00"", ""2022-10-26T13:00"",
                    ""2022-10-26T14:00"", ""2022-10-26T15:00"", ""2022-10-26T16:00"", ""2022-10-26T17:00"", ""2022-10-26T18:00"", ""2022-10-26T19:00"", ""2022-10-26T20:00"", ""2022-10-26T21:00"", ""2022-10-26T22:00"", ""2022-10-26T23:00""
                    ],
                ""pm10"": [
                    16, 14, 13, 14, 13, 14, 15, 13, 13, 13, 12, 11, 10, 9, 9, 8, 8, 8, 8, 8, 9, 10, 9, 9, 9, 9, 8, 8, 6, 7, 8, 9, 9, 9, 8, 8, 9, 8, 8, 8,
                    9, 8, 9, 10, 12, 14, 14, 14, 17, 17, 17, 17, 19, 19, 19, 18, 16, 16, 16, 17, 18, 17, 17, 17, 17, 16, 14, 13, 14, 16, 14, 13, 12, 4, 4, 5, 5, 5, 6, 7,
                    9, 8, 9, 10, 10, 10, 11, 10, 10, 12, 12, 12, 11, 10, 10, 10, 10, 11, 12, 12, 12, 12, 11, 10, 8, 7, 6, 7, 7, null, null, null, null, null, null, null, null, null, null, null
                    ],
                ""pm2_5"": [
                    4, 4, 4, 4, 4, 4, 5, 5, 5, 5, 5, 4, 4, 5, 4, 4, 4, 4, 4, 5, 5, 4, 5, 5, 5, 5, 5, 5, 4, 3, 4, 3, 3, 4, 4, 4, 4, 4, 4, 4,
                    3, 3, 4, 4, 5, 5, 5, 5, 5, 5, 5, 6, 6, 5, 4, 4, 4, 4, 5, 5, 5, 5, 5, 5, 5, 5, 4, 4, 5, 5, 5, 5, 5, 3, 3, 3, 3, 3, 4, 5,
                    6, 5, 6, 6, 6, 7, 7, 6, 7, 8, 8, 8, 7, 7, 7, 7, 7, 8, 8, 8, 8, 8, 8, 6, 6, 4, 4, 4, 5, null, null, null, null, null, null, null, null, null, null, null
                    ]
                }
            }";
        private readonly string expectedSuggestionResultJson = @"{
            ""latitude"": 3.2000046,
            ""longitude"": 101.600006,
            ""utc_offset_seconds"": 0,
            ""timezone"": ""GMT"",
            ""timezone_abbreviation"": ""GMT"",
            ""daily_suggestion"": {
                ""date"": [
                    ""2022-10-22T00:00"", ""2022-10-22T06:00"", ""2022-10-22T12:00"", ""2022-10-22T18:00"",
                    ""2022-10-23T00:00"", ""2022-10-23T06:00"", ""2022-10-23T12:00"", ""2022-10-23T18:00"",
                    ""2022-10-24T00:00"", ""2022-10-24T06:00"", ""2022-10-24T12:00"", ""2022-10-24T18:00"",
                    ""2022-10-25T00:00"", ""2022-10-25T06:00"", ""2022-10-25T12:00"", ""2022-10-25T18:00"",
                    ""2022-10-26T00:00"", ""2022-10-26T06:00"", ""2022-10-26T12:00"", ""2022-10-26T18:00""
                    ],
                ""midnight_suggestion"": [
                    """", """", """", """",
                    """", """", """", """",
                    """", """", """", """",
                    """", """", """", """",
                    """", """", """", """"
                    ],
                ""morning_suggestion"": [
                    """", """", """", """",
                    """", """", """", """",
                    """", """", """", """",
                    """", """", """", """",
                    """", """", """", """"
                    ],
                ""afternoon_suggestion"": [
                    """", """", """", """",
                    """", """", """", """",
                    """", """", """", """",
                    """", """", """", """",
                    """", """", """", """"
                    ],
                ""evening_suggestion"": [
                    """", """", """", """",
                    """", """", """", """",
                    """", """", """", """",
                    """", """", """", """",
                    """", """", """", """"
                    ]
                }
            }";


        [SetUp]
        public void Setup()
        {
            _airQualityParticulateMatterService = new AirQualityParticulateMatterService();
        }

        [Test]
        public void Get_Air_Quality_Particulate_Matter_By_City_Name_Should_Return_Result_And_In_Correct_Type()
        {
            // Arrange
            var expected = JsonSerializer.Deserialize<GetAirQualityParticulateMatterResponseDTO>(expectedGetResultJson, options);

            // Act
            var result = _airQualityParticulateMatterService.GetAirQualityParticulateMatterByCityName("Kuala Lumpur").Result;

            //Assert
            result.Should().BeOfType(typeof(GetAirQualityParticulateMatterResponseDTO));
            result.Should().BeEquivalentTo(expected, options => options
                .Including(flds => flds.Latitude)
                .Including(flds => flds.Longitude)
                .Including(flds => flds.TimeZone)
                .Including(flds => flds.TimeZone_Abbreviation)
                .Including(flds => flds.Hourly_Units.Time)
                .Including(flds => flds.Hourly_Units.PM10)
                .Including(flds => flds.Hourly_Units.PM2_5)
                );
        }

        [Test]
        public void Suggestions_On_Air_Quality_Particular_Matter_Should_Return_Result_And_In_Correct_Type()
        {
            // Arrange
            var expected = JsonSerializer.Deserialize<SuggestionsOnAirQualityParticulateMatterDTO>(expectedSuggestionResultJson, options);

            // Act
            var result = _airQualityParticulateMatterService.SuggestionsOnAirQualityParticulateMatter("Kuala Lumpur").Result;

            //Assert
            result.Should().BeOfType(typeof(SuggestionsOnAirQualityParticulateMatterDTO));
            result.Should().BeEquivalentTo(expected, options => options
                .Including(flds => flds.Latitude)
                .Including(flds => flds.Longitude)
                .Including(flds => flds.TimeZone)
                .Including(flds => flds.TimeZone_Abbreviation)
                );
        }
    }
}