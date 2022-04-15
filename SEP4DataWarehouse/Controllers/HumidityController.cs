using Microsoft.AspNetCore.Mvc;
using SEP4DataWarehouse.Models;
using SEP4DataWarehouse.Services;
using SEP4DataWarehouse.Services.Interfaces;
using SEP4DataWarehouse.Utilities;

namespace SEP4DataWarehouse.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HumidityController : ControllerBase
{
    private readonly IHumidityService temperatureService;
    private readonly IExceptionUtilityService exceptionUtility;

    public HumidityController(IHumidityService temperatureService, IExceptionUtilityService exceptionUtility)
    {
        this.temperatureService = temperatureService;
        this.exceptionUtility = exceptionUtility;
    }
    
    [HttpGet]
    public async Task<ActionResult<IList<Humidity>>> GetHumidity()
    {
        try
        {
            IList<Humidity> humidityLogs = await temperatureService.GetHumidityAsync();
            return Ok(humidityLogs);
        }
        catch (Exception e)
        {
            return exceptionUtility.HandleException(e);
        }
    }

 

    [HttpDelete]
    public async Task<ActionResult> DeleteHumidity()
    {
        try
        {
            await temperatureService.DeleteHumidityAsync();
            return Ok();
        }
        catch (Exception e)
        {
            return exceptionUtility.HandleException(e);
        }
    }
}