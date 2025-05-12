using IntelboardAPI.Data;
using IntelboardAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IntelboardAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MaterialController : Controller
    {
        private readonly IntelboardDbContext _context;
        private readonly Services.IMaterialService _materialService;

        public MaterialController(IntelboardDbContext context, Services.IMaterialService materialService)
        {
            _context = context;
            _materialService = materialService;
        }
        [HttpGet]
        public async Task<List<Material>> GetMaterials()
        {
            {
                return await _context.Materials.ToListAsync();
            }
        }
        [HttpPost("Seed")]
        public async Task<IActionResult> SeedMaterialsAsync()
        {
            await _materialService.SeedMaterialsAsync();
            return Ok("Status kod 200");
        }

    }
}
