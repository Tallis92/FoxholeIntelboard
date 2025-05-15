using IntelboardAPI.Models;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FoxholeIntelboard.DAL
{
    public class AmmunitionManager
    {
        private readonly HttpClient _httpClient;

        public AmmunitionManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        private static Uri BaseAddress = new Uri("https://localhost:7088/");

        public async Task<List<Ammunition>> GetAmmunitionsAsync()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = BaseAddress;
                string uri = "/api/Ammunition/";
                var ammunitions = new List<Ammunition>();

                HttpResponseMessage response = await client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    string responseString = await response.Content.ReadAsStringAsync();
                    ammunitions = JsonSerializer.Deserialize<List<Ammunition>>(responseString);
                }

                return ammunitions;
            }
        }
        public async Task<Ammunition> GetAmmunitionByIdAsync(int? id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = BaseAddress;
                string uri = $"/api/Ammunition/{id}";

                HttpResponseMessage response = await client.GetAsync(uri);
                var ammunition = new Ammunition();
                if (response.IsSuccessStatusCode)
                {
                    string responseString = await response.Content.ReadAsStringAsync();
                    ammunition = JsonSerializer.Deserialize<Ammunition>(responseString);

                    
                }
                return ammunition;
            }
        }

        public async Task CreateAmmunitionAsync(Ammunition ammunition)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = BaseAddress;
                string uri = "/api/Ammunition/";
                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.IgnoreCycles,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = true
                };
                var json = JsonSerializer.Serialize(ammunition, options);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(uri, content);

                Console.WriteLine(response);
            }
        }
        public async Task DeleteAmmunitionAsync(int? id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = BaseAddress;
                string uri = $"/api/Ammunition/{id}";
                HttpResponseMessage response = await client.DeleteAsync(uri);

                Console.WriteLine(response);
            }

        }
        public async Task EditAmmunitionAsync(Ammunition ammunition)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = BaseAddress;
                string uri = $"/api/Ammunition/{ammunition.Id}";
                var json = JsonSerializer.Serialize(ammunition);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync(uri, content);

                Console.WriteLine(response);
            }

        }

        public async Task SeedAmmunitionsAsync()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = BaseAddress;
                string uri = "/api/Ammunition/Seed";

                HttpResponseMessage response = await client.PostAsync(uri, null);

                if (response.IsSuccessStatusCode)
                {
                    string responseString = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseString);
                }

            }
        }
    }
}
