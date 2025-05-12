using FoxholeIntelboard.Models;
using System.Text;
using System.Text.Json;

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

                HttpResponseMessage responseAmmunition = await client.GetAsync(uri);

                if (responseAmmunition.IsSuccessStatusCode)
                {
                    string responseString = await responseAmmunition.Content.ReadAsStringAsync();
                    ammunitions = JsonSerializer.Deserialize<List<Ammunition>>(responseString);
                }

                return ammunitions;
            }
        }
        public async Task CreateAmmunitionAsync(Ammunition ammunition)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = BaseAddress;
                string uri = "/api/Ammunition/";
                var json = JsonSerializer.Serialize(ammunition);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage responseAmmunition = await client.PostAsync(uri, content);

                Console.WriteLine(responseAmmunition);
            }

        }
        public async Task DeleteAmmunitionAsync(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = BaseAddress;
                string uri = $"/api/Ammunition/{id}";
                HttpResponseMessage responseAmmunition = await client.DeleteAsync(uri);

                Console.WriteLine(responseAmmunition);
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
                HttpResponseMessage responseAmmunition = await client.PutAsync(uri, content);

                Console.WriteLine(responseAmmunition);
            }

        }
    }
}
