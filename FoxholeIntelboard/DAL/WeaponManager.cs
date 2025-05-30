using IntelboardAPI.Models;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FoxholeIntelboard.DAL
{
    public class WeaponManager : IWeaponManager
    {
        private readonly HttpClient _httpClient;

        public WeaponManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7088/");

        }

        public async Task<List<Weapon>> GetWeaponsAsync()
        {
            string uri = "/api/Weapon/";
            var weapons = new List<Weapon>();

            HttpResponseMessage response = await _httpClient.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                string responseString = await response.Content.ReadAsStringAsync();
                weapons = JsonSerializer.Deserialize<List<Weapon>>(responseString);
            }
            else
            {
                Console.WriteLine($"Error getting weapons: {response.StatusCode} {response.Content}");
            }

            return weapons;
        }
        public async Task<Weapon> GetWeaponByIdAsync(int? id)
        {

            string uri = $"/api/Weapon/{id}";

            HttpResponseMessage response = await _httpClient.GetAsync(uri);
            var weapon = new Weapon();
            if (response.IsSuccessStatusCode)
            {
                string responseString = await response.Content.ReadAsStringAsync();
                weapon = JsonSerializer.Deserialize<Weapon>(responseString);
            }
            else
            {
                Console.WriteLine($"Error getting weapon: {response.StatusCode} {response.Content}");
            }
            return weapon;
        }

        public async Task CreateWeaponAsync(Weapon weapon)
        {
            string uri = "/api/Weapon/";
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
            var json = JsonSerializer.Serialize(weapon, options);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync(uri, content);

            Console.WriteLine(response.IsSuccessStatusCode ? "Weapon created successfully." : $"Error creating weapon: {response.StatusCode} {response.Content}");

        }
        public async Task DeleteWeaponAsync(int? id)
        {
            string uri = $"/api/Weapon/{id}";
            HttpResponseMessage response = await _httpClient.DeleteAsync(uri);

            Console.WriteLine(response.IsSuccessStatusCode ? "Weapon deleted successfully." : $"Error deleting ammunition: {response.StatusCode} {response.Content}");
        }
        public async Task EditWeaponAsync(Weapon weapon)
        {
            string uri = $"/api/Weapon/{weapon.Id}";
            var json = JsonSerializer.Serialize(weapon);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PutAsync(uri, content);

            Console.WriteLine(response.IsSuccessStatusCode ? "Weapon updated successfully." : $"Error updating weapon: {response.StatusCode} {response.Content}");
        }

        public async Task SeedWeaponsAsync()
        {
            string uri = "/api/Weapon/Seed";

            HttpResponseMessage response = await _httpClient.PostAsync(uri, null);

            Console.WriteLine(response.IsSuccessStatusCode ? "Weapons seeded successfully." : $"Error seeding weapons: {response.StatusCode} {response.Content}");


        }
    }
}


