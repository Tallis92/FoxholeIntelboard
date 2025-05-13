using FoxholeIntelboard.Models;
using Humanizer.Localisation;
using System.Text;
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
        public async Task<Resource> GetResourceByIdAsync(int? id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = BaseAddress;
                string uri = $"/api/Resource/{id}";

                HttpResponseMessage response = await client.GetAsync(uri);
                var resource = new Resource();
                if (response.IsSuccessStatusCode)
                {
                    string responseString = await response.Content.ReadAsStringAsync();
                    resource = JsonSerializer.Deserialize<Resource>(responseString);


                }
                return resource;
            }
        }

        public async Task CreateResourceAsync(Resource resource)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = BaseAddress;
                string uri = "/api/Resource/";
                var json = JsonSerializer.Serialize(resource);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(uri, content);

                Console.WriteLine(response);
            }

        }

        public async Task DeleteResourceAsync(int? id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = BaseAddress;
                string uri = $"/api/Resource/{id}";
                HttpResponseMessage response = await client.DeleteAsync(uri);

                Console.WriteLine(response);
            }
        }
        public async Task EditResourceAsync(Resource resource)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = BaseAddress;
                string uri = $"/api/Resource/{resource.Id}";
                var json = JsonSerializer.Serialize(resource);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync(uri, content);

                Console.WriteLine(response);
            }

        }

        public async Task SeedResourcesAsync()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = BaseAddress;
                string uri = "/api/Resource/Seed";
                var resources = new List<Resource>();

                HttpResponseMessage responseResource = await client.PostAsync(uri, null);

                if (responseResource.IsSuccessStatusCode)
                {
                    string responseString = await responseResource.Content.ReadAsStringAsync();
                    Console.WriteLine(responseString);
                }

            }
        }
    }
}
