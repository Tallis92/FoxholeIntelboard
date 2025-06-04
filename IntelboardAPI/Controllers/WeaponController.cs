using IntelboardAPI.Data;
using IntelboardAPI.Models;
using IntelboardAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IntelboardAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeaponController : Controller
    {
        private readonly IntelboardDbContext _context;
        private readonly IWeaponService _service;

        public WeaponController(IntelboardDbContext context, IWeaponService service)
        {
            _context = context;
            _service = service;
        }

        [HttpGet]
        public async Task<List<Weapon>> GetWeapons()
        {
            
            return await _context.Weapons.ToListAsync();

            
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Weapon>> GetWeaponByIdAsync(int id)
        {
            try
            {
                var weapon = await _context.Weapons
                 .Include(w => w.ProductionCost)
                 .SingleOrDefaultAsync(w => w.Id == id);

                if (weapon == null)
                {
                    return NotFound();
                }

                return Ok(weapon);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateWeaponAsync([FromBody] Weapon weapon)
        {
            if (weapon == null)
            {
                return BadRequest();
            }
            await _context.Weapons.AddAsync(weapon);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditWeaponAsync(Weapon editedWeapon)
        {
            if (editedWeapon == null)
            {
                return NotFound();
            }

            var existingWeapon = await _context.Weapons
               .Include(a => a.ProductionCost)
               .FirstOrDefaultAsync(a => a.Id == editedWeapon.Id);

            if (existingWeapon == null)
                return NotFound();

            existingWeapon.Name = editedWeapon.Name;
            existingWeapon.CategoryId = editedWeapon.CategoryId;
            existingWeapon.Description = editedWeapon.Description;
            existingWeapon.CrateAmount = editedWeapon.CrateAmount;
            existingWeapon.AmmunitionId = editedWeapon.AmmunitionId;
            existingWeapon.WeaponProperties = editedWeapon.WeaponProperties;
            existingWeapon.WeaponType = editedWeapon.WeaponType;
            existingWeapon.FactionId = editedWeapon.FactionId;
            existingWeapon.IsTeched = editedWeapon.IsTeched;

            _context.Costs.RemoveRange(existingWeapon.ProductionCost);

            existingWeapon.ProductionCost = editedWeapon.ProductionCost.Select(c => new Cost
            {
                CraftableItemId = c.CraftableItemId,
                Amount = c.Amount,
                ResourceId = c.ResourceId,
                MaterialId = c.MaterialId
            }).ToList();

            _context.SaveChanges();

            return Ok(editedWeapon);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWeaponAsync(int id)
        {
            var weapon = await _context.Weapons.FindAsync(id);
            if (weapon == null)
            {
                return NotFound();
            }

            _context.Weapons.Remove(weapon);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPost("Seed")]
        public async Task<IActionResult> SeedWeaponsAsync()
        {
            await _service.SeedWeaponsAsync();
            return Ok("Weapons seeded successfully!");
        }

    }
}
