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
        
        [HttpGet("/data/{cityName}")]
        public async Task<IActionResult> GetAirQualityParticulateMatterByCityName(string cityName)
        {
            GetAirQualityParticulateMatterResponseDTO response;
            try
            {
                response = await _service.GetAirQualityParticulateMatterByCityName(cityName);
            }
            catch (Exception ex)
            {
                //throw;
                return BadRequest(ex.Message);
            }
            return Ok(response);
        }

        [HttpGet("/suggestion/{cityName}")]
        public async Task<IActionResult> SuggestionsOnAirQualityParticulateMatter(string cityName)
        {
            SuggestionsOnAirQualityParticulateMatterDTO response;
            try
            {
                response = await _service.SuggestionsOnAirQualityParticulateMatter(cityName);
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