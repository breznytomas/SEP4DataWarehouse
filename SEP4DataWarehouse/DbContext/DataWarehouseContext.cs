using Microsoft.EntityFrameworkCore;

namespace SEP4DataWarehouse.DbContext
{
    public class DataWarehouseContext : Microsoft.EntityFrameworkCore.DbContext
    {
        
        //this just has to be there for postgresql compatibility
        public DataWarehouseContext(DbContextOptions<DataWarehouseContext> dbContextOptions) : base(dbContextOptions)
        {
            
        }
        

        //just for testing if it does something
        public DbSet<Reading> Readings { get; set; }

        //change in case database changes
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseNpgsql(
            "Host=ec2-63-32-248-14.eu-west-1.compute.amazonaws.com;Database=d5g87javtbe0sb;Username=tvxzufojhnbdmi;Password=a4992f6a91bd7d1f61de47b915e66342528b6a310283b29944c6e91924e335f5;");


    }
    //just for testing if it does something
    public class Reading
    {
        public int ReadingId { get; set; }
        public int Timestamp { get; set; }
        public int Humidity { get; set; }
    }
}