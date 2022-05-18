using Microsoft.EntityFrameworkCore;
using SEP4DataWarehouse.DbContext;
using SEP4DataWarehouse.Models;
using SEP4DataWarehouse.Services.Interfaces;

namespace SEP4DataWarehouse.Services.Implementations;

public class DbEventService : IEventService
{
    
    private readonly GreenHouseDbContext _context;

    public DbEventService(GreenHouseDbContext context)
    {
        _context = context;
    }

    public async Task<ICollection<Event>> GetEventsByBoardId(string boardId)
    {

        var board = await _context.Boards.Include(b => b.EventList).ThenInclude(e=> e.TriggerList).FirstAsync(b => b.Id == boardId);
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

    public Task UpdateEvent(string boardId, EventDto eventDto)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteEventFromBoard( string boardId, long eventId)
    {
        var board = await _context.Boards.Include(b => b.EventList).FirstAsync(b => b.Id.Equals(boardId));
        var eventToDelete = board.EventList.First(e => e.Id == eventId);
        board.EventList.Remove(eventToDelete);
    }
    
    
}