using Microsoft.EntityFrameworkCore;
using SEP4DataWarehouse.Contexts.DbContext;
using SEP4DataWarehouse.Models.DbModels;

namespace SEP4DataWarehouse.Services.DbServices.Implementations;

public class DbBoardService : IBoardService
{
    private readonly GreenHouseDbContext _context;


    public DbBoardService(GreenHouseDbContext context)
    {
        _context = context;
    }


    public async Task AttachUserToBoard(string boardId, string userEmail)
    {
        try
        {
            var board =  _context.Boards.Include(b=> b.UserList).First(b => b.Id.Equals(boardId));
            var user = _context.Users.First(u => u.Email.Equals(userEmail));
        
            board.UserList?.Add(user);
        
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new KeyNotFoundException("board or user not found");
        }
        
       
       
    }

    public async Task<ICollection<Board>> GetBoardsByUser(string userEmail)
    {
        try
        {
            var user = _context.Users.Include(u => u.BoardList).FirstOrDefault(u => u.Email.Equals(userEmail));
            return user.BoardList;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new KeyNotFoundException("User does not have any board");
        }
       
    }

    public async Task<Board> AddBoardAsync(Board board)
    {
       var entity = await _context.Boards.AddAsync(board);
       await _context.SaveChangesAsync();
       return entity.Entity;

    }

    public async Task DeleteBoard(string boardId)
    {
        try
        {
            var boardToDelete = _context.Boards.Include(b => b.HumidityList).Include(e=>e.EventList)
                .Include(t=>t.TemperatureList).Include(c=>c.CarbonDioxideList).Include(l=>l.LightLists).Include(u=>u.UserList)
                .First(board => board.Id.Equals(boardId));

            if (boardToDelete.EventList != null)
            {
                foreach (var theEvent in boardToDelete.EventList)
                {
                    if (theEvent.TriggerList!=null)
                    {
                        _context.Triggers.RemoveRange(theEvent.TriggerList);
                    }
               
                }

                _context.Events.RemoveRange(boardToDelete.EventList);
            
            }

            if (boardToDelete.HumidityList != null) _context.HumiditySet.RemoveRange(boardToDelete.HumidityList);
            if (boardToDelete.TemperatureList != null) _context.TemperatureSet.RemoveRange(boardToDelete.TemperatureList);
            if (boardToDelete.CarbonDioxideList != null)
                _context.CarbonDioxideSet.RemoveRange(boardToDelete.CarbonDioxideList);
            if (boardToDelete.LightLists != null) _context.LightSet.RemoveRange(boardToDelete.LightLists);

            _context.Boards.Remove(boardToDelete);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new KeyNotFoundException("Board with the provided id was not found");
        }
        
       
    }

    public async Task DissasociateBoard(string boardId, string userEmail)
    {
        try
        {
            var boardToDissasociate = _context.Boards.Include(u=>u.UserList).First(b => b.Id.Equals(boardId));
            var userToDissasociate = _context.Users.Include(b=>b.BoardList).First(u => u.Email.Equals(userEmail));
            boardToDissasociate.UserList.Remove(userToDissasociate);
            userToDissasociate.BoardList.Remove(boardToDissasociate);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new KeyNotFoundException("the board or the user was not found");
        }
        


    }
}