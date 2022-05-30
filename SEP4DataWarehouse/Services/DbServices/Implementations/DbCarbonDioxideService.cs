using Microsoft.EntityFrameworkCore;
using SEP4DataWarehouse.Contexts.DbContext;
using SEP4DataWarehouse.Models.DbModels;

namespace SEP4DataWarehouse.Services.DbServices.Implementations;

public class DbCarbonDioxideService: ICarbonDioxideService
{
    
    private readonly GreenHouseDbContext _context;


    public DbCarbonDioxideService(GreenHouseDbContext context)
    {
        _context = context;
    }
    
    
    public async Task<ICollection<CarbonDioxide>> GetCarbonDioxideAsync(string boardId)
    {
        try
        {
            var board = await _context.Boards.Include(b =>b.CarbonDioxideList ).FirstAsync(board => board.Id == boardId);
            if (board.CarbonDioxideList != null) return board.CarbonDioxideList;
            throw new KeyNotFoundException("Board Id not found, or bord does not have any measurements");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new KeyNotFoundException("Board Id not found, or bord does not have any measurements");
        }
    }
    
    
    
    public async Task AddCarboDioxideAsync(string boardId, ICollection<CarbonDioxide> carbonDioxides)
    {
        var board =  _context.Boards.Include(b => b.CarbonDioxideList).First(b => b.Id.Equals(boardId));
        foreach (var carbon in carbonDioxides)
        {
            board.CarbonDioxideList?.Add(carbon);
        }
        await _context.SaveChangesAsync();
    }

   
    
    public async Task DeleteCarbonDioxideAsync(string boardId)
    {
        try
        {
            var board = await _context.Boards.Include(b =>b.CarbonDioxideList ).FirstAsync(board => board.Id == boardId);
            var carbonSet = board.CarbonDioxideList;

            if (board.CarbonDioxideList != null)
            {
                if (carbonSet != null) _context.CarbonDioxideSet.RemoveRange(carbonSet);
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