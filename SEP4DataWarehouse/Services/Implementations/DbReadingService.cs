using SEP4DataWarehouse.DbContext;
using SEP4DataWarehouse.Models;

namespace SEP4DataWarehouse.Services.Implementations;

public class DbReadingService: IReadingService
{
    
    private readonly GreenHouseDbContext _context;


    public DbReadingService(GreenHouseDbContext context)
    {
        _context = context;
    }


    public async Task AddReading(ReadingDto readingDto)
    {

        var reading = new Reading()
        {
            BoardId = readingDto.BoardId,
            LightLists = readingDto.LightLists,
            HumidityList = readingDto.HumidityList,
            TemperatureList = readingDto.TemperatureList,
            CarbonDioxideList = readingDto.CarbonDioxideList
        };
        
        await _context.Readings.AddAsync(reading);
       await _context.SaveChangesAsync();
    }
}