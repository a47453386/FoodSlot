using FoodSlot.Services.Interfaces;
using FoodSlot.DTOs;

using Newtonsoft.Json.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FoodSlot.Services
{
    public class APIResultService : IAPIResultService
    {
        private readonly HttpClient _httpClient;

        private readonly IConfiguration _configuration;

        public APIResultService(
            HttpClient httpClient,
            IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<List<StoreDTO>> NearbySearchAsync(
            NearbySearchRequestDTO request)
        {
            List<StoreDTO> stores = new();

            string apiKey =
                _configuration["GoogleApi:ApiKey"]!;

            string url =
                $"https://maps.googleapis.com/maps/api/place/nearbysearch/json" +
                $"?location={request.Latitude},{request.Longitude}" +
                $"&radius={request.Radius}" +
                $"&keyword={request.Keyword}" +
                $"&language=zh-TW" +
                $"&key={apiKey}";

            HttpResponseMessage response =
                await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                return stores;
            }

            string json =
                await response.Content.ReadAsStringAsync();

            JObject data = JObject.Parse(json);

            JArray? results =
                data["results"] as JArray;

            if (results == null)
            {
                return stores;
            }

            foreach (var item in results)
            {
                stores.Add(new StoreDTO
                {
                    Name =
                        item["name"]?.ToString()
                        ?? "",

                    Address =
                        item["vicinity"]?.ToString()
                        ?? "",

                    Rating =
                        item["rating"]?.Value<double>()
                        ?? 0,

                    Latitude =
                        item["geometry"]?["location"]?["lat"]
                        ?.Value<double>()
                        ?? 0,

                    Longitude =
                        item["geometry"]?["location"]?["lng"]
                        ?.Value<double>()
                        ?? 0
                });
            }

            return stores;
        }
    }
}