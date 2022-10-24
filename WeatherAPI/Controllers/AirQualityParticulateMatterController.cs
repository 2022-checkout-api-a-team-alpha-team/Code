using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeatherAPI.Services;

namespace WeatherAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirQualityParticulateMatterController : ControllerBase
    {
        public IAirQualityParticulateMatterService _service;
        public AirQualityParticulateMatterController(IAirQualityParticulateMatterService service)
        {
            _service = service;
        }
        
        [HttpGet("{cityName}")]
        public async Task<IActionResult> GetAirQualityParticulateMatterByCityName(string cityName)
        {
            var response = await _service.GetAirQualityParticulateMatterByCityName(cityName);
            return Ok(response);
        }
    }
}