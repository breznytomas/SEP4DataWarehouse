using Microsoft.EntityFrameworkCore;
using SEP4DataWarehouse.DbContext;
using SEP4DataWarehouse.Models;
using SEP4DataWarehouse.Services.Interfaces;

namespace SEP4DataWarehouse.Services.Implementations;

public class DbHumidityService : IHumidityService
{
    
    private readonly DataWarehouseDbContext _context;

    public DbHumidityService(DataWarehouseDbContext context)
    {
        _context = context;
    }


    public async Task<ICollection<Humidity>> GetHumidity(int boardId)
    {
        try
        {
            var board = await _context.Boards.Include(b =>b.HumidityList ).FirstAsync(board => board.Id == boardId);
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
    
    
    
    public async Task AddHumidityAsync(ICollection<Humidity> humidities)
    {
        throw new NotImplementedException();
    }

   
    
    public async Task DeleteHumidity(int boardId)
    {
        var board = await _context.Boards.Include(b =>b.HumidityList ).FirstAsync(board => board.Id == boardId);
        board.HumidityList.Clear();
       await _context.SaveChangesAsync();
    }
}