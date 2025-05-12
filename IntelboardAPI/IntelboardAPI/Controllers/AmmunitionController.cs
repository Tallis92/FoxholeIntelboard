using IntelboardAPI.Data;
using IntelboardAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IntelboardAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AmmunitionController : Controller
    {
        private readonly IntelboardDbContext _context;
    
        public AmmunitionController(IntelboardDbContext context)
        {
            _context = context;
        }
        [HttpGet] 
        public async Task<List<Ammunition>> GetAmmunitions()
        {
            {
                return await _context.Ammunitions.ToListAsync();
            }
        }
    }
}
