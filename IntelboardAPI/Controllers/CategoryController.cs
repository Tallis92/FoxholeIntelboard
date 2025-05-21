using IntelboardAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IntelboardAPI.Services;
using IntelboardAPI.Data;

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

        [HttpPost("Seed")]
        public async Task<IActionResult> SeedCategoriesAsync()
        {
            await _categoryService.SeedCategoriesAsync();
            return Ok("Status kod 200");
        }
        
    }
}
