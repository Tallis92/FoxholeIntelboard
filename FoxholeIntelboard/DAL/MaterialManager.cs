using FoxholeIntelboard.Models;
using Humanizer.Localisation;
using System.Text;
using System.Text.Json;
namespace FoxholeIntelboard.DAL
{
    public class MaterialManager
    {
        private readonly HttpClient _httpClient;

        public MaterialManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        private static Uri BaseAddress = new Uri("https://localhost:7088/");

        public async Task<List<Material>> GetMaterialsAsync()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = BaseAddress;
                string uri = "/api/Material/";
                var materials = new List<Material>();

                HttpResponseMessage response = await client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    string responseString = await response.Content.ReadAsStringAsync();
                    materials = JsonSerializer.Deserialize<List<Material>>(responseString);
                }

                return materials;
            }
        }
        public async Task<Material> GetMaterialByIdAsync(int? id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = BaseAddress;
                string uri = $"/api/Material/{id}";

                HttpResponseMessage response = await client.GetAsync(uri);
                var material = new Material();
                if (response.IsSuccessStatusCode)
                {
                    string responseString = await response.Content.ReadAsStringAsync();
                    material = JsonSerializer.Deserialize<Material>(responseString);


                }
                return material;
            }
        }

        public async Task CreateMaterialsAsync(Material material)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = BaseAddress;
                string uri = "/api/Material/";
                var json = JsonSerializer.Serialize(material);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(uri, content);

                Console.WriteLine(response);
            }
        }
        public async Task DeleteMaterialAsync(int? id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = BaseAddress;
                string uri = $"/api/Material/{id}";
                HttpResponseMessage response = await client.DeleteAsync(uri);

                Console.WriteLine(response);
            }

        }
        public async Task EditMaterialAsync(Material material)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = BaseAddress;
                string uri = $"/api/Material/{material.Id}";
                var json = JsonSerializer.Serialize(material);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync(uri, content);

                Console.WriteLine(response);
            }

        }

        public async Task SeedMaterialsAsync()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = BaseAddress;
                string uri = "/api/Material/Seed";

                HttpResponseMessage responseResource = await client.GetAsync(uri);

                if (responseResource.IsSuccessStatusCode)
                {
                    string responseString = await responseResource.Content.ReadAsStringAsync();
                    Console.WriteLine(responseString);
                }

            }
        }
    }
}
