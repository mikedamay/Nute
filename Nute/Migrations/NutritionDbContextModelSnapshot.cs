﻿// <auto-generated />
using System;
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
                .HasAnnotation("ProductVersion", "2.1.3-rtm-32065")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Nute.Entities.BodyType", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.ToTable("BodyType");

                    b.HasData(
                        new { Id = 1L, Description = "Male Std.", Name = "Male" },
                        new { Id = 2L, Description = "Female Std.", Name = "Female" }
                    );
                });

            modelBuilder.Entity("Nute.Entities.Constituent", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("IngredientId")
                        .IsRequired();

                    b.Property<long>("NutrientId");

                    b.Property<decimal>("_quantityCount")
                        .HasColumnName("QuantityCount")
                        .HasColumnType("decimal(18,4)");

                    b.Property<long>("_quantityUnitId")
                        .HasColumnName("QuantityUnitId");

                    b.Property<decimal>("_servingSizeCount")
                        .HasColumnName("ServingSizeCount")
                        .HasColumnType("decimal(18,4)");

                    b.Property<long>("_servingSizeUnitId")
                        .HasColumnName("ServingSizeUnitId");

                    b.HasKey("Id");

                    b.HasIndex("IngredientId");

                    b.HasIndex("NutrientId");

                    b.HasIndex("_quantityUnitId");

                    b.HasIndex("_servingSizeUnitId");

                    b.ToTable("Constituent");

                    b.HasData(
                        new { Id = 1L, IngredientId = 1L, NutrientId = 1L, _quantityCount = 100m, _quantityUnitId = 1L, _servingSizeCount = 125m, _servingSizeUnitId = 1L }
                    );
                });

            modelBuilder.Entity("Nute.Entities.Ingredient", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("ShortCode")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<decimal>("_servingSizeCount")
                        .HasColumnName("ServingSizeCount")
                        .HasColumnType("decimal(18,4)");

                    b.Property<long>("_servingSizeUnitId")
                        .HasColumnName("ServingSizeUnitId");

                    b.HasKey("Id");

                    b.HasAlternateKey("Name")
                        .HasName("AK_Ingredient_Name");


                    b.HasAlternateKey("ShortCode")
                        .HasName("AK_Ingredient_ShortCode");

                    b.HasIndex("_servingSizeUnitId");

                    b.ToTable("Ingredient");

                    b.HasData(
                        new { Id = 1L, Name = "Bran Flakes", ShortCode = "BRNFL", _servingSizeCount = 125m, _servingSizeUnitId = 1L }
                    );
                });

            modelBuilder.Entity("Nute.Entities.Meal", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("MealTimeId");

                    b.HasKey("Id");

                    b.HasIndex("MealTimeId");

                    b.ToTable("Meal");

                    b.HasData(
                        new { Id = 1L, MealTimeId = 1L }
                    );
                });

            modelBuilder.Entity("Nute.Entities.MealIngredient", b =>
                {
                    b.Property<long>("MealId");

                    b.Property<long>("IngredientId");

                    b.HasKey("MealId", "IngredientId");

                    b.HasIndex("IngredientId");

                    b.ToTable("MealIngredient");
                });

            modelBuilder.Entity("Nute.Entities.MealTime", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Longevity");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("MealTime");

                    b.HasData(
                        new { Id = 1L, Longevity = 1, Name = "Breakfast" },
                        new { Id = 2L, Longevity = 1, Name = "Lunch" },
                        new { Id = 3L, Longevity = 1, Name = "Dinner" }
                    );
                });

            modelBuilder.Entity("Nute.Entities.Nutrient", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("ShortCode")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<bool>("Subsidiary");

                    b.HasKey("Id");

                    b.HasAlternateKey("Name")
                        .HasName("AK_Nutrient_Name");


                    b.HasAlternateKey("ShortCode")
                        .HasName("AK_Nutrient_ShortCode");

                    b.ToTable("Nutrient");

                    b.HasData(
                        new { Id = 1L, Name = "Energy", ShortCode = "ENERGY", Subsidiary = false },
                        new { Id = 2L, Name = "Fat", ShortCode = "FAT", Subsidiary = false },
                        new { Id = 3L, Name = "Saturated Fat", ShortCode = "SATFAT", Subsidiary = true },
                        new { Id = 4L, Name = "Carbohydrate", ShortCode = "CARB", Subsidiary = false },
                        new { Id = 5L, Name = "Sugars", ShortCode = "SUGAR", Subsidiary = true },
                        new { Id = 6L, Name = "Fibre", ShortCode = "FIBRE", Subsidiary = false },
                        new { Id = 7L, Name = "Protein", ShortCode = "PROTEIN", Subsidiary = false },
                        new { Id = 8L, Name = "Salt", ShortCode = "SALT", Subsidiary = false },
                        new { Id = 9L, Name = "Thiamin (B1)", ShortCode = "B1", Subsidiary = false },
                        new { Id = 10L, Name = "Riboflavin (B2)", ShortCode = "B2", Subsidiary = false },
                        new { Id = 11L, Name = "Niacin", ShortCode = "NIACIN", Subsidiary = false },
                        new { Id = 12L, Name = "Vitamin B6", ShortCode = "B6", Subsidiary = false },
                        new { Id = 13L, Name = "Folic Acid (B9)", ShortCode = "B9", Subsidiary = false },
                        new { Id = 14L, Name = "Vitamin B12", ShortCode = "B12", Subsidiary = false },
                        new { Id = 15L, Name = "Iron", ShortCode = "IRON", Subsidiary = false }
                    );
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
                        new { Id = 2L, Abbrev = "each", Name = "each" },
                        new { Id = 3L, Abbrev = "lge", Name = "large" },
                        new { Id = 4L, Abbrev = "kcal", Name = "calorie" },
                        new { Id = 5L, Abbrev = "mg", Name = "milligram" },
                        new { Id = 6L, Abbrev = "μg", Name = "micro-gram" }
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
                        new { Id = 1L, SequenceNumber = 1, StartDate = new DateTime(2019, 1, 11, 0, 0, 0, 0, DateTimeKind.Local) }
                    );
                });

            modelBuilder.Entity("Nute.Entities.Constituent", b =>
                {
                    b.HasOne("Nute.Entities.Ingredient")
                        .WithMany("Constituents")
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Nute.Entities.Nutrient", "Nutrient")
                        .WithMany()
                        .HasForeignKey("NutrientId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Nute.Entities.Unit", "_quantityUnit")
                        .WithMany()
                        .HasForeignKey("_quantityUnitId");

                    b.HasOne("Nute.Entities.Unit", "_servingSizeUnit")
                        .WithMany()
                        .HasForeignKey("_servingSizeUnitId");
                });

            modelBuilder.Entity("Nute.Entities.Ingredient", b =>
                {
                    b.HasOne("Nute.Entities.Unit", "_servingSizeUnit")
                        .WithMany()
                        .HasForeignKey("_servingSizeUnitId");
                });

            modelBuilder.Entity("Nute.Entities.Meal", b =>
                {
                    b.HasOne("Nute.Entities.MealTime", "MealTime")
                        .WithMany()
                        .HasForeignKey("MealTimeId");
                });

            modelBuilder.Entity("Nute.Entities.MealIngredient", b =>
                {
                    b.HasOne("Nute.Entities.Ingredient")
                        .WithMany("Meals")
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Nute.Entities.Meal")
                        .WithMany("Ingredients")
                        .HasForeignKey("MealId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
