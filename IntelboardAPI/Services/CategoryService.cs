using IntelboardAPI.Models;
using Microsoft.EntityFrameworkCore;
using IntelboardAPI.Data;

namespace IntelboardAPI.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IWebHostEnvironment _env;
        private readonly IntelboardDbContext _context;

        public CategoryService(IntelboardDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // Loads and loops through csv file, checks for identical objects in the database and then saves a new category into the database.
        public async Task SeedCategoriesAsync()
        {
            List<string> categoryNames = await _context.Categories.Select(r => r.Name).ToListAsync(); ;

            var filePath = Path.Combine(_env.ContentRootPath, "Data\\CSV", "Categories.csv");
            Console.WriteLine("Filepath: " + filePath);

            if (System.IO.File.Exists(filePath))
            {
                var lines = System.IO.File.ReadAllLines(filePath);
                foreach (var line in lines.Skip(1))
                {
                    var values = line.Split(',');
                    if (!categoryNames.Contains(values[0]))
                    {
                        Category newCategory = new Category
                        {
                            Name = values[0]
                        };
                        _context.Categories.Add(newCategory);
                        Console.WriteLine($"{values[0]} added succesfully to resource list!");
                    }
                    else
                    {
                        Console.WriteLine($"Resource {values[0]} already exists in database");
                    }
                }
                await _context.SaveChangesAsync();
            }

        }
    }
}
