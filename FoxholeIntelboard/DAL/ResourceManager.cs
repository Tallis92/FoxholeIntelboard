using FoxholeIntelboard.Models;
using Humanizer.Localisation;
using System.Text.Json;
namespace FoxholeIntelboard.DAL
{
    public class ResourceManager
    {
        private readonly HttpClient _httpClient;

        public ResourceManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        private static Uri BaseAddress = new Uri("https://localhost:7088/");

        public async Task<List<Resource>> GetResourcesAsync()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = BaseAddress;
                string uri = "/api/Resource/";
                var resources = new List<Resource>();

                HttpResponseMessage responseResource = await client.GetAsync(uri);

                if (responseResource.IsSuccessStatusCode)
                {
                    string responseString = await responseResource.Content.ReadAsStringAsync();
                    resources = JsonSerializer.Deserialize<List<Resource>>(responseString);
                }

                return resources;
            }
        }

        public async Task SeedResourcesAsync()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = BaseAddress;
                string uri = "/api/Resource/Seed";
                var resources = new List<Resource>();

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
