using SEP4DataWarehouse.Models;

namespace SEP4DataWarehouse.Services.Interfaces;

public interface IEventService
{
    // todo im adding GetEventsByUser/board, CreateEvent
    // in diagrams there was update event with parameters: id & event, i made it just event
    
    Task<ICollection<Event>> GetEventsByBoardId(string boardId);
    
    //Task<IList<Event>> GetEventsByUserId(string email);
    Task AddEventToBoard(EventDto eventDto, string boardId);
    Task UpdateEvent(string boardId, EventDto eventDto);
    Task DeleteEventFromBoard(string boardId, long eventId);
}