using IntelboardAPI.Data;
using IntelboardAPI.Models;
using Microsoft.AspNetCore.Mvc;

// Not currently in use, will be used and better implemented soon
namespace IntelboardAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IntelboardDbContext _context;
        public UserController(IntelboardDbContext context)
        {
            _context = context;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditUserAsync(int id, [FromBody] User editedUser)
        {
            if (editedUser == null || id != editedUser.Id)
            {
                return BadRequest();
            }
            var existingUser = await _context.Users.FindAsync(id);
            if (existingUser == null)
            {
                return NotFound();
            }
            existingUser.Name = editedUser.Name;
            existingUser.Role = editedUser.Role;

            _context.Users.Update(existingUser);
            await _context.SaveChangesAsync();
            return Ok(existingUser);
        }

        // This is only for demo purposes, will be removed and remade for actual users in the future
        [HttpPost("demo-role")]
        public IActionResult SetDemoRole([FromBody] string newRole)
        {
            DemoUser.Role = newRole;
            return Ok(new { success = true, role = newRole });
        }

        public class RoleRequest
        {
            public string Role { get; set; }
        }
    }
}
