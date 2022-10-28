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