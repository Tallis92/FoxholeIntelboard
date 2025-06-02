using IntelboardAPI.Data;
using IntelboardAPI.Models;
using IntelboardAPI.Services;
using IntelboardAPI.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IntelboardAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AmmunitionController : Controller
    {
        private readonly IntelboardDbContext _context;
        private readonly IAmmunitionService _service;
    
        public AmmunitionController(IntelboardDbContext context, IAmmunitionService service)
        {
            _context = context;
            _service = service;
        }

        [HttpGet]
        public async Task<List<Ammunition>> GetAmmunitions()
        {
            var ammunitions = await _context.Ammunitions.ToListAsync();
            return ammunitions;
          
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Ammunition>> GetAmmunitionByIdAsync(int id)
        {
            var ammunition = await _context.Ammunitions.FindAsync(id);
            if (ammunition == null)
            {
                return NotFound();
            }
            return Ok(ammunition);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAmmunitionAsync([FromBody] Ammunition ammunition)
        {
            if (ammunition == null)
            {
                return BadRequest();
            }
            await _context.Ammunitions.AddAsync(ammunition);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditAmmunitionAsync(Ammunition editedammunition)
        {

            if (editedammunition == null)
            {
                return NotFound();
            }
            _context.Ammunitions.Update(editedammunition);
            _context.SaveChanges();

            return Ok(editedammunition);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAmmunitionAsync(int id)
        {
            var ammunition = await _context.Ammunitions.FindAsync(id);
            if (ammunition == null)
            {
                return NotFound();
            }

            _context.Ammunitions.Remove(ammunition);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPost("Seed")]
        public async Task<IActionResult> SeedAmmunitionsAsync()
        {
            await _service.SeedAmmunitionsAsync();
            return Ok("Ammunitions seeded successfully!");
        }

    }
}
