namespace WeatherAPI.DTOs.Pollen
{
    public class PollenDailyAggregatedDTO
    {
        public Dictionary<string, double> Pollens { get; } = new ();
        private string NO_POLLEN_SUGGESTION = "No pollen in the air. You can safely go for a walk in nature.";
        private string POLLEN_SUGGESTION = $"Be careful in case of allergies, the presence of pollen in the air is possible ({0}). Do not forget to take anti allergic medicine.";

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
                    ? String.Format(POLLEN_SUGGESTION, String.Join(",", pollenNames))
                    : NO_POLLEN_SUGGESTION;
                return message;
            }
        }
    }
}
