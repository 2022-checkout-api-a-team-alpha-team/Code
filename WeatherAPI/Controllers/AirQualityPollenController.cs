using Microsoft.AspNetCore.Mvc;
using WeatherAPI.Services;
using WeatherAPI.Helper;

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
            try
            {
                if (String.IsNullOrEmpty(cityName))
                    return BadRequest(ErrorHelper.PARAMETER_CANNOT_BE_NULL_OR_EMPTY);


                var response = await _service.GetPollenData(cityName);
                return Ok(response);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error:" + e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ErrorHelper.SERVER_ERROR);
            }
        }
    }
}
