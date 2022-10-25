using System.Text.Json.Serialization;

namespace WeatherAPI.DTOs
{
    public class GetHourlyPollenDTO
    {
        private readonly IDictionary<string, List<double?>> _pollens = new Dictionary<string, List<double?>>();

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
