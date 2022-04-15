using Microsoft.AspNetCore.Mvc;
using SEP4DataWarehouse.Models;
using SEP4DataWarehouse.Services;
using SEP4DataWarehouse.Services.Interfaces;
using SEP4DataWarehouse.Utilities;

namespace SEP4DataWarehouse.Controllers;

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
    public async Task<ActionResult<IList<Event>>> GetEvents()
    {
        try
        {
            IList<Event> eventLogs = await eventService.GetEvents();
            return Ok(eventLogs);
        }
        catch (Exception e)
        {
            return exceptionUtility.HandleException(e);
        }
    }

    [HttpGet]
    [Route("/events/board/{id:long}")]
    public async Task<ActionResult<IList<Event>>> GetEventsByBoardId([FromRoute] long id)
    {
        try
        {
            IList<Event> eventLogs = await eventService.GetEventsByBoardId(id);
            return Ok(eventLogs);
        }
        catch (Exception e)
        {
            return exceptionUtility.HandleException(e);
        }
    }

    [HttpGet]
    [Route("/events/user/{id:long}")]
    public async Task<ActionResult<IList<Event>>> GetEventsByUserId([FromRoute] long id)
    {
        try
        {
            IList<Event> eventLogs = await eventService.GetEventsByUserId(id);
            return Ok(eventLogs);
        }
        catch (Exception e)
        {
            return exceptionUtility.HandleException(e);
        }
    }

    [HttpPost]
    public async Task<ActionResult<Event>> CreateEvent([FromBody] Event newEvent)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            Event createdEvent = await eventService.CreateEvent(newEvent);

            //todo exception because the class is empty for now
            // return Created($"/{createdEvent.Id}", createdEvent);
            return Created($"/{0}", createdEvent);
            
        }
        catch (Exception e)
        {
            return exceptionUtility.HandleException(e);
        }
    }

    [HttpPut]
    [Route("/events/{id:long}")]
    public async Task<ActionResult<Event>> UpdateEvent([FromRoute] long id, [FromBody] Event updateEvent)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            Event updatedEvent = await eventService.UpdateEvent(id, updateEvent);
            return Ok(updatedEvent);
        }
        catch (Exception e)
        {
            return exceptionUtility.HandleException(e);
        }
    }


    [HttpDelete]
    [Route("/events/{id:long}")]
    public async Task<ActionResult> DeleteEvent([FromRoute] long id)
    {
        try
        {
            await eventService.DeleteEvent(id);
            return Ok();
        }
        catch (Exception e)
        {
            return exceptionUtility.HandleException(e);
        }
    }
}