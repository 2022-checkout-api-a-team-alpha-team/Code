using Microsoft.AspNetCore.Mvc;
using WeatherAPI.Services;

namespace WeatherAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AirQualityPollenController : ControllerBase
    {
        private IAirQualityPollenService _service;
        public AirQualityPollenController(IAirQualityPollenService service)
        {
            _service = service;
        }


        [HttpGet("{cityName}")]
        public async Task<IActionResult> GetPollenData(string cityName)
        {
            var response = await _service.GetPollenData(cityName);
            return Ok(response);
        }
    }
}
