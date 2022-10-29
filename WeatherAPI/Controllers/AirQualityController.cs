using Microsoft.AspNetCore.Mvc;
using WeatherAPI.Services;
using WeatherAPI.Helper;
using WeatherAPI.DTOs;

namespace WeatherAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AirQualityController : ControllerBase
    {
        private readonly IAirQualityService _service;
        public AirQualityController(IAirQualityService service)
        {
            _service = service;
        }


        /// <summary>
        /// This API returns the object that contains the suggesttion based on presence of pollens in the air.
        /// </summary>
        /// <param name="cityName">Insert city name.</param>
        /// <returns>Returns 5 days pollen forecast.</returns>
        [HttpGet("Pollen/{cityName}")]
        public async Task<IActionResult> GetPollenSuggestion(string cityName)
        {
            try
            {
                if (String.IsNullOrEmpty(cityName))
                    return BadRequest(ErrorHelper.PARAMETER_CANNOT_BE_NULL_OR_EMPTY);

                var response = await _service.GetPollenSuggestion(cityName);
                return Ok(response);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error:" + e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ErrorHelper.SERVER_ERROR);
            }
        }

        /// <summary>
        /// This API returns 5 days of suggestions based on air quality particulate matter in midnight, morning, afternoon and evening everyday.
        /// </summary>
        /// <param name="cityName">Insert city name.</param>
        /// <returns>Returns an object having suggestions for the air quality conditions based on the particulate matter in 
        /// midnight, morning, afternoon and evening everyday for next 5 days in a list of objects.</returns>
        /// <remarks>
        /// Sample results:
        /// 
        ///     Get /Kuala Lumpur
        ///     {
        ///     "latitude": 3.2000046,
        ///     "longitude": 101.600006,
        ///         "utC_Offset_Seconds": 0,
        ///         "timeZone": "GMT",
        ///         "timeZone_Abbreviation": "GMT",
        ///         "daily_Suggestion": {
        ///             "date": [
        ///                 "2022-10-27",
        ///                 "2022-10-28",
        ///                 "2022-10-29",
        ///                 "2022-10-30",
        ///                 "2022-10-31"
        ///                 ],
        ///             "midnight_Suggestion": [
        ///                 "The air quality is usual.",
        ///                 "The air quality is usual.",
        ///                 "The air quality is usual.",
        ///                 "The air quality is usual.",
        ///                 "The air quality is usual."
        ///                 ],
        ///             "morning_Suggestion": [
        ///                 "The air quality is usual.",
        ///                 "The air quality is usual.",
        ///                 "The air quality is usual.",
        ///                 "The air quality is usual.",
        ///                 "The air quality is usual."
        ///                 ],
        ///             "afternoon_Suggestion": [
        ///                 "The air quality is usual.",
        ///                 "The air quality is usual.",
        ///                 "The air quality is not very good for air-sensitive individuals.",
        ///                 "The air quality is not very good for air-sensitive individuals.",
        ///                 "The air quality is not very good for air-sensitive individuals."
        ///                 ],
        ///             "evening_Suggestion": [
        ///                 "The air quality is usual.",
        ///                 "The air quality is usual.",
        ///                 "The air quality is usual.",
        ///                 "The air quality is not very good for air-sensitive individuals.",
        ///                 "The air quality is usual."
        ///                 ]
        ///         }
        ///     }
        /// </remarks>
        /// <response code="200">Returns Request successfully.</response>
        [HttpGet("ParticulateMatter/{cityName}")]
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
