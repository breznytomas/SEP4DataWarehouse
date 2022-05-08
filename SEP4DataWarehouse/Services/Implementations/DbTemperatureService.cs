using Microsoft.EntityFrameworkCore;
using SEP4DataWarehouse.DbContext;
using SEP4DataWarehouse.Models;
using SEP4DataWarehouse.Services.Interfaces;

namespace SEP4DataWarehouse.Services.Implementations;

public class DbTemperatureService : ITemperatureService
{
    private DataWarehouseDbContext _context;

    public DbTemperatureService(DataWarehouseDbContext context)
    {
        this._context = context;
    }

    public async Task<ICollection<Temperature>> GetTemperatureAsync(int boardId)
    {
        try
        {
            var board = await _context.Boards.Include(b =>b.TemperatureList ).FirstAsync(board => board.Id == boardId);
            //todo by tomas null pointer possible reference below has to be addressed
            if (board.TemperatureList != null) return board.TemperatureList;
            throw new Exception();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception();
        }
    }
    
    
    
    public async Task AddTemperatureAsync(long id, ICollection<Temperature> temperatures)
    {
        var board =  _context.Boards.Include(b => b.TemperatureList).First(b => b.Id == id);
        foreach (var temperature in temperatures)
        {
            board.TemperatureList.Add(temperature);
        }
        await _context.SaveChangesAsync();
    }

   
    
    public async Task DeleteTemperatureAsync(int boardId)
    {
        var board = await _context.Boards.Include(b =>b.TemperatureList ).FirstAsync(board => board.Id == boardId);
        board.TemperatureList.Clear();
    }

  
}