using Microsoft.EntityFrameworkCore;
using SEP4DataWarehouse.DbContext;
using SEP4DataWarehouse.Models;
using SEP4DataWarehouse.Services.Interfaces;

namespace SEP4DataWarehouse.Services.Implementations;

public class DbTemperatureService : ITemperatureService
{
    private DataWarehouseDbContext context;

    public DbTemperatureService(DataWarehouseDbContext context)
    {
        this.context = context;
    }

    public async Task<List<Temperature>> GetTemperatureAsync()
    {
        return await context.TemperatureSet
            .ToListAsync();
    }

    public async Task DeleteTemperatureAsync()
    {
        //todo implement better delete all?
        context.TemperatureSet.RemoveRange(context.TemperatureSet.ToList());
        await context.SaveChangesAsync();
        
        throw new NotImplementedException();
    }
}