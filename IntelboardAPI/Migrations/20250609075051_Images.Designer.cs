﻿// <auto-generated />
using System;
using IntelboardAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace IntelboardAPI.Migrations
{
    [DbContext(typeof(IntelboardDbContext))]
    [Migration("20250609075051_Images")]
    partial class Images
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("IntelboardAPI.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "name");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("IntelboardAPI.Models.Cost", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Amount")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "amount");

                    b.Property<int>("CraftableItemId")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "craftableItemId");

                    b.Property<int?>("MaterialId")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "materialId");

                    b.Property<int?>("ResourceId")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "resourceId");

                    b.HasKey("Id");

                    b.HasIndex("CraftableItemId");

                    b.HasIndex("MaterialId");

                    b.HasIndex("ResourceId");

                    b.ToTable("Costs", (string)null);

                    b.HasAnnotation("Relational:JsonPropertyName", "productionCost");
                });

            modelBuilder.Entity("IntelboardAPI.Models.CraftableItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "image");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "name");

                    b.HasKey("Id");

                    b.ToTable("CraftableItems", (string)null);

                    b
                        .UseTptMappingStrategy()
                        .HasAnnotation("Relational:JsonPropertyName", "craftableItem");
                });

            modelBuilder.Entity("IntelboardAPI.Models.CratedItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Amount")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "amount");

                    b.Property<int>("CraftableItemId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "description");

                    b.Property<Guid?>("InventoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("RequiredAmount")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "requiredAmount");

                    b.HasKey("Id");

                    b.HasIndex("CraftableItemId");

                    b.HasIndex("InventoryId");

                    b.ToTable("CratedItems");

                    b.HasAnnotation("Relational:JsonPropertyName", "cratedItems");
                });

            modelBuilder.Entity("IntelboardAPI.Models.Inventory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasAnnotation("Relational:JsonPropertyName", "id");

                    b.Property<int?>("FactionId")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "factionId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "name");

                    b.HasKey("Id");

                    b.ToTable("Inventories");
                });

            modelBuilder.Entity("IntelboardAPI.Models.Resource", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "image");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "name");

                    b.HasKey("Id");

                    b.ToTable("Resources");

                    b.HasAnnotation("Relational:JsonPropertyName", "resource");
                });

            modelBuilder.Entity("IntelboardAPI.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("IntelboardAPI.Models.Ammunition", b =>
                {
                    b.HasBaseType("IntelboardAPI.Models.CraftableItem");

                    b.PrimitiveCollection<string>("AmmoProperties")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "ammoProperties");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "categoriId");

                    b.Property<int>("CrateAmount")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "crateAmount");

                    b.Property<int>("DamageType")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "damage");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "description");

                    b.ToTable("Ammunitions", (string)null);
                });

            modelBuilder.Entity("IntelboardAPI.Models.Material", b =>
                {
                    b.HasBaseType("IntelboardAPI.Models.CraftableItem");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "categoryId");

                    b.Property<int>("CrateAmount")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "crateAmount");

                    b.Property<bool?>("FacilityMade")
                        .HasColumnType("bit")
                        .HasAnnotation("Relational:JsonPropertyName", "facilitymade");

                    b.Property<bool?>("LargeMaterial")
                        .HasColumnType("bit")
                        .HasAnnotation("Relational:JsonPropertyName", "largeMaterial");

                    b.Property<bool?>("TechMaterial")
                        .HasColumnType("bit")
                        .HasAnnotation("Relational:JsonPropertyName", "techMaterial");

                    b.ToTable("Materials", (string)null);

                    b.HasAnnotation("Relational:JsonPropertyName", "material");
                });

            modelBuilder.Entity("IntelboardAPI.Models.Weapon", b =>
                {
                    b.HasBaseType("IntelboardAPI.Models.CraftableItem");

                    b.Property<int>("AmmunitionId")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "ammunitionId");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "categoryId");

                    b.Property<int>("CrateAmount")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "crateAmount");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "description");

                    b.Property<int>("FactionId")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "factionId");

                    b.Property<bool>("IsTeched")
                        .HasColumnType("bit")
                        .HasAnnotation("Relational:JsonPropertyName", "isTeched");

                    b.PrimitiveCollection<string>("WeaponProperties")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "weaponProperties");

                    b.Property<int>("WeaponType")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "weaponType");

                    b.ToTable("Weapons");
                });

            modelBuilder.Entity("IntelboardAPI.Models.Cost", b =>
                {
                    b.HasOne("IntelboardAPI.Models.CraftableItem", "CraftableItem")
                        .WithMany("ProductionCost")
                        .HasForeignKey("CraftableItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IntelboardAPI.Models.Material", "Material")
                        .WithMany()
                        .HasForeignKey("MaterialId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("IntelboardAPI.Models.Resource", "Resource")
                        .WithMany()
                        .HasForeignKey("ResourceId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("CraftableItem");

                    b.Navigation("Material");

                    b.Navigation("Resource");
                });

            modelBuilder.Entity("IntelboardAPI.Models.CratedItem", b =>
                {
                    b.HasOne("IntelboardAPI.Models.CraftableItem", "CraftableItem")
                        .WithMany()
                        .HasForeignKey("CraftableItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IntelboardAPI.Models.Inventory", null)
                        .WithMany("CratedItems")
                        .HasForeignKey("InventoryId");

                    b.Navigation("CraftableItem");
                });

            modelBuilder.Entity("IntelboardAPI.Models.Ammunition", b =>
                {
                    b.HasOne("IntelboardAPI.Models.CraftableItem", null)
                        .WithOne()
                        .HasForeignKey("IntelboardAPI.Models.Ammunition", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("IntelboardAPI.Models.Material", b =>
                {
                    b.HasOne("IntelboardAPI.Models.CraftableItem", null)
                        .WithOne()
                        .HasForeignKey("IntelboardAPI.Models.Material", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("IntelboardAPI.Models.Weapon", b =>
                {
                    b.HasOne("IntelboardAPI.Models.CraftableItem", null)
                        .WithOne()
                        .HasForeignKey("IntelboardAPI.Models.Weapon", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("IntelboardAPI.Models.CraftableItem", b =>
                {
                    b.Navigation("ProductionCost");
                });

            modelBuilder.Entity("IntelboardAPI.Models.Inventory", b =>
                {
                    b.Navigation("CratedItems");
                });
#pragma warning restore 612, 618
        }
    }
}
