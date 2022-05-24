using Microsoft.EntityFrameworkCore;
using Npgsql;
using SEP4DataWarehouse.Models.DbModels;

namespace SEP4DataWarehouse.Contexts.DbContext
{
    public class GreenHouseDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<Temperature> TemperatureSet { get; set; }
        public DbSet<Light> LightSet { get; set; }
        public DbSet<CarbonDioxide> CarbonDioxideSet { get; set; }
        public DbSet<Humidity> HumiditySet { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Trigger> Triggers { get; set; }
        public DbSet<Board> Boards { get; set; }
        public DbSet<Reading> Readings { get; set; }
       
       



        //connects the program to postgres, just don't touch this
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseNpgsql(getDatabaseUrl());
        
        // takes the database connection string provided by heroku and uses string manipulation to create and return the connection string in format suitable for C#
        // if in debug build configuration it takes the string from a file. Remember to have this file here and to set your build config to 'debug' (next to the 'Run' button) when running the app locally 
        // if in release configuration it takes the string from environment variable that is provided by heroku to the running docker container
        // don't touch this method also. if something appears to be broken maybe the connection string changed due to maintenance, in that case let me know -oliver
        private String getDatabaseUrl()
        {
            string? databaseUrl;
            #if (DEBUG)
            databaseUrl = System.IO.File.ReadAllText("./Contexts/DbContext/DbString.txt");
            #else
            // for heroku
            databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
            #endif
            var databaseUri = new Uri(databaseUrl);
            var userInfo = databaseUri.UserInfo.Split(':');

            var builder = new NpgsqlConnectionStringBuilder
            {
                Host = databaseUri.Host,
                Port = databaseUri.Port,
                Username = userInfo[0],
                Password = userInfo[1],
                Database = databaseUri.LocalPath.TrimStart('/')
            };
            return builder.ToString();

            
        }

    }
}