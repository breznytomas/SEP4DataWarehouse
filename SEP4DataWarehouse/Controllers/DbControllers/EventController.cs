using Microsoft.AspNetCore.Mvc;
using SEP4DataWarehouse.DTO;
using SEP4DataWarehouse.Models.DbModels;
using SEP4DataWarehouse.Services.DbServices;
using SEP4DataWarehouse.Utilities;

namespace SEP4DataWarehouse.Controllers.DbControllers;

[ApiController]
[Route("api/[controller]")]
public class EventController : ControllerBase
{
    private IEventService eventService;
    private IExceptionUtilityService exceptionUtility;

    public EventController(IEventService eventService, IExceptionUtilityService exceptionUtility)
    {
        this.eventService = eventService;
        this.exceptionUtility = exceptionUtility;
    }
    

    [HttpGet]
    public async Task<ActionResult<IList<Event>>> GetEventsByBoardId(string boardId)
    {
        try
        {
            var eventLogs = await eventService.GetEventsByBoardId(boardId);
            return Ok(eventLogs);
        }
        catch (Exception e)
        {
            return exceptionUtility.HandleException(e);
        }
    }
    //Todo by tomas I think this one will be deleted as we have many to many between boards and users, so returing all events from multiple boards, i dont think that is a good idea
    /*[HttpGet]
    [Route("/events/user/{id:long}")]
    public async Task<ActionResult<IList<Event>>> GetEventsByUserId([FromRoute] string boardid)
    {
        try
        {
            IList<Event> eventLogs = await eventService.GetEventsByUserId(boardid);
            return Ok(eventLogs);
        }
        catch (Exception e)
        {
            return exceptionUtility.HandleException(e);
        }
    }*/

    [HttpPost]
    public async Task<ActionResult<Event>> AddEvent([FromBody] EventDto eventDto, string boardId)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            await eventService.AddEventToBoard(eventDto, boardId);

            //todo exception because the class is empty for now
            // return Created($"/{createdEvent.Id}", createdEvent);
            return Ok();

        }
        catch (Exception e)
        {
            return exceptionUtility.HandleException(e);
        }
    }

    [HttpPut]
    public async Task<ActionResult<Event>> UpdateEvent(string boardId, [FromBody] EventDto updateEvent)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            await eventService.UpdateEvent(boardId, updateEvent);
            return Ok();
        }
        catch (Exception e)
        {
            return exceptionUtility.HandleException(e);
        }
    }


    [HttpDelete]
    public async Task<ActionResult> DeleteEvent(string boardId, long eventId)
    {
        try
        {
            await eventService.DeleteEventFromBoard(boardId, eventId);
            return Ok();
        }
        catch (Exception e)
        {
            return exceptionUtility.HandleException(e);
        }
    }
}