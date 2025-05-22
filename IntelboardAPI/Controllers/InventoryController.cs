using IntelboardAPI.Data;
using IntelboardAPI.DTO;
using IntelboardAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IList<InventoryDto>> GetInventoriesAsync()
        {
            var inventories = await _context.Inventories
                .Include(i => i.CratedItems)
                .ToListAsync();

            var inventoryDtos = inventories.Select(inventory => new InventoryDto
            {
                Name = inventory.Name,
                CratedItems = inventory.CratedItems.Select(item => new CratedItemDto
                {
                    Description = item.Description,
                    Amount = item.Amount,
                    CraftableItemId = item.Id 
                }).ToList()
            }).ToList();

            return inventoryDtos;
        }



        [HttpPost]
        public async Task<IActionResult> CreateInventoryAsync([FromBody] InventoryDto inventory)
        {
            if (inventory == null)
            {
                return BadRequest();
            }
            var cratedItems = new List<CratedItem>();

            foreach (var item in inventory.CratedItems)
            {
                var craftableItem = await _context.CraftableItems.FirstOrDefaultAsync(i => i.Id == item.Id);


                var cratedItem = new CratedItem
                {
                    Description = item.Description,
                    Amount = item.Amount,
                    CraftableItem = craftableItem
                };
                cratedItems.Add(cratedItem);
            }

            var newInventory = new Inventory
            {
                Name = inventory.Name,
                CratedItems = cratedItems
            };
            await _context.Inventories.AddAsync(newInventory);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
