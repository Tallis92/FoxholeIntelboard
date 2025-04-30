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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Konfigurera CraftableItem som abstrakt
            modelBuilder.Entity<CraftableItem>()
                .HasKey(c => c.Id);

            // Konfigurera relationen mellan CraftableItem och Cost
            modelBuilder.Entity<CraftableItem>()
                .HasMany(c => c.ProductionCost)
                .WithOne(c => c.CraftableItem)
                .HasForeignKey(c => c.CraftableItemId)
                .OnDelete(DeleteBehavior.Cascade);

            // Konfigurera TPT för subklasser
            modelBuilder.Entity<Ammunition>().ToTable("Ammunitions");
            modelBuilder.Entity<Material>().ToTable("Materials");
            modelBuilder.Entity<Resource>().ToTable("Resources");
            modelBuilder.Entity<Cost>().ToTable("Cost");

            base.OnModelCreating(modelBuilder);
        }
    }
}