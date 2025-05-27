using IntelboardAPI.Data;
using IntelboardAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IntelboardAPI.DTO;

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
            foreach(var item in craftables)
            {
                CraftableItemDto currentCraftable = new CraftableItemDto();
                currentCraftable.CraftableItemId = item.Id;
                currentCraftable.Name = item.Name;
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
                //currentCraftable.ProductionCost = costs.Where(c => c.CraftableItemId == item.Id).Select(c => c.CraftableItem == item.ProductionCost).ToList();
                craftablesDto.Add(currentCraftable);
            }
            return craftablesDto;
        }
    }
}
