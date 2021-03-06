using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Nute.Entities;
using Nute.Repos;
using Xunit;
using Version = Nute.Entities.Version;

namespace Nute
{
    public class Runner
    {
        private readonly NutritionDbContext dbContext;

        public Runner()
        {
            dbContext = new NutritionDbContext();
            dbContext.Database.EnsureCreated();
            dbContext.Database.Migrate();
            new CustomSqlDbContext().Database.Migrate();
            dbContext.Database.BeginTransaction();
        }

        [Fact]
        public void Write_Nutrient()
        {
            dbContext.Nutrient.Add(new Nutrient(id: 0, shortCode: "TEST", name: "Test Nutrient"));
            dbContext.SaveChanges();
            Assert.NotNull(dbContext.Nutrient.ToList().FirstOrDefault(n => n.Name == "Test Nutrient"));
        }

        [Fact]
        public void Write_Ingredient()
        {
            var ndh = new NutrientDataHandler(dbContext);
            var nutrients = ndh.LoadNutrients();
            var units = ndh.LoadUnits();
            var branFlakes = CreateIngredient("BF1", "Bran Flakes T1"
                , nutrients, units);
            ndh.SaveIngredient(branFlakes);
            var loaded = ndh.GetIngredient(branFlakes.Id);
            dbContext.Database.RollbackTransaction();
        }

        [Fact]
        public void Delete_Ingredient()
        {
            var ndh = new NutrientDataHandler(dbContext);
            var nutrients = ndh.LoadNutrients();
            var units = ndh.LoadUnits();
            var branFlakes = CreateIngredient("BF2", "Bran Flakes T2"
                , nutrients, units);
            ndh.SaveIngredient(branFlakes);
            var ingredient = ndh.GetIngredient(branFlakes.Id);
            ndh.DeleteIngredient(ingredient);
            dbContext.Database.RollbackTransaction();
        }

        private Ingredient CreateIngredient(string shortCode, string name
            , IReadOnlyDictionary<string, Nutrient> nutrients
            , IReadOnlyDictionary<string, Unit> units)
        {
            var ingredient = new Ingredient(
                shortCode: "BFMS_TEST"
                , name: "Bran Flakes (M&S) Test"
                , servingSize: new Quantity(125, units[Unit.GRAM])
                , constituents: new List<Constituent>
                {
                    new Constituent(
                        nutrient: nutrients[TestConstants.Energy]
                        , quantity: new Quantity(354, units[Unit.KCAL])
                        , servingSize: new Quantity(100, units[Unit.GRAM])),
                    new Constituent(
                        nutrient: nutrients[TestConstants.Fat]
                        , quantity: new Quantity(2, units[Unit.GRAM])
                        , servingSize: new Quantity(100, units[Unit.GRAM])),
                    new Constituent(
                        nutrient: nutrients[TestConstants.SaturatedFat]
                        , quantity: new Quantity((decimal) 0.3, units[Unit.GRAM])
                        , servingSize: new Quantity(100, units[Unit.GRAM])),
                    new Constituent(
                        nutrient: nutrients[TestConstants.Carbohydrate]
                        , quantity: new Quantity((decimal) 64.5, units[Unit.GRAM])
                        , servingSize: new Quantity(100, units[Unit.GRAM])),
                    new Constituent(
                        nutrient: nutrients[TestConstants.Sugars]
                        , quantity: new Quantity((decimal) 15.6, units[Unit.GRAM])
                        , servingSize: new Quantity(100, units[Unit.GRAM])),
                    new Constituent(
                        nutrient: nutrients[TestConstants.Fibre]
                        , quantity: new Quantity((decimal) 16.2, units[Unit.GRAM])
                        , servingSize: new Quantity(100, units[Unit.GRAM])),
                    new Constituent(
                        nutrient: nutrients[TestConstants.Protein]
                        , quantity: new Quantity((decimal) 11.3, units[Unit.GRAM])
                        , servingSize: new Quantity(100, units[Unit.GRAM])),
                    new Constituent(
                        nutrient: nutrients[TestConstants.Salt]
                        , quantity: new Quantity((decimal) 1.03, units[Unit.GRAM])
                        , servingSize: new Quantity(100, units[Unit.GRAM])),
                    new Constituent(
                        nutrient: nutrients[TestConstants.Thiamin]
                        , quantity: new Quantity((decimal) 0.8, units[Unit.MILLIGRAM])
                        , servingSize: new Quantity(100, units[Unit.GRAM])),
                    new Constituent(
                        nutrient: nutrients[TestConstants.Riboflavin]
                        , quantity: new Quantity((decimal) 1.1, units[Unit.MILLIGRAM])
                        , servingSize: new Quantity(100, units[Unit.GRAM])),
                    new Constituent(
                        nutrient: nutrients[TestConstants.Niacin]
                        , quantity: new Quantity((decimal) 20, units[Unit.MILLIGRAM])
                        , servingSize: new Quantity(100, units[Unit.GRAM])),
                    new Constituent(
                        nutrient: nutrients[TestConstants.VitaminB6]
                        , quantity: new Quantity((decimal) 1.7, units[Unit.MILLIGRAM])
                        , servingSize: new Quantity(100, units[Unit.GRAM])),
                    new Constituent(
                        nutrient: nutrients[TestConstants.FolicAcid]
                        , quantity: new Quantity((decimal) 395, units[Unit.MICROGRAM])
                        , servingSize: new Quantity(100, units[Unit.GRAM])),
                    new Constituent(
                        nutrient: nutrients[TestConstants.VitaminB12]
                        , quantity: new Quantity((decimal) 0.4, units[Unit.MICROGRAM])
                        , servingSize: new Quantity(100, units[Unit.GRAM])),
                    new Constituent(
                        nutrient: nutrients[TestConstants.Iron]
                        , quantity: new Quantity((decimal) 11, units[Unit.MILLIGRAM])
                        , servingSize: new Quantity(100, units[Unit.GRAM])),
                }
            );
            return ingredient;
        }
    }

    public static class TestConstants
    {
        public const string Energy = "Energy";
        public const string Fat = "Fat";
        public const string SaturatedFat = "Saturated Fat";
        public const string Carbohydrate = "Carbohydrate";
        public const string Sugars = "Sugars";
        public const string Fibre = "Fibre";
        public const string Protein = "Protein";
        public const string Salt = "Salt";
        public const string Thiamin = "Thiamin (B1)";
        public const string Riboflavin = "Riboflavin (B2)";
        public const string Niacin = "Niacin";
        public const string VitaminB6 = "Vitamin B6";
        public const string FolicAcid = "Folic Acid (B9)";
        public const string VitaminB12 = "Vitamin B12";
        public const string Iron = "Iron";

        public const string EnergySC = "ENERGY";
        public const string FatSC = "FAT";
        public const string SaturatedFatSC = "SATFAT";
        public const string CarbohydrateSC = "CARB";
        public const string SugarsSC = "SUGAR";
        public const string FibreSC = "FIBRE";
        public const string ProteinSC = "PROTEIN";
        public const string SaltSC = "SALT";
        public const string ThiaminSC = "B1";
        public const string RiboflavinSC = "B2";
        public const string NiacinSC = "NIACIN";
        public const string VitaminB6SC = "B6";
        public const string FolicAcidSC = "B9";
        public const string VitaminB12SC = "B12";
        public const string IronSC = "IRON";
    }
}