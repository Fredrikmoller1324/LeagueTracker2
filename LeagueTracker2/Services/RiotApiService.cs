using LeagueTracker2.Data.Models;
using LeagueTracker2.HelperClasses;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace LeagueTracker2.Services
{
    public class RiotApiService
    {
        private readonly HttpClient _httpClient;

        public RiotApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private void SetHeaders(HttpRequestMessage request)
        {
            request.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/111.0.0.0 Safari/537.36");
            request.Headers.Add("Accept-Language", "sv-SE,sv;q=0.9,en-US;q=0.8,en;q=0.7");
            request.Headers.Add("Origin", "https://developer.riotgames.com");
        }

        public async Task<string> GetPuuidBySummonerName(string summonerName)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://euw1.api.riotgames.com/lol/summoner/v4/summoners/by-name/{summonerName}?api_key=RGAPI-711ce0a5-eb40-417d-8a21-be40764add25");

            SetHeaders(request);

            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to get Puuid for summoner {summonerName}. Error code: {response.StatusCode}");
            }

            var responseString = await response.Content.ReadAsStringAsync();
            var summoner = JsonConvert.DeserializeObject<Summoner>(responseString);

            return summoner.Puuid;
        }

        public async Task<List<string>> GetMatchIdsByPuuid(string puuid)
        {
            // Create the query parameters
            var queryParameters = new List<string>();
            queryParameters.Add($"count=100");

            var startTime = EpochTimeHelper.GetStartTimeSevenDaysAgo();
            var endTime = EpochTimeHelper.GetCurrentEpochTime();

            if (startTime != 0)
            {
                queryParameters.Add($"startTime={startTime}");
            }

            if (endTime != 0)
            {
                queryParameters.Add($"endTime={endTime}");
            }

            var queryString = string.Join("&", queryParameters);

            // Create the request
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://europe.api.riotgames.com/lol/match/v5/matches/by-puuid/{puuid}/ids?{queryString}&api_key=RGAPI-711ce0a5-eb40-417d-8a21-be40764add25");

            SetHeaders(request);

            // Send the request
            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to get match IDs. Status code: {response.StatusCode}");
            }

            var responseContent = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<string>>(responseContent);
        }
    }
}
