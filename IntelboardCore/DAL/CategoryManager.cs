using IntelboardCore.Models;
using System.Net.Http;
using System.Text.Json;

namespace IntelboardCore.DAL
{
    public class CategoryManager : ICategoryManager
    {
        private readonly HttpClient _httpClient;

        public CategoryManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7088/");

        }
        public async Task<List<Category>> GetCategoriesAsync()
        {
            string uri = "/api/Category/";
            var categories = new List<Category>();

            HttpResponseMessage response = await _httpClient.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                string responseString = await response.Content.ReadAsStringAsync();
                categories = JsonSerializer.Deserialize<List<Category>>(responseString);
            }
            else
            {
                Console.WriteLine($"Error getting resources: {response.StatusCode} {response.Content}");
            }

            return categories;
        }
        public async Task SeedCategoriesAsync()
        {
            string uri = "/api/Category/Seed";

            HttpResponseMessage response = await _httpClient.PostAsync(uri, null);

            Console.WriteLine(response.IsSuccessStatusCode ? "Categories seeded successfully." : $"Error seeding categories: {response.StatusCode} {response.Content}");
        }
    }
}
