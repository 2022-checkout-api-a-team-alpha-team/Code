using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace WeatherAPI.DTOs
{
    public class GetPollenDTO
    {
        public GetHourlyPollenDTO Hourly { get; set; }
    }
}
