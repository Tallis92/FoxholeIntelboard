using IntelboardAPI.Data;
using Microsoft.AspNetCore.Mvc;
using IntelboardAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace IntelboardAPI.Controllers
{
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
    }
}
