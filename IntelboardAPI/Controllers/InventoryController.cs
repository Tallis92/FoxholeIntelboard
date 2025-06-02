using IntelboardAPI.Data;
using IntelboardAPI.DTO;
using IntelboardAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IntelboardAPI.Controllers
{
    // TODO: Add edit, delete and get by id methods for InventoryController
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
                .Include(i => i.CratedItems).ThenInclude(c => c.CraftableItem)
                .ToListAsync();

            var inventoryDtos = inventories.Select(inventory => new InventoryDto
            {
                InventoryId = inventory.Id,
                Name = inventory.Name,
                CratedItems = inventory.CratedItems.Select(item => new CratedItemDto
                {
                    Description = item.Description,
                    Amount = item.Amount,
                    RequiredAmount = item.RequiredAmount,
                    CraftableItemId = item.CraftableItem.Id
                    
                }).ToList()
            }).ToList();

            return inventoryDtos;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InventoryDto>> GetInventoryByIdAsync(Guid id)
        {
            var inventory = await _context.Inventories
                .Include(i => i.CratedItems)
                    .ThenInclude(ci => ci.CraftableItem)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (inventory == null)
            {
                return NotFound();
            }


            var dto = new InventoryDto
            {
                InventoryId = inventory.Id,
                Name = inventory.Name,
                CratedItems = inventory.CratedItems.Select(ci => new CratedItemDto
                {
                    Id = ci.Id,
                    CraftableItemId = ci.CraftableItem.Id,
                    Amount = ci.Amount,
                    RequiredAmount = ci.RequiredAmount,
                    Description = ci.Description,
                    Type = ci.CraftableItem is Ammunition ? "Ammunition"
                        : ci.CraftableItem is Material ? "Material"
                        : ci.CraftableItem is Weapon ? "Weapon"
                        : "Type not found"

                }).ToList()

            };

            return Ok(dto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInventoryAsync(Guid id)
        {
            var cratedItems = await _context.Inventories.Include(ci => ci.CratedItems).ThenInclude(c => c.CraftableItem)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (cratedItems == null)
            {
                return NotFound();
            }
            _context.Inventories.Remove(cratedItems);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInventoryAsync(Guid id, [FromBody] InventoryDto editedInventory)
        {
            if (editedInventory == null || id != editedInventory.InventoryId)
            {
                return BadRequest();
            }
            var existingInventory = await _context.Inventories
                .Include(i => i.CratedItems).ThenInclude(c => c.CraftableItem)
                .FirstOrDefaultAsync(i => i.Id == id);
            if (existingInventory == null)
            {
                return NotFound();
            }
            existingInventory.Name = editedInventory.Name;
            existingInventory.CratedItems.Clear();
            foreach (var item in editedInventory.CratedItems)
            {
                var craftableItem = await _context.CraftableItems.FirstOrDefaultAsync(i => i.Id == item.CraftableItemId);
                if (craftableItem != null)
                {
                    var cratedItem = new CratedItem
                    {
                        Description = item.Description,
                        Amount = item.Amount,
                        RequiredAmount = item.RequiredAmount,
                        CraftableItem = craftableItem
                    };
                    existingInventory.CratedItems.Add(cratedItem);
                }
            }
            _context.Inventories.Update(existingInventory);
            await _context.SaveChangesAsync();
            return Ok(existingInventory);
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
                var craftableItem = await _context.CraftableItems.FirstOrDefaultAsync(i => i.Id == item.CraftableItemId);


                var cratedItem = new CratedItem
                {
                    Description = item.Description,
                    Amount = item.Amount,
                    RequiredAmount = item.RequiredAmount,
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
