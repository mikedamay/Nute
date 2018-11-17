﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Nute;

namespace Nute.Migrations
{
    [DbContext(typeof(NutritionDbContext))]
    [Migration("20181117065817_NutritionProfileView")]
    partial class NutritionProfileView
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.3-rtm-t000")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Nute.Entities.Nutrient", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasAlternateKey("Name")
                        .HasName("AK_Nutrient_Name");

                    b.ToTable("Nutrient");

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

            modelBuilder.Entity("Nute.Entities.NutrientProfile", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<long>("NutrientId");

                    b.Property<long>("VersionId");

                    b.Property<decimal>("_dailyRecommendedMaxCount");

                    b.Property<long>("_dailyRecommendedMaxUnitId");

                    b.HasKey("Id");

                    b.HasAlternateKey("NutrientId", "VersionId")
                        .HasName("AK_NutritionID_VersionId");

                    b.HasIndex("VersionId")
                        .IsUnique();

                    b.HasIndex("_dailyRecommendedMaxUnitId");

                    b.ToTable("NutrientProfile");
                });

            modelBuilder.Entity("Nute.Entities.Unit", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Abbrev")
                        .IsRequired()
                        .HasMaxLength(5);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasAlternateKey("Abbrev")
                        .HasName("AK_Unit_Abbrev");


                    b.HasAlternateKey("Name")
                        .HasName("AK_Unit_Name");

                    b.ToTable("Unit");

                    b.HasData(
                        new { Id = 1L, Abbrev = "g", Name = "gram" },
                        new { Id = 2L, Abbrev = "ea", Name = "each" },
                        new { Id = 3L, Abbrev = "lge", Name = "large" }
                    );
                });

            modelBuilder.Entity("Nute.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasMaxLength(64);

                    b.HasKey("Id");

                    b.HasAlternateKey("Token");

                    b.ToTable("User");

                    b.HasData(
                        new { Id = 1L, Token = "magic1" }
                    );
                });

            modelBuilder.Entity("Nute.Entities.Version", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("date");

                    b.Property<int>("SequenceNumber");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("date");

                    b.HasKey("Id");

                    b.ToTable("Version");

                    b.HasData(
                        new { Id = 1L, SequenceNumber = 1, StartDate = new DateTime(2018, 11, 17, 0, 0, 0, 0, DateTimeKind.Local) }
                    );
                });

            modelBuilder.Entity("Nute.Entities.NutrientProfile", b =>
                {
                    b.HasOne("Nute.Entities.Nutrient", "Nutrient")
                        .WithMany()
                        .HasForeignKey("NutrientId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Nute.Entities.Version", "Version")
                        .WithOne("NutrientProfile")
                        .HasForeignKey("Nute.Entities.NutrientProfile", "VersionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Nute.Entities.Unit", "_dailyRecommendedMaxUnit")
                        .WithMany()
                        .HasForeignKey("_dailyRecommendedMaxUnitId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
