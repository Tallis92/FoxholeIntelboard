using IntelboardAPI.Models;
using IntelboardAPI.DTO;
using System.Text.Json;

namespace FoxholeIntelboard.DAL
{
    public class CraftableItemManager : ICraftableItemManager
    {
        private readonly HttpClient _httpClient;

        public CraftableItemManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7088/");
        }

        public async Task<List<CraftableItemDto>> GetCraftableItemsAsync()
        {
            string uri = "/api/CraftableItem/";
            var craftables = new List<CraftableItemDto>();

            HttpResponseMessage response = await _httpClient.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                string responseString = await response.Content.ReadAsStringAsync();
                craftables = JsonSerializer.Deserialize<List<CraftableItemDto>>(responseString);
            }
            else
            {
                Console.WriteLine($"Error getting craftable items: {response.StatusCode} {response.Content}");
            }

            return craftables;
        }
    }
}
