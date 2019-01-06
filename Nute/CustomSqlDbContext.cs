using Microsoft.EntityFrameworkCore;
using Nute.Entities;

namespace Nute
{
    public class CustomSqlDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder ob)
        {
            ob.UseSqlServer(
                "Server=(localdb)\\mssqllocaldb;Database=food;Trusted_Connection=True;MultipleActiveResultSets=True");
/*
            ob.UseSqlServer(
                "Server=localhost,1401;Database=nutrition;User Id=sa;Password=M1cromus");
*/
        }

//        private DbSet<User> Users { get; set; }
    }
}