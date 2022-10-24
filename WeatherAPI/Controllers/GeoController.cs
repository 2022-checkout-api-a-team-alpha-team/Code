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
