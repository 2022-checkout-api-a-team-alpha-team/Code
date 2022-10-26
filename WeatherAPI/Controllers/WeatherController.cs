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

        /// <summary>
        /// This API returns Weekly Forecast and Suggestions based on City's HourlyTemperature
        /// </summary>
        /// <param name="cityName"></param>
        /// <returns> 7 days Forecast with Suggestions and Forecast - HourlyTempForeCastAndSuggestionsDTO - Day,Date, AverageTemperature and Suggestions</returns>
        /// <response code="200">Returns Request successfully.</response>
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

        [HttpGet("FeelsLikeTemperature/{cityName}")]
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

        /// <summary>
        /// This API return suggestions for what to wear outside based on the weather
        /// for next 7 days.
        /// </summary>
        /// <param name="cityName">Insert city name.</param>
        /// <returns>Returns an object having suggestions for what to wear outside based on the weather
        /// for next 7 days in a list of objects.</returns>
        /// <remarks>
        /// Sample Result:
        ///
        ///     GET /London
        ///     {
        ///      "latitude": 51.5,
        ///      "longitude": -0.120000124,
        ///      "elevation": 23,
        ///      "utcOffsetSeconds": 3600,
        ///      "timezone": "Europe/London",
        ///      "timezoneAbbreviation": "BST",
        ///      "weatherSuggestions": [
        ///        {
        ///          "time": "2022-10-26",
        ///          "maxTemperature": 18.8,
        ///          "minTemperature": 14.4,
        ///          "suggestion": "It is overcast. Better to wear a jumper/ a jacket to avoid any chills."
        ///        },
        ///        {
        ///          "time": "2022-10-27",
        ///          "maxTemperature": 19.6,
        ///          "minTemperature": 14,
        ///          "suggestion": "There are light rain showers. Better to wear a jumper/ a jacket to avoid any chills."
        ///        },
        ///        {
        ///        "time": "2022-10-28",
        ///          "maxTemperature": 17.9,
        ///          "minTemperature": 12.6,
        ///          "suggestion": "There is slight rain. Better to wear a jumper/ a jacket to avoid any chills."
        ///        },
        ///        {
        ///        "time": "2022-10-29",
        ///          "maxTemperature": 18.5,
        ///          "minTemperature": 12.6,
        ///          "suggestion": "There is morderate rain. Better to wear a jumper/ a jacket to avoid any chills."
        ///        },
        ///        {
        ///        "time": "2022-10-30",
        ///          "maxTemperature": 17.8,
        ///          "minTemperature": 12.8,
        ///          "suggestion": "It is overcast. Better to wear a jumper/ a jacket to avoid any chills."
        ///        },
        ///        {
        ///        "time": "2022-10-31",
        ///          "maxTemperature": 17.1,
        ///          "minTemperature": 12.6,
        ///          "suggestion": "It is overcast. Better to wear a jumper/ a jacket to avoid any chills."
        ///        },
        ///        {
        ///        "time": "2022-11-01",
        ///          "maxTemperature": 17.4,
        ///          "minTemperature": 12,
        ///          "suggestion": "There is slight rain. Better to wear a jumper/ a jacket to avoid any chills."
        ///        }
        ///      ]
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns Request successfully.</response>
        [HttpGet("GetSuggestionsBasedOnWeather/{cityName}")]
        public async Task<IActionResult> GetSuggestionsBasedOnWeather(string cityName)
        {
            try
            {
                if (String.IsNullOrEmpty(cityName))
                {
                    return BadRequest(ErrorHelper.PARAMETER_CANNOT_BE_NULL_OR_EMPTY);
                }
                var response = await _service.GetSuggestionsBasedOnCurrentWeather(cityName);
                return Ok(response);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error:" + e.Message);
                return BadRequest(ErrorHelper.SERVER_ERROR);
            }
        }
    }
}
  