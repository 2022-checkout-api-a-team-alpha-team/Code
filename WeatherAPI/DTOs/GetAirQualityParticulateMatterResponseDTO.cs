using Newtonsoft.Json;

namespace WeatherAPI.DTOs
{
    public class GetAirQualityParticulateMatterResponseDTO
    {
        [JsonProperty("latitude")]//3.2000046
        public double Latitude { get; set; }

        [JsonProperty("longitude")]//101.600006
        public double Longitude { get; set; }

        [JsonProperty("generationtime_ms")]//0.23102760314941406
        public double GenerationtimeMs { get; set; }

        [JsonProperty("utc_offset_seconds")]//0
        public int UTCOffsetSeconds { get; set; }

        [JsonProperty("timezone")]//"GMT"
        public string TimeZone { get; set; }

        [JsonProperty("timezone_abbreviation")]//"GMT"
        public string TimeZoneAbbreviation { get; set; }

        [JsonProperty("hourly_units")]//<HourlyUnitsDTO>
        public HourlyUnitsDTO HourlyUnits { get; set; }

        [JsonProperty("hourly")]//<HourlyDTO>
        public HourlyDTO Hourly { get; set; }
    }

    public class HourlyUnitsDTO
    {
        [JsonProperty("time")]//"iso8601"
        public string Time { get; set; }

        [JsonProperty("pm10")]//"μg/m³"
        public string PM10 { get; set; }

        [JsonProperty("pm2_5")]//"μg/m³"
        public string PM2_5 { get; set; }
    }

    public class HourlyDTO
    {
        [JsonProperty("time")]//[1 to 120]"2022-10-25T04:00"
        public List<string> Time { get; set; }

        [JsonProperty("pm10")]//[1 to 120]83
        public List<int?> PM10 { get; set; }

        [JsonProperty("pm2_5")]//[1 to 120]57
        public List<int?> PM2_5 { get; set; }
    }
}
