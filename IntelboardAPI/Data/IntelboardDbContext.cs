using IntelboardAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace IntelboardAPI.Data
{
    public class IntelboardDbContext : DbContext
    {
        public IntelboardDbContext(DbContextOptions<IntelboardDbContext> options)
            : base(options)
        {
        }

        public DbSet<CraftableItem> CraftableItems { get; set; }
        public DbSet<Ammunition> Ammunitions { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Cost> Costs { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<Weapon> Weapons { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<CratedItem> CratedItems { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            // 1) Map base
            builder.Entity<CraftableItem>()
                   .UseTptMappingStrategy()          // Använd TPT istället för TPH
                   .ToTable("CraftableItems");

            // 2) Map each derived type
            builder.Entity<Ammunition>()
                   .ToTable("Ammunitions");
            builder.Entity<Material>()
                   .ToTable("Materials");

            // 3) Costs ←→ CraftableItem
            builder.Entity<CraftableItem>()
                   .HasMany(ci => ci.ProductionCost)
                   .WithOne(c => c.CraftableItem)
                   .HasForeignKey(c => c.CraftableItemId)
                   .OnDelete(DeleteBehavior.Cascade);

            // 4) Cost → Material (restrict)
            builder.Entity<Cost>()
                   .ToTable("Costs")
                   .HasOne(c => c.Material)
                   .WithMany()
                   .HasForeignKey(c => c.MaterialId)
                   .OnDelete(DeleteBehavior.Restrict);

            // 5) Cost → Resource (restrict)
            builder.Entity<Cost>()
                   .HasOne(c => c.Resource)
                   .WithMany()
                   .HasForeignKey(c => c.ResourceId)
                   .OnDelete(DeleteBehavior.Restrict);

        }
    }
}