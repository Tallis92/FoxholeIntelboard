using IntelboardAPI.Data;
using Microsoft.AspNetCore.Mvc;
using IntelboardAPI.Models;
using Microsoft.EntityFrameworkCore;
using IntelboardAPI.DTO;

namespace IntelboardAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : Controller
    {
        private readonly IntelboardDbContext _context;
        public InventoryController(IntelboardDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IList<Inventory>> GetInventoriesAsync()
        {
            return await _context.Inventories.ToListAsync();
        }

        [HttpPost]
        public async Task<IActionResult> CreateInventoryAsync([FromBody] InventoryDto inventory)
        {
            if (inventory == null)
            {
                return BadRequest();
            }
            List<CratedItem> cratedItems = new List<CratedItem>();
            foreach(var item in inventory.CratedItems)
            {
                cratedItems = item;
            }
            Inventory newInventory = new Inventory
            {
                Name = inventory.Name,
                CratedItems = inventory.CratedItems
            };
            await _context.Inventories.AddAsync(newInventory);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
