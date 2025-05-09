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
        public DbSet<Cost> Costs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<CraftableItem>()
                   .UseTptMappingStrategy()  
                   .ToTable("CraftableItems");

            builder.Entity<Material>()
                   .ToTable("Materials");
            builder.Entity<Ammunition>()
                   .ToTable("Ammunitions");

            builder.Entity<CraftableItem>()
                   .HasMany(ci => ci.ProductionCost)
                   .WithOne(c => c.CraftableItem)
                   .HasForeignKey(c => c.CraftableItemId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Cost>()
                   .ToTable("Costs")
                   .HasOne(c => c.Material)
                   .WithMany()
                   .HasForeignKey(c => c.MaterialId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Cost>()
                   .HasOne(c => c.Resource)
                   .WithMany()
                   .HasForeignKey(c => c.ResourceId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}