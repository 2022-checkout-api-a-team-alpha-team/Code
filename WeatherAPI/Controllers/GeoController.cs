using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            var response = await _service.GetGeoCoordinatesByCityName(cityName);
            return Ok(response);
        }

    }
}
