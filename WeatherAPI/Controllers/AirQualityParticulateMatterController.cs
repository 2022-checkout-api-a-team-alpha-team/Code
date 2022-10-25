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
        public async Task<IActionResult> GetAirQualityParticulateMatterByCityName(string cityName)
        {
            GetAirQualityParticulateMatterResponseDTO response;
            try
            {
                response = await _service.GetAirQualityParticulateMatterByCityName(cityName);
            }
            catch (Exception ex)
            {
                throw;
            }
            return Ok(response);
        }
    }
}