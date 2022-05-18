using Microsoft.AspNetCore.Mvc;
using SEP4DataWarehouse.Models;
using SEP4DataWarehouse.Services;
using SEP4DataWarehouse.Services.Interfaces;
using SEP4DataWarehouse.Utilities;

namespace SEP4DataWarehouse.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TemperatureController : ControllerBase
{
    private readonly ITemperatureService temperatureService;
    private readonly IExceptionUtilityService exceptionUtility;

    public TemperatureController(ITemperatureService temperatureService, IExceptionUtilityService exceptionUtility)
    {
        this.temperatureService = temperatureService;
        this.exceptionUtility = exceptionUtility;
    }
    
    [HttpGet]
    public async Task<ActionResult<ICollection<Temperature>>> GetTemperature(string boardId)
    {
        try
        {
            ICollection<Temperature> tempLogs = await temperatureService.GetTemperatureAsync(boardId);
            return Ok(tempLogs);
        }
        catch (Exception e)
        {
            return exceptionUtility.HandleException(e);
        }
    }
    
    [HttpDelete]
    public async Task<ActionResult> DeleteTemperature(string boardId)
    {
        try
        {
            await temperatureService.DeleteTemperatureAsync(boardId);
            
            return Ok();
        }
        catch (Exception e)
        {
            return exceptionUtility.HandleException(e);
        }
    }
    
}