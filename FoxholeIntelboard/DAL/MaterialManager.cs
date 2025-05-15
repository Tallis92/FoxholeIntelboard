using IntelboardAPI.Models;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
namespace FoxholeIntelboard.DAL
{
    public class MaterialManager
    {
        private readonly HttpClient _httpClient;

        public MaterialManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7088/");
        }
        public async Task<List<Material>> GetMaterialsAsync()
        {
            string uri = "/api/Material/";
            var materials = new List<Material>();

            HttpResponseMessage response = await _httpClient.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                string responseString = await response.Content.ReadAsStringAsync();
                materials = JsonSerializer.Deserialize<List<Material>>(responseString);
            }

            return materials;
        }
        public async Task<Material> GetMaterialByIdAsync(int? id)
        {

            string uri = $"/api/Material/{id}";

            HttpResponseMessage response = await _httpClient.GetAsync(uri);
            var material = new Material();
            if (response.IsSuccessStatusCode)
            {
                string responseString = await response.Content.ReadAsStringAsync();
                material = JsonSerializer.Deserialize<Material>(responseString);


            }
            return material;
        }

        public async Task CreateMaterialsAsync(Material material)
        {

            string uri = "/api/Material/";

            // Adds options to be able to serialize the productionCosts and not cause any Json serialization conflicts when trying to
            // post into the API
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
            var json = JsonSerializer.Serialize(material, options);

            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync(uri, content);
            if (!response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Status: {response.StatusCode}");
                Console.WriteLine($"Response Body: {responseBody}");
            }
            Console.WriteLine(response);

        }
        public async Task DeleteMaterialAsync(int? id)
        {

            string uri = $"/api/Material/{id}";
            HttpResponseMessage response = await _httpClient.DeleteAsync(uri);

            Console.WriteLine(response);
        }
        public async Task EditMaterialAsync(Material material)
        {
            string uri = $"/api/Material/{material.Id}";
            var json = JsonSerializer.Serialize(material);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PutAsync(uri, content);

            Console.WriteLine(response);

        }

        public async Task SeedMaterialsAsync()
        {
            string uri = "/api/Material/Seed";

            HttpResponseMessage responseResource = await _httpClient.PostAsync(uri, null);

            if (responseResource.IsSuccessStatusCode)
            {
                string responseString = await responseResource.Content.ReadAsStringAsync();
                Console.WriteLine(responseString);
            }
        }
    }
}
