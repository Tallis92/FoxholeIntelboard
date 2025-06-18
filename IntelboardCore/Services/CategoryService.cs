using IntelboardCore.Models;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Json;

namespace IntelboardCore.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly string _contentRootPath;
        private readonly HttpClient _httpClient;

        public CategoryService(string contentRootPath, HttpClient httpClient)
        {
            _contentRootPath = contentRootPath;
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7088/");
        }

        // Loads and loops through csv file, checks for identical objects in the database and then saves a new category into the database.
        public async Task SeedCategoriesAsync()
        {
            var categories = await _httpClient.GetFromJsonAsync<List<Category>>("/api/Category/");

            var filePath = Path.Combine(_contentRootPath, "Data\\CSV", "Categories.csv");
            Console.WriteLine("Filepath: " + filePath);

            if (System.IO.File.Exists(filePath))
            {
                var lines = System.IO.File.ReadAllLines(filePath);
                foreach (var line in lines.Skip(1))
                {
                    var values = line.Split(',');
                    if (!categories.Select(c => c.Name).Contains(values[0]))
                    {
                        Category newCategory = new Category
                        {
                            Name = values[0]       
                        };
                        var response = await _httpClient.PostAsJsonAsync("/api/Resource/", newCategory);
                        Console.WriteLine(response.IsSuccessStatusCode ? $"{values[0]} created successfully." : $"Error creating category: {response.StatusCode} {response.Content}");
                    }
                    else
                    {
                        Console.WriteLine($"Resource {values[0]} already exists in database");
                    }
                }
            }
        }
    }
}
