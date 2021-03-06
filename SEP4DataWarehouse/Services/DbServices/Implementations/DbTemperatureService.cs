using Microsoft.EntityFrameworkCore;
using SEP4DataWarehouse.Contexts.DbContext;
using SEP4DataWarehouse.Models.DbModels;

namespace SEP4DataWarehouse.Services.DbServices.Implementations;

public class DbTemperatureService : ITemperatureService
{
    private GreenHouseDbContext _context;

    public DbTemperatureService(GreenHouseDbContext context)
    {
        this._context = context;
    }

    public async Task<ICollection<Temperature>> GetTemperatureAsync(string boardId)
    {
        try
        {
            var board = await _context.Boards.Include(b =>b.TemperatureList ).FirstAsync(board => board.Id.Equals(boardId));
            if (board.TemperatureList != null) return board.TemperatureList;
            throw new KeyNotFoundException("Board Id not found, or bord does not have any measurements");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new KeyNotFoundException("Board Id not found, or bord does not have any measurements");
        }
    }
    
    
    
    public async Task AddTemperatureAsync(string boardId, ICollection<Temperature> temperatures)
    {
        var board =  _context.Boards.Include(b => b.TemperatureList).First(b => b.Id.Equals(boardId));
        foreach (var temperature in temperatures)
        {
            board.TemperatureList?.Add(temperature);
        }
        await _context.SaveChangesAsync();
    }

   
    
    public async Task DeleteTemperatureAsync(string boardId)
    {
        try
        {
            var board = await _context.Boards.Include(b =>b.TemperatureList ).FirstAsync(board => board.Id.Equals(boardId));
            var temperatureList = board.TemperatureList;

            if (board.TemperatureList != null)
            {
                if (temperatureList != null) _context.TemperatureSet.RemoveRange(temperatureList);
            }

            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new KeyNotFoundException("Board Id not found");
        }
        
    }

  
}