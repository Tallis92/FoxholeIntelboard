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
            List<CraftableItemDto> craftablesDto = new List<CraftableItemDto>();
            var craftables = await _context.CraftableItems.ToListAsync();
            foreach(var item in craftables)
            {
                CraftableItemDto currentCraftable = new CraftableItemDto();
                currentCraftable.CraftableItemId = item.Id;
                currentCraftable.Name = item.Name;
                currentCraftable.ProductionCost = item.ProductionCost;
                foreach(var cost in currentCraftable.ProductionCost.Where(c => c.CraftableItemId == item.Id)){

                }
                craftablesDto.Add(currentCraftable);
            }
            return craftablesDto;
        }
    }
}
