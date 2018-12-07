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

EXEC sp_addextendedproperty 
    @name = N'MS_Description', @value = 'subsidiary nutrients (e.g. saturated fats) would not be included in any calculation of an ingredients weight - if we actually did that',
    @level0type = N'Schema', @level0name = 'dbo',
    @level1type = N'Table', @level1name = 'Nutrient', 
    @level2type = N'Column', @level2name = 'Subsidiary'
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
";
    }
}