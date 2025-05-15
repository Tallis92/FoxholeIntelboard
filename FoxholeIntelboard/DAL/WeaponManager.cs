using IntelboardAPI.Models;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FoxholeIntelboard.DAL
{
    public class WeaponManager
    {
        private readonly HttpClient _httpClient;

        public WeaponManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        private static Uri BaseAddress = new Uri("https://localhost:7088/");

        public async Task<List<Weapon>> GetWeaponsAsync()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = BaseAddress;
                string uri = "/api/Weapon/";
                var weapons = new List<Weapon>();

                HttpResponseMessage response = await client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    string responseString = await response.Content.ReadAsStringAsync();
                    weapons = JsonSerializer.Deserialize<List<Weapon>>(responseString);
                }

                return weapons;
            }
        }
        public async Task<Weapon> GetWeaponByIdAsync(int? id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = BaseAddress;
                string uri = $"/api/Weapon/{id}";

                HttpResponseMessage response = await client.GetAsync(uri);
                var weapon = new Weapon();
                if (response.IsSuccessStatusCode)
                {
                    string responseString = await response.Content.ReadAsStringAsync();
                    weapon = JsonSerializer.Deserialize<Weapon>(responseString);


                }
                return weapon;
            }
        }

        public async Task CreateWeaponAsync(Weapon weapon)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = BaseAddress;
                string uri = "/api/Weapon/";
                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.IgnoreCycles,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = true
                };
                var json = JsonSerializer.Serialize(weapon, options);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(uri, content);

                Console.WriteLine(response);
            }
        }
        public async Task DeleteWeaponAsync(int? id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = BaseAddress;
                string uri = $"/api/Weapon/{id}";
                HttpResponseMessage response = await client.DeleteAsync(uri);

                Console.WriteLine(response);
            }

        }
        public async Task EditWeaponAsync(Weapon weapon)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = BaseAddress;
                string uri = $"/api/Weapon/{weapon.Id}";
                var json = JsonSerializer.Serialize(weapon);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync(uri, content);

                Console.WriteLine(response);
            }

        }

        public async Task SeedWeaponsAsync()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = BaseAddress;
                string uri = "/api/Weapon/Seed";

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

