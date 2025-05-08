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

            modelBuilder.Entity<CraftableItem>().HasKey(c => c.Id);
            modelBuilder.Entity<Material>().ToTable("Materials");
            modelBuilder.Entity<CraftableItem>()
                .HasMany(c => c.ProductionCost)
                .WithOne(c => c.CraftableItem)
                .HasForeignKey(c => c.CraftableItemId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Cost>().ToTable("Costs");

            modelBuilder.Entity<Cost>()
                .HasOne(c => c.Material)
                .WithMany()
                .HasForeignKey(c => c.MaterialId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Cost>()
                .HasOne(c => c.Resource)
                .WithMany()
                .HasForeignKey(c => c.ResourceId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CraftableItem>().ToTable("CraftableItems");

            base.OnModelCreating(modelBuilder);

           
        }
    }
}