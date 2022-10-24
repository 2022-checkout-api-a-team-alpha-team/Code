using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace WeatherAPI.DTOs
{
    public class GetPollenDTO
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
        public AQPHourlyUnitsDTO Hourly_Units { get; set; }

        [JsonProperty("hourly")]
        public AQPHourlyDTO Hourly { get; set; }
    }

    public class AQPHourlyUnitsDTO
    {
        [JsonProperty("time")]
        public string Time { get; set; }

        [JsonProperty("alder_pollen")]
        public string Alder_Pollen { get; set; }

        [JsonProperty("birch_pollen")]
        public string Birch_Pollen { get; set; }

        [JsonProperty("grass_pollen")]
        public string Grass_Pollen { get; set; }

        [JsonProperty("mugwort_pollen")]
        public string Mugwort_Pollen { get; set; }

        [JsonProperty("olive_pollen")]
        public string Olive_Pollen { get; set; }

        [JsonProperty("ragweed_pollen")]
        public string Ragweed_Pollen { get; set; }
    }
    public class AQPHourlyDTO
    {
        [JsonProperty("time")]
        public List<string> Time { get; set; }

        //Pollen section
        [JsonPropertyName("alder_pollen")]
        public List<double?> AlderPollen { get; set; }

        [JsonPropertyName("birch_pollen")]
        public List<double?> BirchPollen { get; set; }

        [JsonPropertyName("grass_pollen")]
        public List<double?> GrassPollen { get; set; }

        [JsonPropertyName("mugwort_pollen")]
        public List<double?> MugwortPollen { get; set; }

        [JsonPropertyName("olive_pollen")]
        public List<double?> OlivePollen { get; set; }

        [JsonPropertyName("ragweed_pollen")]
        public List<double?> RagweedPollen { get; set; }

    }
}
