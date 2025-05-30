using Azure;
using IntelboardAPI.Models;
using System.Text;
using System.Text.Json;
namespace FoxholeIntelboard.DAL
{
    public class ResourceManager : IResourceManager
    {
        private readonly HttpClient _httpClient;


        public ResourceManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7088/");

        }

        public async Task<List<Resource>> GetResourcesAsync()
        {
            string uri = "/api/Resource/";
            var resources = new List<Resource>();

            HttpResponseMessage responseResource = await _httpClient.GetAsync(uri);

            if (responseResource.IsSuccessStatusCode)
            {
                string responseString = await responseResource.Content.ReadAsStringAsync();
                resources = JsonSerializer.Deserialize<List<Resource>>(responseString);
            }
            else
            {
                Console.WriteLine($"Error getting resources: {responseResource.StatusCode} {responseResource.Content}");
            }

            return resources;
        }
        public async Task<Resource> GetResourceByIdAsync(int? id)
        {
            string uri = $"/api/Resource/{id}";

            HttpResponseMessage response = await _httpClient.GetAsync(uri);
            var resource = new Resource();
            if (response.IsSuccessStatusCode)
            {
                string responseString = await response.Content.ReadAsStringAsync();
                resource = JsonSerializer.Deserialize<Resource>(responseString);
            }
            else
            {
                Console.WriteLine($"Error getting resource: {response.StatusCode} {response.Content}");
            }
            return resource;
        }

        public async Task CreateResourceAsync(Resource resource)
        {
            string uri = "/api/Resource/";
            var json = JsonSerializer.Serialize(resource);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync(uri, content);

            Console.WriteLine(response.IsSuccessStatusCode ? "Resource created successfully." : $"Error creating resource: {response.StatusCode} {response.Content}");
        }

        public async Task DeleteResourceAsync(int? id)
        {
            string uri = $"/api/Resource/{id}";
            HttpResponseMessage response = await _httpClient.DeleteAsync(uri);

            Console.WriteLine(response.IsSuccessStatusCode ? "Resource deleted successfully." : $"Error deleting resource: {response.StatusCode} {response.Content}");
        }
        public async Task EditResourceAsync(Resource resource)
        {
            string uri = $"/api/Resource/{resource.Id}";
            var json = JsonSerializer.Serialize(resource);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PutAsync(uri, content);

            Console.WriteLine(response.IsSuccessStatusCode ? "Resource updated successfully." : $"Error updating resource: {response.StatusCode} {response.Content}");
        }

        public async Task SeedResourcesAsync()
        {
            string uri = "/api/Resource/Seed";
            HttpResponseMessage response = await _httpClient.PostAsync(uri, null);

            Console.WriteLine(response.IsSuccessStatusCode ? "Resources seeded successfully." : $"Error seeding resources: {response.StatusCode} {response.Content}");

        }
       
    }
}
