using FoxholeIntelboard.Models;
using Humanizer.Localisation;
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

                HttpResponseMessage responseMaterial = await client.GetAsync(uri);

                if (responseMaterial.IsSuccessStatusCode)
                {
                    string responseString = await responseMaterial.Content.ReadAsStringAsync();
                    materials = JsonSerializer.Deserialize<List<Material>>(responseString);
                }

                return materials;
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
