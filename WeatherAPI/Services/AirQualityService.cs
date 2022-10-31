using System.Globalization;
using System.Text.Json;
using WeatherAPI.DTOs;
using WeatherAPI.DTOs.Pollen;
using WeatherAPI.Helper;

namespace WeatherAPI.Services
{
    public class AirQualityService : IAirQualityService
    {
        private readonly HttpClient _httpClient;
        private readonly IGeoService _geoService;
        private readonly JsonSerializerOptions options = new ()
        {
            PropertyNameCaseInsensitive = true,
            AllowTrailingCommas = true,
            DictionaryKeyPolicy = JsonNamingPolicy.CamelCase
        };
        private const int HOURS_PER_DAY = 24;
        private const int DAYS_WINDOW = 5;

        public AirQualityService(HttpClient httpClient, IGeoService geoService)
        {
            _httpClient = httpClient;
            _geoService = geoService;
        }

        public async Task<GetPollenDTO> GetPollenData(string cityName)
        {
            var coordinates = await _geoService.GetGeoCoordinatesByCityName(cityName);

            if (coordinates.Results != null && coordinates.Results.Count > 0)
            {
                GeoCoordDTO coordDTO = coordinates.Results[0];
                var latitude = coordDTO.Latitude.ToString(CultureInfo.InvariantCulture);
                var longitude = coordDTO.Longitude.ToString(CultureInfo.InvariantCulture);
                var query = $"?latitude={latitude}&longitude={longitude}&hourly=alder_pollen,birch_pollen,grass_pollen,mugwort_pollen,olive_pollen,ragweed_pollen";

                var result = await _httpClient.GetFromJsonAsync<GetPollenDTO>(query, options);
                return result;
            }
            return null;
        }

        public async Task<PollenAggregatedDTO> GetPollenSuggestion(string cityName)
        {
            var result = new PollenAggregatedDTO();
            var pollenData = await GetPollenData(cityName);
            if (pollenData != null && pollenData.Hourly != null)
            {
                for (int i = 0; i < DAYS_WINDOW; i++)
                {
                    string key = DateTime.Parse(pollenData.Hourly.Time[i * HOURS_PER_DAY]).ToShortDateString();
                    var pollenDaily = new PollenDailyAggregatedDTO();
                    foreach (var keyValuePair in pollenData.Hourly.GetPollens())
                    {
                        pollenDaily.Pollens[keyValuePair.Key] = keyValuePair.Value
                            .Skip(i * HOURS_PER_DAY)
                            .Take(HOURS_PER_DAY)
                            .Select(v => v ?? 0)
                            .Max();
                    }
                    result.Items[key] = pollenDaily;
                }
            }
            return result;
        }

        //----------------------------------

        public async Task<GetAirQualityParticulateMatterResponseDTO> GetAirQualityParticulateMatterByCityName(string cityName)
        {
            var GeoCoordOfCity = await _geoService.GetGeoCoordinatesByCityName(cityName);
            double lat = GeoCoordOfCity.Results.ToList()[0].Latitude;
            double lon = GeoCoordOfCity.Results.ToList()[0].Longitude;

            var result = await _httpClient.GetFromJsonAsync<GetAirQualityParticulateMatterResponseDTO>($"?latitude={lat.ToString().Trim()}&longitude={lon.ToString().Trim()}&hourly=pm10,pm2_5", options);
            return result;
        }

        public async Task<SuggestionsOnAirQualityParticulateMatterDTO> SuggestionsOnAirQualityParticulateMatterByCityName(string cityName)
        {
            try
            {
                var AQPMResult = await GetAirQualityParticulateMatterByCityName(cityName);
                var totalHourCount = AQPMResult.Hourly.Time.Count;

                return AQPMSuggestionBuilder(AQPMResult, totalHourCount);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ex.TargetSite?.Name);
            }
        }

        public SuggestionsOnAirQualityParticulateMatterDTO AQPMSuggestionBuilder(GetAirQualityParticulateMatterResponseDTO AQPMResult, int totalHourCount)
        {
            var result = new SuggestionsOnAirQualityParticulateMatterDTO
            {
                Latitude = AQPMResult.Latitude,
                Longitude = AQPMResult.Longitude,
                UTC_Offset_Seconds = AQPMResult.UTC_Offset_Seconds,
                TimeZone = AQPMResult.TimeZone,
                TimeZone_Abbreviation = AQPMResult.TimeZone_Abbreviation,
                Daily_Suggestion = new AQPMDailySuggestionDTO
                {
                    Date = new List<string>(),
                    Midnight_Suggestion = new List<string>(),
                    Morning_Suggestion = new List<string>(),
                    Afternoon_Suggestion = new List<string>(),
                    Evening_Suggestion = new List<string>()
                }
            };

            for (int dayCount = 0; dayCount < totalHourCount; dayCount += 24)
            {
                result.Daily_Suggestion.Date.Add(AQPMResult.Hourly.Time[dayCount][..10]);
                for (int rangeOfHour = 0; rangeOfHour < RangeOfHour.DAY_DURATONS.GetLength(0); rangeOfHour++)
                {
                    double? rangeOfHourSumAvgPM25 = 0;
                    double? rangeOfHourSumAvgPM10 = 0;
                    bool nullRange = false;
                    for (int hourCount = dayCount + RangeOfHour.DAY_DURATONS[rangeOfHour, 0]; hourCount < dayCount + RangeOfHour.DAY_DURATONS[rangeOfHour, 0] + RangeOfHour.DAY_DURATONS[rangeOfHour, 1]; hourCount++)
                    {
                        if (AQPMResult.Hourly.PM2_5[hourCount] == null || AQPMResult.Hourly.PM10[hourCount] == null)
                        {
                            nullRange = true;
                            break;
                        }
                        rangeOfHourSumAvgPM25 += AQPMResult.Hourly.PM2_5[hourCount];
                        rangeOfHourSumAvgPM10 += AQPMResult.Hourly.PM10[hourCount];
                    }
                    if (nullRange)
                    {
                        switch (rangeOfHour)
                        {
                            case 0: result.Daily_Suggestion.Midnight_Suggestion.Add(AQPMSuggestionMessages.MSG_INSUFFICIENT_DATA); break;
                            case 1: result.Daily_Suggestion.Morning_Suggestion.Add(AQPMSuggestionMessages.MSG_INSUFFICIENT_DATA); break;
                            case 2: result.Daily_Suggestion.Afternoon_Suggestion.Add(AQPMSuggestionMessages.MSG_INSUFFICIENT_DATA); break;
                            case 3: result.Daily_Suggestion.Evening_Suggestion.Add(AQPMSuggestionMessages.MSG_INSUFFICIENT_DATA); break;
                        }
                    }
                    else
                    {
                        rangeOfHourSumAvgPM25 /= RangeOfHour.DAY_DURATONS[rangeOfHour, 1];
                        rangeOfHourSumAvgPM10 /= RangeOfHour.DAY_DURATONS[rangeOfHour, 1];
                        switch (rangeOfHour)
                        {
                            case 0: result.Daily_Suggestion.Midnight_Suggestion.Add(AQPMSuggestion(rangeOfHourSumAvgPM25, rangeOfHourSumAvgPM10)); break;
                            case 1: result.Daily_Suggestion.Morning_Suggestion.Add(AQPMSuggestion(rangeOfHourSumAvgPM25, rangeOfHourSumAvgPM10)); break;
                            case 2: result.Daily_Suggestion.Afternoon_Suggestion.Add(AQPMSuggestion(rangeOfHourSumAvgPM25, rangeOfHourSumAvgPM10)); break;
                            case 3: result.Daily_Suggestion.Evening_Suggestion.Add(AQPMSuggestion(rangeOfHourSumAvgPM25, rangeOfHourSumAvgPM10)); break;
                        };
                    }
                }
            }
            return result;
        }

        public string AQPMSuggestion(double? rangeOfHourAvgPM25, double? rangeOfHourAvgPM10)
        {
            int worseCaseAQPM25 = rangeOfHourAvgPM25 switch
            {
                < AQPMThreshold.PM_25_GOOD => 0,
                < AQPMThreshold.PM_25_MODERATE => 1,
                < AQPMThreshold.PM_25_UNHEALTHY_FOR_SENSITIVE_INDIVIDUALS => 2,
                < AQPMThreshold.PM_25_UNHEALTHY => 3,
                < AQPMThreshold.PM_25_VERY_UNHEALTHY => 4,
                < AQPMThreshold.PM_25_HAZARDOUS => 5,
                < AQPMThreshold.PM_25_VERY_HAZARDOUS => 6,
                _ => 7,
            };

            int worseCaseAQPM10 = rangeOfHourAvgPM10 switch
            {
                < AQPMThreshold.PM_10_GOOD => 0,
                < AQPMThreshold.PM_10_MODERATE => 1,
                < AQPMThreshold.PM_10_UNHEALTHY_FOR_SENSITIVE_INDIVIDUALS => 2,
                < AQPMThreshold.PM_10_UNHEALTHY => 3,
                < AQPMThreshold.PM_10_VERY_UNHEALTHY => 4,
                < AQPMThreshold.PM_10_HAZARDOUS => 5,
                < AQPMThreshold.PM_10_VERY_HAZARDOUS => 6,
                _ => 7,
            };
            int worseCaseAQPM = worseCaseAQPM25 > worseCaseAQPM10 ? worseCaseAQPM25 : worseCaseAQPM10;

            return worseCaseAQPM switch
            {
                < 1 => AQPMSuggestionMessages.MSG_GOOD,
                < 2 => AQPMSuggestionMessages.MSG_MODERATE,
                < 3 => AQPMSuggestionMessages.MSG_UNHEALTHY_FOR_SENSITIVE_INDIVIDUALS,
                < 4 => AQPMSuggestionMessages.MSG_UNHEALTHY,
                < 5 => AQPMSuggestionMessages.MSG_VERY_UNHEALTHY,
                < 6 => AQPMSuggestionMessages.MSG_HAZARDOUS,
                < 7 => AQPMSuggestionMessages.MSG_VERY_HAZARDOUS,
                _ => AQPMSuggestionMessages.MSG_EXTREMELY_HAZARDOUS,
            };
        }
    }
}
