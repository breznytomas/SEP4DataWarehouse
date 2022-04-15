using System;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace SEP4DataWarehouse.DbContext
{
    public class DataWarehouseContext : Microsoft.EntityFrameworkCore.DbContext
    {
        //connects the program to postgres, just don't touch this
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseNpgsql(getDatabaseUrl());
        
        // takes the database connection string provided by heroku and uses string manipulation to create and return the connection string in format suitable for C#
        // if in debug build configuration it takes the string from a file. Remember to have this file here and to set your build config to 'debug' (next to the 'Run' button) when running the app locally 
        // if in release configuration it takes the string from environment variable that is provided by heroku to the running docker container
        // don't touch this method also. if something appears to be broken maybe the connection string changed due to maintenance, in that case let me know -oliver
        public String getDatabaseUrl()
        {
            string? databaseUrl;
            #if (DEBUG)
            databaseUrl = System.IO.File.ReadAllText("./DbContext/DbString.txt");
            #else
            //for heroku
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
        
       

        //just for testing if infrastructure works, feel free to delete
        public DbSet<Reading> Readings { get; set; }
        
    }
    //just for testing if infrastructure works, feel free to delete
    public class Reading
    {
        public int ReadingId { get; set; }
        public int Timestamp { get; set; }
        public int Humidity { get; set; }
    }
}