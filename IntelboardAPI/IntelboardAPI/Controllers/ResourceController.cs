using IntelboardAPI.Data;
using IntelboardAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;

namespace IntelboardAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResourceController : Controller
    {
        private readonly IntelboardDbContext _context;
        private readonly Services.IResourceService _resourceService;

        public ResourceController(IntelboardDbContext context, Services.IResourceService resourceService)
        {
            _context = context;
            _resourceService = resourceService;
        }

        [HttpGet]
        public async Task<List<Resource>> GetResourcesAsync()
        {
            var resources = await _context.Resources.ToListAsync();

            return resources;

        }
        [HttpPost("Seed")]
        public async Task<IActionResult> SeedResourcesAsync()
        {
            await _resourceService.SeedResourcesAsync();
            return Ok("Status kod 200");
        }

    }
}
