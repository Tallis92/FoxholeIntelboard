using FoxholeIntelboard.Models;
using Microsoft.EntityFrameworkCore;

namespace FoxholeIntelboard.Data
{
    public class IntelboardDBContext : DbContext
    {
        public IntelboardDBContext(DbContextOptions<IntelboardDBContext> options)
        : base(options)
        {

        }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Ammunition> Ammunitions { get; set; }
    }
}
