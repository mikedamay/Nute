using System;
using Microsoft.EntityFrameworkCore;
using Nute;
using Nute.Repos;

namespace NuteRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            var dbContext = new NutritionDbContext();
            dbContext.Database.EnsureCreated();
            dbContext.Database.Migrate();
            new CustomSqlDbContext().Database.Migrate();
            var ndh = new NutrientDataHandler(dbContext);
            var nutrients = ndh.LoadNutrients();
            var units = ndh.LoadUnits();
            dbContext.Dispose();
        }
    }
}