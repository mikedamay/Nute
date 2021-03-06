using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Options;
using Nute.Common;
using Nute.Entities;
using Version = Nute.Entities.Version;


// https://www.nutritionix.com/business/api
// https://www.gov.uk/government/publications/composition-of-foods-integrated-dataset-cofid

namespace Nute
{
    public class NutritionDbContext : DbContextBase
    {
        public NutritionDbContext()
        {
            this.ChangeTracker.AutoDetectChangesEnabled = false;
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder ob)
        {
            var lf = CreateLoggerFactoryUsingDI();
            ob.UseLoggerFactory(lf);
            base.OnConfiguring(ob);
        }

        private static ILoggerFactory CreateLoggerFactoryUsingDI()
        {
            var sc = new ServiceCollection();
            sc.AddLogging(
                opts =>
                {
                    opts.SetMinimumLevel(LogLevel.Information);
                    opts.AddConsole();
                });
            var sp = sc.BuildServiceProvider();
            var lf = (ILoggerFactory) sp.GetService(typeof(ILoggerFactory));
            return lf;
        }

        private static ILoggerFactory CreateLoggerFactory()
        {
            var of = new OptionsFactory<ConsoleLoggerOptions>();
            
            var om = new OptionsMonitor<ConsoleLoggerOptions>(of, null, null);
            var lf = new LoggerFactory(
                new [] {new ConsoleLoggerProvider(om) });
            return lf;
        }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            CreateBodyType(mb);
            CreateNutrient(mb);
            CreateUnit(mb);
            CreateVersion(mb);
            CreateUser(mb);
            CreateConstituent(mb);
            CreateIngredient(mb);
//            CreateMealIngredient(mb);
            CreateMeal(mb);
            SeedData(mb);
        }

        private void CreateIngredient(ModelBuilder mb)
        {
            mb.Entity<Ingredient>()
                .Property("_servingSizeCount")
                .HasColumnName("ServingSizeCount")
                .HasColumnType(Constants.StdDecimalSpec);
            mb.Entity<Ingredient>()
                .HasOne<Unit>("_servingSizeUnit")
                .WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull);
           mb.Entity<Ingredient>()
                .HasAlternateKey(i => i.ShortCode)
                .HasName("AK_Ingredient_ShortCode");
            mb.Entity<Ingredient>()
                .HasAlternateKey(i => i.Name)
                .HasName("AK_Ingredient_Name")
                  ;
            mb.Entity<Ingredient>()
                .Property("_servingSizeUnitId")
                .HasColumnName("ServingSizeUnitId");
            mb.Entity<Ingredient>()
                .HasMany(ii => ii.Constituents)
                .WithOne()
                .HasForeignKey("IngredientId")
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }

        private void CreateConstituent(ModelBuilder mb)
        {
            mb.Entity<Constituent>()
                .Property("_quantityCount")
                .HasColumnName("QuantityCount")
                .HasColumnType(Constants.StdDecimalSpec);
            mb.Entity<Constituent>()
                .Property("_quantityUnitId")
                .HasColumnName("QuantityUnitId");
            mb.Entity<Constituent>()
                .HasOne<Unit>("_quantityUnit")
                .WithMany()
                // specifying an FK here will create a shadow
                // property and make that the FK rather than
                // using QuantityUnitId
                // ditto for ServingSize and Ingredient.ServingSize
                // It must insist on inferring the FK name
                // from the navigation property name
                .OnDelete(DeleteBehavior.ClientSetNull);
            mb.Entity<Constituent>()
                .Property("_servingSizeCount")
                .HasColumnName("ServingSizeCount")
                .HasColumnType(Constants.StdDecimalSpec);
            mb.Entity<Constituent>()
                .Property("_servingSizeUnitId")
                .HasColumnName("ServingSizeUnitId");
            mb.Entity<Constituent>()
                .HasOne<Unit>("_servingSizeUnit")
                .WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull);
        }

        private void CreateBodyType(ModelBuilder mb)
        {
            mb.Entity<BodyType>()
                .HasData(
                    new BodyType("Male", "Male Std.", 1L)
                    ,new BodyType("Female", "Female Std.", 2L)
                );
        }

        private void CreateNutrient(ModelBuilder mb)
        {
            mb.Entity<Nutrient>()
                .HasAlternateKey(n => n.ShortCode)
                .HasName("AK_Nutrient_ShortCode");
            mb.Entity<Nutrient>()
                .HasAlternateKey(n => n.Name)
                .HasName("AK_Nutrient_Name");
        }

        private void CreateUnit(ModelBuilder mb)
        {
            mb.Entity<Unit>()
                .Property(typeof(Int64), "Id");
            mb.Entity<Unit>()
                .Property(typeof(string), "Name");
            mb.Entity<Unit>()
                .Property(typeof(string), "Abbrev");
            mb.Entity<Unit>()
                .HasKey("Id");
            mb.Entity<Unit>()
                .HasAlternateKey("Name")
                .HasName("AK_Unit_Name");
            mb.Entity<Unit>()
                .HasAlternateKey("Abbrev")
                .HasName("AK_Unit_Abbrev");
           
        }

        private void CreateVersion(ModelBuilder mb)
        {
            mb.Entity<Version>()
                .HasData(
                    new Version(1, DateTime.Today, null, 1L)
                );
        }

        private void CreateUser(ModelBuilder mb)
        {
            mb.Entity<User>()
                .HasAlternateKey(Constants.Token);
            mb.Entity<User>()
                .HasData(new User(id: 1, token: "magic1"))
                ;
        }

        private void CreateMealIngredient(ModelBuilder mb)
        {
/*
            mb.Entity<MealIngredient>()
                .HasKey(mi => new {mi.MealId, mi.IngredientId}  );
            mb.Entity<MealIngredient>()
                .HasOne<Meal>().WithMany(m => m.Ingredients)
                .HasForeignKey(mi => mi.MealId);
            mb.Entity<MealIngredient>()
                .HasOne<Ingredient>().WithMany(i => i.Meals)
                .HasForeignKey(mi => mi.IngredientId);
*/
        }

        private void CreateMeal(ModelBuilder mb)
        
        {
            mb.Entity<Serving>()
                .HasOne<Meal>()
                .WithMany(m => m.Servings);
        }

        private void SeedData(ModelBuilder mb)
        {
             mb.Entity<Nutrient>().HasData(
                new Nutrient (id : 1, shortCode : TestConstants.EnergySC, name : TestConstants.Energy)
                , new Nutrient (id : 2, shortCode : TestConstants.FatSC, name : TestConstants.Fat)
                , new Nutrient (id : 3, shortCode : TestConstants.SaturatedFatSC, name : TestConstants.SaturatedFat, subsidiary: true)
                , new Nutrient (id : 4, shortCode : TestConstants.CarbohydrateSC, name : TestConstants.Carbohydrate)
                , new Nutrient (id : 5, shortCode : TestConstants.SugarsSC, name : TestConstants.Sugars, subsidiary: true)
                , new Nutrient (id : 6, shortCode : TestConstants.FibreSC, name : TestConstants.Fibre)
                , new Nutrient (id : 7, shortCode : TestConstants.ProteinSC, name : TestConstants.Protein)
                , new Nutrient (id : 8, shortCode : TestConstants.SaltSC, name : TestConstants.Salt)
                , new Nutrient (id : 9, shortCode : TestConstants.ThiaminSC, name : TestConstants.Thiamin)
                , new Nutrient (id : 10, shortCode : TestConstants.RiboflavinSC, name : TestConstants.Riboflavin)
                , new Nutrient (id : 11, shortCode : TestConstants.NiacinSC, name : TestConstants.Niacin)
                , new Nutrient (id : 12, shortCode : TestConstants.VitaminB6SC, name : TestConstants.VitaminB6)
                , new Nutrient (id : 13, shortCode : TestConstants.FolicAcidSC, name : TestConstants.FolicAcid)
                , new Nutrient (id : 14, shortCode : TestConstants.VitaminB12SC, name : TestConstants.VitaminB12)
                , new Nutrient (id : 15, shortCode : TestConstants.IronSC, name : TestConstants.Iron)
            );
            mb.Entity<Unit>().HasData(
               new {Id = 1L, Name = "gram", Abbrev = Nute.Entities.Unit.GRAM}
                ,new {Id = 2L, Name = "each", Abbrev = Nute.Entities.Unit.EACH}
                ,new {Id = 3L, Name = "large", Abbrev = Nute.Entities.Unit.LGE}
                ,new {Id = 4L, Name = "calorie", Abbrev = Nute.Entities.Unit.KCAL}
                ,new {Id = 5L, Name = "milligram", Abbrev = Nute.Entities.Unit.MILLIGRAM}
                ,new {Id = 6L, Name = "micro-gram", Abbrev = Nute.Entities.Unit.MICROGRAM}
            );
           mb.Entity<MealTime>()
                .HasData(
                    new {Id = 1L, Name = "Breakfast", Longevity = MealTimeValueLongevity.System},
                    new {Id = 2L, Name = "Lunch", Longevity = MealTimeValueLongevity.System},
                    new {Id = 3L, Name = "Dinner", Longevity = MealTimeValueLongevity.System}
                );
            mb.Entity<Meal>()
                .HasData(
                    new {Id = 1L, MealTimeId = 1L }
                );
            mb.Entity<Ingredient>()
                .HasData(
                    new
                    {
                        Id = 1L, ShortCode = "BRNFL", Name = "Bran Flakes"
                        ,_servingSizeCount = 125m, _servingSizeUnitId = 1L
                    }
                    );
            mb.Entity<Serving>()
                .HasData(
                    new
                    {
                        Id = 1L, IngredientId = 1L, MealId = 1L
                        ,_quantityCount = 125m, _quantityUnitId = 1L
                    }
                );
            mb.Entity<Constituent>()
                .HasData(
                    new {Id = 1L, NutrientId = 1L
                      , _quantityCount = 100m, _quantityUnitId = 1L, IngredientId = 1L
                      , _servingSizeCount = 125m, _servingSizeUnitId = 1L
                      }
                );

        }

        public DbSet<Nutrient> Nutrient { get; set; }
        public DbSet<Unit> Unit { get; set; }
        public DbSet<Version> Version { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<BodyType> BodyType { get; set; }
        public DbSet<Constituent> Constituent { get; set; }
        public DbSet<Ingredient> Ingredient { get; set; }
        public DbSet<Meal> Meal { get; set; }
        public DbSet<MealTime> MealTime { get; set; }
        public DbSet<ScheduledMeal> ScheduledMeal { get; set; }
    }

    public class OptionsFactory<T> : IOptionsFactory<T> where T : class, new()
    {
        public T Create(string name)
        {
            return new T();
        }
    }
}
