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
        public HourlyUnitsDTO Hourly_Units { get; set; }

        [JsonProperty("hourly")]
        public HourlyDTO Hourly { get; set; }
    }

    public class HourlyUnitsDTO
    {
        [JsonProperty("time")]
        public string Time { get; set; }

        [JsonProperty("pm10")]
        public string PM10 { get; set; }

        [JsonProperty("pm2_5")]
        public string PM2_5 { get; set; }
    }

    public class HourlyDTO
    {
        [JsonProperty("time")]
        public List<string> Time { get; set; }

        [JsonProperty("pm10")]
        public List<int?> PM10 { get; set; }

        [JsonProperty("pm2_5")]
        public List<int?> PM2_5 { get; set; }

    }



    //Pollen section
    public class HourlyPollenDTO
    {
        private readonly IDictionary<string, List<double?>> _pollens;

        public List<string> Time { get; set; }

        [JsonPropertyName("alder_pollen")]
        public List<double?> AlderPollen
        {
            get => _pollens["Alder"];
            set => _pollens["Alder"] = value;
        }

        [JsonPropertyName("birch_pollen")]
        public List<double?> BirchPollen
        {
            get => _pollens["Birch"];
            set => _pollens["Birch"] = value;
        }

        [JsonPropertyName("grass_pollen")]
        public List<double?> GrassPollen
        {
            get => _pollens["Grass"];
            set => _pollens["Grass"] = value;
        }

        [JsonPropertyName("mugwort_pollen")]
        public List<double?> MugwortPollen
        {
            get => _pollens["Mugwort"];
            set => _pollens["Mugwort"] = value;
        }

        [JsonPropertyName("olive_pollen")]
        public List<double?> OlivePollen
        {
            get => _pollens["Olive"];
            set => _pollens["Olive"] = value;
        }

        [JsonPropertyName("ragweed_pollen")]
        public List<double?> RagweedPollen
        {
            get => _pollens["Ragweed"];
            set => _pollens["Ragweed"] = value;
        }

        public IDictionary<string, List<double?>> GetPollens() => _pollens;
    }
}
