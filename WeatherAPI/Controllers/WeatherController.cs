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

        [HttpGet("WeeklyForecast/{cityName}")]
        public async Task<IActionResult> GetHourlyTemperatureByCity(string cityName)
        {
            if (string.IsNullOrEmpty(cityName))
                return BadRequest(ErrorHelper.PARAMETER_CANNOT_BE_NULL_OR_EMPTY);
            else
            {
                try
                {
                    var response = await _service.GetHourlyTemperatureByCity(cityName);
                    return Ok(response);
                }
                catch (Exception)
                {
                    return BadRequest(ErrorHelper.SERVER_ERROR);
                }
            }
        }

        [HttpGet("feelsLikeTemperature/{cityName}")]
        public async Task<IActionResult> GetHourlyFeelsLikeTemperatureByCity(string cityName)
        {
            if (String.IsNullOrEmpty(cityName))
                return BadRequest(ErrorHelper.PARAMETER_CANNOT_BE_NULL_OR_EMPTY);
            try
            {
                var response = await _service.GetHourlyFeelsLikeTemperatureByCity(cityName);
                return Ok(response);
            }
            catch (Exception)
            {
                return BadRequest(ErrorHelper.SERVER_ERROR);
            }
        }
    }
}
  