namespace WeatherAPI.DTOs.Pollen
{
    public class PollenAggregatedDTO
    {
        public Dictionary<string, PollenDailyAggregatedDTO> Items { get; } = new ();
    }
}
