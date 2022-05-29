using SEP4DataWarehouse.DTO.DbDTO;
using SEP4DataWarehouse.Models.DbModels;

namespace SEP4DataWarehouse.Services.DbServices;

public interface IEventService
{
    // todo im adding GetEventsByUser/board, CreateEvent
    // in diagrams there was update event with parameters: id & event, i made it just event
    
    Task<ICollection<Event>> GetEventsByBoardId(string boardId);
    
    //Task<IList<Event>> GetEventsByUserId(string email);
    Task AddEventToBoard(EventDto eventDto, string boardId);
    Task UpdateEvent(string boardId, Event eventReceived);
    Task DeleteEventFromBoard(string boardId, long eventId);
}