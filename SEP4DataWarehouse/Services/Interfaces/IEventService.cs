using SEP4DataWarehouse.Models;

namespace SEP4DataWarehouse.Services.Interfaces;

public interface IEventService
{
    // todo im adding GetEventsByUser/board, CreateEvent
    // in diagrams there was update event with parameters: id & event, i made it just event
    
    Task<IList<Event>> GetEvents();
    Task<IList<Event>> GetEventsByBoardId(long boardId);
    Task<IList<Event>> GetEventsByUserId(long userId);
    Task<Event> CreateEvent(Event newEvent);
    Task<Event> UpdateEvent(long id, Event updateEvent);
    Task DeleteEvent(long id);
}