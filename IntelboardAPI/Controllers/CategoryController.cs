using IntelboardCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IntelboardAPI.Data;
using IntelboardCore.Services.Interfaces;

namespace IntelboardAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IntelboardDbContext _context;
        public CategoryController(ICategoryService categoryService, IntelboardDbContext context)
        {
            _categoryService = categoryService;
            _context = context;
        }

        [HttpGet]
        public async Task<IList<Category>> GetCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] Category newCategory)
        {
            _context.Categories.Add(newCategory);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCategoriesAsync), new { id = newCategory.Id }, newCategory);
        }

        [HttpPost("Seed")]
        public async Task<IActionResult> SeedCategoriesAsync()
        {
            await _categoryService.SeedCategoriesAsync();
            return Ok("Categories seeded successfully!");
        }
        
    }
}
