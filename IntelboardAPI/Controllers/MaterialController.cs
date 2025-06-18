using IntelboardAPI.Data;
using IntelboardCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IntelboardCore.Services;

namespace IntelboardAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MaterialController : Controller
    {
        private readonly IntelboardDbContext _context;
        private readonly IMaterialService _materialService;

        public MaterialController(IntelboardDbContext context, IMaterialService materialService)
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
            var material = await _context.Materials.Include(m => m.ProductionCost).SingleOrDefaultAsync(m => m.Id == id);
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
            material.CategoryId = 4;
            await _context.Materials.AddAsync(material);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditMaterialAsync(Material editedMaterial)
        {
            if (editedMaterial == null)
            {
                return BadRequest();
            }
                
            var existingMaterial = await _context.Materials
                .Include(m => m.ProductionCost)
                .FirstOrDefaultAsync(m => m.Id == editedMaterial.Id);

            if (existingMaterial == null)
            {
                return NotFound();
            }

            existingMaterial.Name = editedMaterial.Name;
            existingMaterial.CrateAmount = editedMaterial.CrateAmount;
            existingMaterial.CategoryId = 4;

            // This removes the existing values in the Costs table and then adds new one to avoid making multiple costs for the same craftable item
            _context.Costs.RemoveRange(existingMaterial.ProductionCost);

            existingMaterial.ProductionCost = editedMaterial.ProductionCost.Select(c => new Cost
            {
                CraftableItemId = c.CraftableItemId,
                Amount = c.Amount,
                ResourceId = c.ResourceId,
                MaterialId = c.MaterialId
            }).ToList();

            await _context.SaveChangesAsync();

            return Ok(existingMaterial);
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
            return Ok("Materials seeded successfully!");
        }

    }
}
