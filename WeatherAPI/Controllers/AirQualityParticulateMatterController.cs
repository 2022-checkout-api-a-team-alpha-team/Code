using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeatherAPI.DTOs;
using WeatherAPI.Services;

namespace WeatherAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirQualityParticulateMatterController : ControllerBase
    {
        private IAirQualityParticulateMatterService _service;
        public AirQualityParticulateMatterController(IAirQualityParticulateMatterService service)
        {
            _service = service;
        }

        [HttpGet("{cityName}")]
        public async Task<IActionResult> SuggestionsOnAirQualityParticulateMatterByCityName(string cityName)
        {
            SuggestionsOnAirQualityParticulateMatterDTO response;
            try
            {
                response = await _service.SuggestionsOnAirQualityParticulateMatterByCityName(cityName);
            }
            catch (Exception ex)
            {
                //throw;
                return BadRequest(ex.Message);
            }
            return Ok(response);
        }
    }
}