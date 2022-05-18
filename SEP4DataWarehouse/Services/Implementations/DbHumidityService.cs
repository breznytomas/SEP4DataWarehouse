using Microsoft.EntityFrameworkCore;
using SEP4DataWarehouse.DbContext;
using SEP4DataWarehouse.Models;
using SEP4DataWarehouse.Services.Interfaces;

namespace SEP4DataWarehouse.Services.Implementations;

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
            //todo by tomas null pointer possible reference below has to be addressed better 
            if (board.HumidityList != null) return board.HumidityList;
            throw new Exception();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception();
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
        var board = await _context.Boards.Include(b =>b.HumidityList ).FirstAsync(board => board.Id.Equals(boardId));
        board.HumidityList.Clear();
       await _context.SaveChangesAsync();
    }
}