using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SEP4DataWarehouse.DbContext;
using SEP4DataWarehouse.Models;
using SEP4DataWarehouse.Services.Interfaces;

namespace SEP4DataWarehouse.Services.Implementations;

public class DbCarbonDioxideService: ICarbonDioxideService
{
    
    private readonly DataWarehouseDbContext _context;


    public DbCarbonDioxideService(DataWarehouseDbContext context)
    {
        _context = context;
    }
    
    
    public async Task<ICollection<CarbonDioxide>> GetCarbonDioxideAsync(int boardId)
    {
        try
        {
            var board = await _context.Boards.Include(b =>b.CarbonDioxideList ).FirstAsync(board => board.Id == boardId);
            //todo by tomas null pointer possible reference below has to be addressed
            if (board.CarbonDioxideList != null) return board.CarbonDioxideList;
            throw new Exception();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception();
        }
    }
    
    
    
    public async Task AddCarboDioxideAsync(long id, ICollection<CarbonDioxide> carbonDioxides)
    {
        var board =  _context.Boards.Include(b => b.CarbonDioxideList).First(b => b.Id == id);
        foreach (var carbon in carbonDioxides)
        {
            board.CarbonDioxideList.Add(carbon);
        }
        await _context.SaveChangesAsync();
    }

   
    
    public async Task DeleteCarbonDioxideAsync(int boardId)
    {
        var board = await _context.Boards.Include(b =>b.CarbonDioxideList ).FirstAsync(board => board.Id == boardId);
        board.CarbonDioxideList.Clear();
        await _context.SaveChangesAsync();
    }
    
   
}