using Microsoft.AspNetCore.Mvc;
using SEP4DataWarehouse.Models;
using SEP4DataWarehouse.Services;
using SEP4DataWarehouse.Services.Interfaces;
using SEP4DataWarehouse.Utilities;

namespace SEP4DataWarehouse.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LightController : ControllerBase
{
    private readonly ILightService temperatureService;
    private readonly IExceptionUtilityService exceptionUtility;

    public LightController(ILightService temperatureService, IExceptionUtilityService exceptionUtility)
    {
        this.temperatureService = temperatureService;
        this.exceptionUtility = exceptionUtility;
    }
    
    [HttpGet]
    public async Task<ActionResult<IList<Light>>> GetLight()
    {
        try
        {
            IList<Light> lightLogs = await temperatureService.GetLightAsync();
            return Ok(lightLogs);
        }
        catch (Exception e)
        {
            return exceptionUtility.HandleException(e);
        }
    }

 

    [HttpDelete]
    public async Task<ActionResult> DeleteLight()
    {
        try
        {
            await temperatureService.DeleteLightAsync();
            return Ok();
        }
        catch (Exception e)
        {
            return exceptionUtility.HandleException(e);
        }
    }
}