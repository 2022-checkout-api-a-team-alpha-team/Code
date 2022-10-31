namespace WeatherAPI.DTOs.Pollen
{
    public class PollenDailyAggregatedDTO
    {
        public Dictionary<string, double> Pollens { get; } = new ();

        public string Message
        {
            get
            {
                List<string> pollenNames = new ();
                foreach (var keyValuePair in Pollens)
                {
                    if (keyValuePair.Value > 0)
                    {
                        pollenNames.Add(keyValuePair.Key);
                    }
                }
                string message = (pollenNames.Count > 0)
                    ? $"Be careful in case of allergies, the presence of pollen in the air is possible ({String.Join(",", pollenNames)})."
                    : "No pollen in the air.";
                return message;
            }
        }
    }
}
