using Microsoft.EntityFrameworkCore;
using SEP4DataWarehouse.Contexts.DbContext;
using SEP4DataWarehouse.DTO.DbDTO;
using SEP4DataWarehouse.Models.DbModels;

namespace SEP4DataWarehouse.Services.DbServices.Implementations;

public class DbEventService : IEventService
{
    
    private readonly GreenHouseDbContext _context;

    public DbEventService(GreenHouseDbContext context)
    {
        _context = context;
    }

    public async Task<ICollection<Event>> GetEventsByBoardId(string boardId)
    {

        var board = await _context.Boards
            .Include(b => b.EventList)
            .ThenInclude(e=> e.TriggerList)
            .FirstAsync(b => b.Id == boardId);
        return board.EventList;
        
    }

    public async Task AddEventToBoard(EventDto eventDto, string boardId)
    {
        var board = await _context.Boards.Include(b => b.EventList).FirstAsync(b => b.Id.Equals(boardId));

        Event newEvent = new Event()
        {
            Name = eventDto.Name,
            Bottom = eventDto.Bottom,
            Top = eventDto.Top,
            EventTypes = eventDto.EventTypes
            
        };

        board.EventList.Add(newEvent);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateEvent(string boardId, Event eventReceived)
    {
       // TODO by tomas finish this
       var board = _context.Boards.Include(b => b.EventList).First(board => board.Id.Equals(boardId));
       var eventFromDb = board.EventList.First(e => e.Id == eventReceived.Id);

       eventFromDb.Bottom = eventReceived.Bottom;
       eventFromDb.Top = eventReceived.Top;
       eventFromDb.Name = eventReceived.Name;

       await _context.SaveChangesAsync();


    }

    public async Task DeleteEventFromBoard( string boardId, long eventId)
    {
        var board = await _context.Boards.Include(b => b.EventList).FirstAsync(b => b.Id.Equals(boardId));

       
        
            var event1 = board.EventList.First(e => e.Id == eventId);
        

        if (board.EventList != null)
        {
            _context.Events.Remove(event1);
        }
        
        await _context.SaveChangesAsync();
    }
    
    
}