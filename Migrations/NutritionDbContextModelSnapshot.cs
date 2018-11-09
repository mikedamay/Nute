﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Nute;

namespace Nute.Migrations
{
    [DbContext(typeof(NutritionDbContext))]
    partial class NutritionDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Nute.Nutrient", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Nutrients");

                    b.HasData(
                        new { Id = 1L, Name = "Energy" },
                        new { Id = 2L, Name = "Fat" },
                        new { Id = 3L, Name = "Saturated Fat" },
                        new { Id = 4L, Name = "Carbohydrate" },
                        new { Id = 5L, Name = "Sugars" },
                        new { Id = 6L, Name = "Fibre" },
                        new { Id = 7L, Name = "Protein" },
                        new { Id = 8L, Name = "Salt" }
                    );
                });
#pragma warning restore 612, 618
        }
    }
}
