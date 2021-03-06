using System.Runtime.CompilerServices;

namespace Nute.Migrations.CustomSql
{
    public static partial class SundryViews
    {
        private static string upScript = @"
create view Nutrient_v
  as
    select Nutrient.Id
        , Nutrient.Name as Nutrient
    from Nutrient
go
create view BodyType_v
  as
    select BodyType.Id
        , BodyType.Name
    from BodyType
go

IF NOT EXISTS (SELECT NULL FROM SYS.EXTENDED_PROPERTIES WHERE [major_id] = OBJECT_ID('Nutrient') AND [name] = N'MS_Description' AND [minor_id] = (SELECT [column_id] FROM SYS.COLUMNS WHERE [name] = 'Subsidiary' AND [object_id] = OBJECT_ID('Nutrient')))
EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'subsidiary nutrients (e.g. saturated fats) would not be included in any calculation of an ingredients weight - if we actually did that',
    @level0type = N'Schema', @level0name = 'dbo',
    @level1type = N'Table', @level1name = 'Nutrient', 
    @level2type = N'Column', @level2name = 'Subsidiary'
go

create view Ingredient_v as
  select Ingredient.ShortCode as [Short Code],
         ingredient.Name as                                                     Name,
         Nutrient.ShortCode as [Nutrient],
         Nutrient.Name   as                                                     [Nutrient-Name],
         concat(format(Constituent.QuantityCount, '###0.##'), QtyUnit.Abbrev)   [Qty per Serving],
         concat(format(Constituent.ServingSizeCount, '###0.##'), SsUnit.Abbrev) [Serving Size]
  from Constituent
         inner join Ingredient on Constituent.IngredientId = Ingredient.Id
         inner join Nutrient on Constituent.NutrientId = Nutrient.Id
         inner join Unit as QtyUnit on Constituent.QuantityUnitId = QtyUnit.Id
         inner join Unit as SsUnit on Constituent.ServingSizeUnitId = SsUnit.Id
go
            ";

        private static string downScript = @"
drop view Nutrient_v
go
drop view BodyType_v
go
EXEC sp_dropextendedproperty 
    @name = N'MS_Description',
    @level0type = N'Schema', @level0name = 'dbo',
    @level1type = N'Table', @level1name = 'Nutrient', 
    @level2type = N'Column', @level2name = 'Subsidiary'
go

drop view Ingredient_v
go
";

private static string longevityUpScript = @"
create table Longevity (
    [Code] int not null,
    [Description] nvarchar(50) not null,
    constraint [PK_Longevity] primary key ([Code]),
    constraint [AK_Longevity_Description] unique ([Description]) 
)
go
";

private static string mealtimeLongevityFKUpScript = @"
alter table MealTime
  add constraint FK_MealTime_Longevity foreign key (Longevity) references Longevity
go
";        
        
        
public static string longevityDownScript = @"
drop table Longevity
go
";

private static string mealtimeLongevityFKDownScript = @"
alter table MealTime
  drop constraint FK_MealTime_Longevity
go
";        
        
    }
}