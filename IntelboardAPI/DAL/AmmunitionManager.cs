using IntelboardAPI.Models;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace IntelboardAPI.DAL
{
    public class AmmunitionManager
    {
        private readonly HttpClient _httpClient;

        public AmmunitionManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7088/");

        }

        public async Task<List<Ammunition>> GetAmmunitionsAsync()
        {

            string uri = "/api/Ammunition/";
            var ammunitions = new List<Ammunition>();

            HttpResponseMessage response = await _httpClient.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                string responseString = await response.Content.ReadAsStringAsync();
                ammunitions = JsonSerializer.Deserialize<List<Ammunition>>(responseString);
            }
            else
            {
                Console.WriteLine($"Error getting ammunition: {response.StatusCode} {response.Content}");
            }

            return ammunitions;

        }
        public async Task<Ammunition> GetAmmunitionByIdAsync(int? id)
        {
            string uri = $"/api/Ammunition/{id}";

            HttpResponseMessage response = await _httpClient.GetAsync(uri);
            var ammunition = new Ammunition();
            if (response.IsSuccessStatusCode)
            {
                string responseString = await response.Content.ReadAsStringAsync();
                ammunition = JsonSerializer.Deserialize<Ammunition>(responseString);
            }
            else
            {
                Console.WriteLine($"Error getting ammunition: {response.StatusCode} {response.Content}");
            }
            return ammunition;

        }

        public async Task CreateAmmunitionAsync(Ammunition ammunition)
        {

            string uri = "/api/Ammunition/";
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
            var json = JsonSerializer.Serialize(ammunition, options);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync(uri, content);

            Console.WriteLine(response.IsSuccessStatusCode ? "Ammunition created successfully." : $"Error creating ammunition: {response.StatusCode} {response.Content}");

        }
        public async Task DeleteAmmunitionAsync(int? id)
        {

            string uri = $"/api/Ammunition/{id}";
            HttpResponseMessage response = await _httpClient.DeleteAsync(uri);

            Console.WriteLine(response.IsSuccessStatusCode ? "Ammunition deleted successfully." : $"Error deleting ammunition: {response.StatusCode} {response.Content}" );
        }
        public async Task EditAmmunitionAsync(Ammunition ammunition)
        {
            string uri = $"/api/Ammunition/{ammunition.Id}";
            var json = JsonSerializer.Serialize(ammunition);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PutAsync(uri, content);

            Console.WriteLine(response.IsSuccessStatusCode ? "Ammunition updated successfully." : $"Error updated ammunition: {response.StatusCode} {response.Content}");

        }

        public async Task SeedAmmunitionsAsync()
        {
            string uri = "/api/Ammunition/Seed";

            HttpResponseMessage response = await _httpClient.PostAsync(uri, null);

            Console.WriteLine(response.IsSuccessStatusCode ? "Ammunition seeded successfully." : $"Error seeding ammunition: {response.StatusCode} {response.Content}");

        }
    }
}
