using IntelboardAPI.Data;
using IntelboardCore.DTO;
using IntelboardCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IntelboardAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CraftableItemController : Controller
    {
        private readonly IntelboardDbContext _context;
        public CraftableItemController(IntelboardDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IList<CraftableItemDto>> GetCraftableItemsAsync()
        {
            var costs = await _context.Costs.ToListAsync();

            List<CraftableItemDto> craftablesDto = new List<CraftableItemDto>();
            var craftables = await _context.CraftableItems.ToListAsync();
            foreach (var item in craftables)
            {
                CraftableItemDto currentCraftable = new CraftableItemDto
                {
                    CraftableItemId = item.Id,
                    Name = item.Name
                };

                // Determine CrateAmount from subclass
                switch (item)
                {
                    case Weapon weapon:
                        currentCraftable.CrateAmount = weapon.CrateAmount;
                        break;
                    case Material material:
                        currentCraftable.CrateAmount = material.CrateAmount;
                        break;
                    case Ammunition ammo:
                        currentCraftable.CrateAmount = ammo.CrateAmount;
                        break;
                    default:
                        currentCraftable.CrateAmount = 0;
                        break;
                }

                foreach (var cost in costs.Where(c => c.CraftableItemId == item.Id))
                {
                    CostDto costDto = new CostDto
                    {
                        Amount = cost.Amount,
                        CraftableItemId = item.Id,
                        ResourceId = cost.ResourceId,
                        MaterialId = cost.MaterialId
                    };
                    currentCraftable.ProductionCost.Add(costDto);
                }       
                
                craftablesDto.Add(currentCraftable);
            }
            
            return craftablesDto;
        }
    }
}
