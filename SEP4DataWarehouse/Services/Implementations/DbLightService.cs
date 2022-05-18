using Microsoft.EntityFrameworkCore;
using SEP4DataWarehouse.DbContext;
using SEP4DataWarehouse.Models;
using SEP4DataWarehouse.Services.Interfaces;

namespace SEP4DataWarehouse.Services.Implementations;

public class DbLightService : ILightService
{
    private readonly GreenHouseDbContext _context;

    public DbLightService(GreenHouseDbContext context)
    {
        _context = context;
    }


    public async Task<ICollection<Light>> GetLightAsync(string boardId)
    {
        try
        {
            var board = await _context.Boards.Include(b =>b.LightLists ).FirstAsync(board => board.Id.Equals(boardId));
            //todo by tomas null pointer possible reference below has to be addressed better
            if (board.LightLists != null) return board.LightLists;
            throw new Exception();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception();
        }
    }

    
    
    public async Task AddLightAsync(string boardId, ICollection<Light> lights)
    {
        var board =  _context.Boards.Include(b => b.LightLists).First(b => b.Id.Equals(boardId));
        foreach (var light in lights)
        {
            board.LightLists.Add(light);
        }
        await _context.SaveChangesAsync();
    }
    
    public async Task DeleteLightAsync(string boardId)
    {
        var board = await _context.Boards.Include(b =>b.LightLists ).FirstAsync(board => board.Id.Equals(boardId));
        board.TemperatureList.Clear();
    }

    
}