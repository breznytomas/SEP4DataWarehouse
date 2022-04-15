using SEP4DataWarehouse.DbContext;
using SEP4DataWarehouse.Models;
using SEP4DataWarehouse.Services.Interfaces;
namespace SEP4DataWarehouse.Services.Implementations;

public class DbReadingService : IReadingService
{
    private DataWarehouseDbContext context;

    public DbReadingService(DataWarehouseDbContext context)
    {
        this.context = context;
    }

    public async Task CreateReadingAsync(Reading reading)
    {
        // split declaration because of 
        var addTemperatureAsync = AddTemperatureAsync(reading.TemperatureList);
        // todo call methods to add other variables
        
        //todo call check against breakpoint
        
        
        await addTemperatureAsync;
        
    }

    private async Task AddTemperatureAsync(List<Temperature> list)
    {
        await context.TemperatureSet.AddRangeAsync(list);
        await context.SaveChangesAsync();
    }
}