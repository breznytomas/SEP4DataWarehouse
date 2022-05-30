using Microsoft.EntityFrameworkCore;
using SEP4DataWarehouse.Contexts.DbContext;
using SEP4DataWarehouse.Models.DbModels;

namespace SEP4DataWarehouse.Services.DbServices.Implementations;

public class DbHumidityService : IHumidityService
{
    
    private readonly GreenHouseDbContext _context;

    public DbHumidityService(GreenHouseDbContext context)
    {
        _context = context;
    }


    public async Task<ICollection<Humidity>> GetHumidity(string boardId)
    {
        try
        {
            var board = await _context.Boards.Include(b =>b.HumidityList ).FirstAsync(board => board.Id.Equals(boardId));
            if (board.HumidityList != null) return board.HumidityList;
            throw new KeyNotFoundException("Board Id not found, or bord does not have any measurements");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new KeyNotFoundException("Board Id not found, or bord does not have any measurements");
        }
    }
    
    
    
    public async Task AddHumidityAsync( string boardId, ICollection<Humidity> humidities)
    {
        var board =  _context.Boards.Include(b => b.HumidityList).First(b => b.Id.Equals(boardId));
        foreach (var humidity in humidities)
        {
            board.HumidityList.Add(humidity);
        }
        await _context.SaveChangesAsync();
    }

   
    
    public async Task DeleteHumidity(string boardId)
    {
        try
        {
            var board = await _context.Boards.Include(b =>b.HumidityList ).FirstAsync(board => board.Id.Equals(boardId));
            var humidityList = board.HumidityList;

            if (board.HumidityList != null)
            {
                if (humidityList != null) _context.HumiditySet.RemoveRange(humidityList);
            }
      
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new KeyNotFoundException("Board id not found");
        }
       
    }
}