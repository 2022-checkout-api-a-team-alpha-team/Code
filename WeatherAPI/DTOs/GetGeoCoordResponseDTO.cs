using Newtonsoft.Json;

namespace WeatherAPI.DTOs
{
    public class GetGeoCoordResponseDTO
    {
        [JsonProperty("results")]
        public List<GeoCoordDTO> Results { get; set; }

        [JsonProperty("generationtime_ms")]
        public double GenerationtimeMs { get; set; }
    }
}
