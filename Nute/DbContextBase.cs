using System;
using System.IO;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Nute.Common;

namespace Nute
{
    public abstract class DbContextBase : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder ob)
        {
            var os = new StdOSDetector().DetectOS();
            IConfigurationBuilder cb = new ConfigurationBuilder();
            cb.SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
                .AddJsonFile("appsettings.json", false, false);
            IConfiguration config = cb.Build();
            string connectionString = string.Empty;
            switch (os)
            {
                case Os.Windows:
                    connectionString
                        = config.GetConnectionString("MikesSurfacePro");
                    break;
                case Os.MacOs:
                    connectionString
                        = config.GetConnectionString("MikesImacMacOs");
                    break;
                case Os.Linux:
                    throw new NotImplementedException("we haven't looked at Linux yet");
                case Os.Any:
                    throw new InvalidOperationException("what's going on - unknown OS?");
            }
            ob.UseSqlServer(connectionString).EnableSensitiveDataLogging();
        }
    }
}