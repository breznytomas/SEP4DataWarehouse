using Microsoft.AspNetCore.Mvc;
using SEP4DataWarehouse.Models.DbModels;
using SEP4DataWarehouse.Services.DbServices;
using SEP4DataWarehouse.Utilities;

namespace SEP4DataWarehouse.Controllers.DbControllers;

[ApiController]
[Route("api/[controller]")]
public class LightController : ControllerBase
{
    private readonly ILightService _lightService;
    private readonly IExceptionUtilityService exceptionUtility;

    public LightController(ILightService lightService, IExceptionUtilityService exceptionUtility)
    {
        this._lightService = lightService;
        this.exceptionUtility = exceptionUtility;
    }
    
    [HttpGet]
    public async Task<ActionResult<IList<Light>>> GetLight(string boardId)
    {
        try
        {
            ICollection<Light> lightLogs = await _lightService.GetLightAsync(boardId);
            return Ok(lightLogs);
        }
        catch (Exception e)
        {
            return exceptionUtility.HandleException(e);
        }
    }

 

    [HttpDelete]
    public async Task<ActionResult> DeleteLight(string boardId)
    {
        try
        {
            await _lightService.DeleteLightAsync(boardId);
            return Ok();
        }
        catch (Exception e)
        {
            return exceptionUtility.HandleException(e);
        }
    }
}