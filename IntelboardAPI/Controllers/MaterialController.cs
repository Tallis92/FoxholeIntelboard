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

        [HttpGet("{id}")]
        public async Task<ActionResult<Material>> GetMaterialByIdAsync(int id)
        {
            var material = await _context.Materials.FindAsync(id);
            if (material == null)
            {
                return NotFound();
            }
            return Ok(material);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMaterialAsync([FromBody] Material material)
        {
            if (material == null)
            {
                return BadRequest();
            }
            await _context.Materials.AddAsync(material);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMaterialAsync(Material editedmaterial)
        {

            if (editedmaterial == null)
            {
                return NotFound();
            }
            _context.Materials.Update(editedmaterial);
            _context.SaveChanges();

            return Ok(editedmaterial);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMaterialAsync(int id)
        {
            var material = await _context.Materials.FindAsync(id);
            if (material == null)
            {
                return NotFound();
            }

            _context.Materials.Remove(material);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPost("Seed")]
        public async Task<IActionResult> SeedMaterialsAsync()
        {
            await _materialService.SeedMaterialsAsync();
            return Ok("Status kod 200");
        }

    }
}
