using IntelboardAPI.Data;
using IntelboardCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;
using IntelboardCore.Services;

namespace IntelboardAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResourceController : Controller
    {
        private readonly IntelboardDbContext _context;
        private readonly IntelboardCore.Services.IResourceService _resourceService;

        public ResourceController(IntelboardDbContext context, IntelboardCore.Services.IResourceService resourceService)
        {
            _context = context;
            _resourceService = resourceService;
        }

        [HttpGet]
        public async Task<List<Resource>> GetResourcesAsync()
        {
            return await _context.Resources.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Resource>> GetResourceByIdAsync(int id)
        {
            var resource = await _context.Resources.FindAsync(id);
            if (resource == null)
            {
                return NotFound();
            }
            return Ok(resource);
        }

        [HttpPost]
        public async Task<IActionResult> CreateResourceAsync(Resource resource)
        {
            if (resource == null)
            {
                return BadRequest();
            }
            await _context.Resources.AddAsync(resource);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditResourceAsync(Resource editedResource)
        {

            if (editedResource == null)
            {
                return NotFound();
            }
            _context.Resources.Update(editedResource);
            await _context.SaveChangesAsync();

            return Ok(editedResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResourceAsync(int id)
        {
            var resource = await _context.Resources.FindAsync(id);
            if (resource == null)
            {
                return NotFound();
            }

            _context.Resources.Remove(resource);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("Seed")]
        public async Task<IActionResult> SeedResourcesAsync()
        {
            await _resourceService.SeedResourcesAsync();
            return Ok("Resources seeded successfully!");
        }
        
    }
}
