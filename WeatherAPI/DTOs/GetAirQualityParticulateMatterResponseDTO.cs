using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace WeatherAPI.DTOs
{
    public class GetAirQualityParticulateMatterResponseDTO
    {
        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [JsonProperty("longitude")]
        public double Longitude { get; set; }

        [JsonProperty("generationtime_ms")]
        public double Generationtime_Ms { get; set; }

        [JsonProperty("utc_offset_seconds")]
        public int UTC_Offset_Seconds { get; set; }

        [JsonProperty("timezone")]
        public string TimeZone { get; set; }

        [JsonProperty("timezone_abbreviation")]
        public string TimeZone_Abbreviation { get; set; }

        [JsonProperty("hourly_units")]
        public AQPMHourlyUnitsDTO Hourly_Units { get; set; }

        [JsonProperty("hourly")]
        public AQPMHourlyDTO Hourly { get; set; }
    }

    public class AQPMHourlyUnitsDTO
    {
        [JsonProperty("time")]
        public string Time { get; set; }

        [JsonProperty("pm10")]
        public string PM10 { get; set; }

        [JsonProperty("pm2_5")]
        public string PM2_5 { get; set; }
    }

    public class AQPMHourlyDTO
    {
        [JsonProperty("time")]
        public List<string> Time { get; set; }

        [JsonProperty("pm10")]
        public List<int?> PM10 { get; set; }

        [JsonProperty("pm2_5")]
        public List<int?> PM2_5 { get; set; }
    }
}
