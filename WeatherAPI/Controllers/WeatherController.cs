using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("{latitude}/{longitude}")]
        public async Task<IActionResult> GetHourlyTemperatureByLatitudeAndLongitude(double latitude, double longitude)
        {
            var response = await _service.GetHourlyTemperatureByLatitudeAndLongitude(latitude,longitude);
            return Ok(response);
        }

        [HttpGet("{cityName}")]
        public async Task<IActionResult> GetHourlyTemperatureByCity(string cityName)
        {
            var response = await _service.GetHourlyTemperatureByCity(cityName);
            return Ok(response);
        }

        [HttpGet("feelsLikeTemperature/{cityName}")]
        public async Task<IActionResult> GetHourlyFeelsLikeTemperatureByCity(string cityName)
        {
            var response = await _service.GetHourlyFeelsLikeTemperatureByCity(cityName);
            return Ok(response);
        }
    }
}
  