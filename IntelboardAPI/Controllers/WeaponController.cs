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
            var weapon = await _context.Weapons.FindAsync(id);
            if (weapon == null)
            {
                return NotFound();
            }
            return Ok(weapon);
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
            _context.Weapons.Update(editedWeapon);
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
