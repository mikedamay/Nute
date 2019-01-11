using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Nute.Entities;

/*
    Migrations Workflow (incl. Custom Migrations)
    ---------------------------------------------
 
    Constraints & Dependencies:
    a) We git commit NutritionDbContextModelSnapshot which is required to support Database.Migrate()
    b) We git ignore model based migrations (in NutritionDbContext) so as not to confuse other devs.
    c) Database is disposable - completely re-constructable
    d) Model based migrations should not have dependencies on custom migrations.

    From scratch (from clean install of code-base):
    ******** DESTRUCTIVE *********
    1.1. drop all objects from database (including _EFMigrationHistory) (but leave the "nutrition" database itself
    1.2. remove any migrations generated from model (i.e. any migrations that are direct
       children of the Migrations directory) that happen to be hanging about.  Do not
       remove any files in the Migrations/CustomSql directory.
    1.3. delete the entire contents of NutritionDbContextModelSnapshot 
       but leave the empty file in place so that git is happy (because this is a book mark
       for the current state of of migrations generated from model - we need that to be empty)
    1.4. execute "dotnet ef migrations add --context NutritionDbContext start" 
       - to generate migrations from model
    1.5. execute "dotnet ef database update --context NutritionDbContext"
     
    Incremental (how to handle model changes on dev's machine as code evolves):
    2.1. execute "dotnet ef migrations add --context NutritionDbContext <something>" - to generate migrations from model
    2.2. execute "dotnet ef database update --context NutritionDbContext"
         i.e you can simply operate on the nutrition db context using default commands
         ke "migrations remove" and "database update 0" should work well.
    
    Changes to custom scripts:
    2.0. Do not touch at any stage the dated migration scripts in Migrations/CustomSql
         20181203085019_custom.cs and its cognate or CustomSqlDbContextModelSnapshot.cs
    3.1. execute "dotnet ef database update --context CustomSqlDbContext 0"
        check: views created by the script should no longer be present in the Nutrition database 
    3.2. Make changes to migrations/CustomSql/Scripts.cs. to add or remove views
    3.3. execute "dotnet ef database update --context CustomSqlContext"
        check: views created by the script should now be present in the Nutrition database
        
        if you ever need to regenerate the date migration scripts then the command line
        is "C:\projects\Nute\Nute>dotnet ef migrations add custom --context CustomSqlDbContext --output-dir Migrations/CustomSql"
        after executing that you then need to add in the lines
        SundryViews.Up(migrationBuilder);
        and
        SundryViews.Down(migrationBuilder);
        in the Up and Down methods of <date>_custom.cs

    NOTE:
    A. Before committing, always ensure that there is a valid version of
       NutritionDbContextModelSnapshot.cs to commit to avoid confusion
       and, in particular so that migration-at-startup will work.
    B. migration-at-startup: both the runner exe and the unit tests will
       create the database and execute the migrations if the database or
       migrations are missing and the migration snapshot is available.
    C. A combination that does not work at startup is the presence of incremental migration
       files and an empty (or non-matching) database.  This causes a run-time error.
       Remedy by following the steps in section 1 above.
*/

namespace Nute.Migrations.CustomSql
{
    public static partial class SundryViews
    {
        internal static void Up(MigrationBuilder mib)
        {
            mib.Sql(upScript);
            AddMealTimeValueLongevityTable(mib);
        }

        private static void AddMealTimeValueLongevityTable(MigrationBuilder mib)
        {
            mib.Sql(longevityUpScript);
            var template = "insert into Longevity([Code],[Description]) values({0}, '{1}'){2}go";
            foreach (var e in Enum.GetValues(typeof(MealTimeValueLongevity)))
            {
                mib.Sql(string.Format(template, (int)e, e.ToString(), Environment.NewLine));
            }
        }

        internal static void Down(MigrationBuilder mib)
        {
            mib.Sql(downScript);
            mib.Sql(longevityDownScript);
        }
    }
}
