using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeatherAPI.Helper;
using WeatherAPI.Services;

namespace WeatherAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private IWeatherService _service;
        
        public WeatherController(IWeatherService service)
        {
            _service = service;
        }

        [HttpGet("hourlyTemperature/{cityName}")]
        public async Task<IActionResult> GetHourlyTemperatureByCity(string cityName)
        {
            var response = await _service.GetHourlyTemperatureByCity(cityName);
            return Ok(response);
        }

        [HttpGet("feelsLikeTemperature/{cityName}")]
        public async Task<IActionResult> GetHourlyFeelsLikeTemperatureByCity(string cityName)
        {
            try
            {
                var response = await _service.GetHourlyFeelsLikeTemperatureByCity(cityName);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(ErrorHelper.SERVER_ERROR);
            }
        }
    }
}
  