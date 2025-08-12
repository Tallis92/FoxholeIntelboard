using IntelboardAPI.Data;
using IntelboardCore.DTO;
using IntelboardCore.Models;
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
            // Retrieves all inventories from db with corresponding craftable items.
            var inventories = await _context.Inventories
                .Include(i => i.CratedItems).ThenInclude(c => c.CraftableItem)
                .ToListAsync();

            // Assign all values from inventories into inventoryDtos and then returns all lists with craftableItems already set
            // without the need of a foreach loop.
            var inventoryDtos = inventories.Select(inventory => new InventoryDto
            {
                InventoryId = inventory.Id,
                FactionId = inventory.FactionId,
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

        [HttpGet("Share")]
        public async Task<IActionResult> Share([FromQuery] Guid token)
        {
            // Assuming Inventory has a ShareToken property (add if missing)
            var inventories = await _context.Inventories
                .Include(i => i.CratedItems)
                    .ThenInclude(ci => ci.CraftableItem)
                .Where(i => EF.Property<Guid>(i, "ShareToken") == token)
                .ToListAsync();

            if (inventories == null || inventories.Count == 0)
                return NotFound();

            var inventoryDtos = inventories.Select(inventory => new InventoryDto
            {
                InventoryId = inventory.Id,
                FactionId = inventory.FactionId,
                Name = inventory.Name,
                CratedItems = inventory.CratedItems.Select(item => new CratedItemDto
                {
                    Description = item.Description,
                    Amount = item.Amount,
                    RequiredAmount = item.RequiredAmount,
                    CraftableItemId = item.CraftableItem.Id
                }).ToList()
            }).ToList();

            return Ok(inventoryDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InventoryDto>> GetInventoryByIdAsync(Guid id)
        {
            // Retrieves a specific inventory from db with corresponding craftable items.
            var inventory = await _context.Inventories
                .Include(i => i.CratedItems)
                    .ThenInclude(ci => ci.CraftableItem)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (inventory == null)
            {
                return NotFound();
            }

            // Assign relevant values from the inventory into the DTO
            var dto = new InventoryDto
            {
                InventoryId = inventory.Id,
                Name = inventory.Name,
                FactionId = inventory.FactionId,
                CratedItems = inventory.CratedItems.Select(ci => new CratedItemDto
                {
                    Id = ci.Id,
                    CraftableItemId = ci.CraftableItem.Id,
                    Amount = ci.Amount,
                    RequiredAmount = ci.RequiredAmount,
                    Description = ci.Description,
                    // Uses switch case to chek the type of current object the craftableItem is and assign it to the type
                    // variable in the dto.
                    Type = ci.CraftableItem is Ammunition ? "Ammunition"
                        : ci.CraftableItem is Material ? "Material"
                        : ci.CraftableItem is Weapon ? "Weapon"
                        : "Type not found"

                }).ToList()

            };

            return Ok(dto);
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
                FactionId = inventory.FactionId,
                CratedItems = cratedItems
            };
            await _context.Inventories.AddAsync(newInventory);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditInventoryAsync(Guid id, [FromBody] InventoryDto editedInventory)
        {
            if (editedInventory == null || id != editedInventory.InventoryId)
            {
                return BadRequest();
            }
            // Retrieves a specific inventory from db with corresponding craftable items. If it doesn't exist, return an error.
            var existingInventory = await _context.Inventories
                .Include(i => i.CratedItems).ThenInclude(c => c.CraftableItem)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (existingInventory == null)
            {
                return NotFound();
            }

            // Inserts data from existingInventory into the "new" inventory object to update it with new values
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
       
    }
}
