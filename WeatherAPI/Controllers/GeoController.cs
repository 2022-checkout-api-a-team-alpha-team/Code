using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeatherAPI.Helper;
using WeatherAPI.Services;

namespace WeatherAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeoController : ControllerBase
    {
        private IGeoService _service;
        public GeoController(IGeoService service)
        {
            _service = service;
        }

        /// <summary>
        /// This API returns the object that contains the longitude and latitude information.
        /// </summary>
        /// <param name="cityName">Insert city name.</param>
        /// <returns>Returns an object having the longitude and latitude information.</returns>
        /// <remarks>
        /// Sample Result:
        ///
        ///     GET /London
        ///     {
        ///         "results": [
        ///             "id": 2643743,
        ///             "name": "London",
        ///             "latitude": 51.50853,
        ///             "longitude": -0.12574,
        ///             "elevation": 25,
        ///             "featureCode": null,
        ///             "countryCode": null,
        ///             "admin1Id": 0,
        ///             "admin2Id": 0,
        ///             "timezone": "Europe/London",
        ///             "population": 7556900,
        ///             "countryId": 0,
        ///             "country": "United Kingdom",
        ///             "admin1": "England",
        ///             "admin2": "Greater London"
        ///           }
        ///         ],
        ///         "generationtimeMs": 0
        ///     }
        /// </remarks>
        /// <response code="200">Returns Request successfully.</response>
        [HttpGet("{cityName}")]
        public async Task<IActionResult> GetGeoCoordinatesByCityName(string cityName)
        {
            try
            {
                if (String.IsNullOrEmpty(cityName))
                {
                    return BadRequest(ErrorHelper.PARAMETER_CANNOT_BE_NULL_OR_EMPTY);
                }
                var response = await _service.GetGeoCoordinatesByCityName(cityName);
                return Ok(response);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error:" + e.Message);
                return BadRequest(ErrorHelper.SERVER_ERROR);
            }
        }

    }
}
