using IntelboardAPI.Data;
using IntelboardCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IntelboardAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CostController : Controller
    {
        private readonly IntelboardDbContext _context;

        public CostController(IntelboardDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<List<Cost>> GetCostsAsync()
        {
            return await _context.Costs.ToListAsync();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCostAsync([FromBody] Cost cost)
        {
            if (cost == null)
            {
                return BadRequest();
            }
            await _context.AddAsync(cost);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpGet("Exists")]
        public async Task<ActionResult<bool>> CostExists(int craftableItemId, int amount, string itemName)
        {
            var exists = await _context.Costs.AnyAsync(c =>
                c.CraftableItemId == craftableItemId &&
                c.Amount == amount &&
                ((c.Resource != null && c.Resource.Name == itemName) ||
                 (c.Material != null && c.Material.Name == itemName)));

            return Ok(exists);
        }

    }
}
