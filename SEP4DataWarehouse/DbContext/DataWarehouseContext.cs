using System;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace SEP4DataWarehouse.DbContext
{
    public class DataWarehouseContext : Microsoft.EntityFrameworkCore.DbContext
    {
        
        //this just has to be there for postgresql compatibility
  

        public String getDatabaseUrl()
        {
            var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
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
            Console.WriteLine(builder.ToString());
            return builder.ToString();
        }

        //just for testing if it does something
        public DbSet<Reading> Readings { get; set; }

        
        //change in case database changes
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseNpgsql(getDatabaseUrl());


    }
    //just for testing if it does something
    public class Reading
    {
        public int ReadingId { get; set; }
        public int Timestamp { get; set; }
        public int Humidity { get; set; }
    }
}