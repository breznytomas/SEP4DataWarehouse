using Microsoft.AspNetCore.Mvc;
using SEP4DataWarehouse.Models.DbModels;
using SEP4DataWarehouse.Services.DbServices;
using SEP4DataWarehouse.Utilities;

namespace SEP4DataWarehouse.Controllers.DbControllers;

[ApiController]
[Route("api/[controller]")]
public class HumidityController : ControllerBase
{
    private readonly IHumidityService _humidityService;
    private readonly IExceptionUtilityService exceptionUtility;

   

    public HumidityController(IHumidityService humidityService, IExceptionUtilityService exceptionUtility)
    {
        _humidityService = humidityService;
        this.exceptionUtility = exceptionUtility;
    }


    [HttpGet]
    public async Task<ActionResult<IList<Humidity>>> GetHumidity(string boardId)
    {
        try
        {
            var carbonDioxideLogs = await _humidityService.GetHumidity(boardId);
            return Ok(carbonDioxideLogs);
        }
        catch (Exception e)
        {
            return exceptionUtility.HandleException(e);
        }
    }

 

    [HttpDelete]
    public async Task<ActionResult> DeleteHumidityAsync(string boardId)
    {
        try
        {
            await _humidityService.DeleteHumidity(boardId);
            return Ok();
        }
        catch (Exception e)
        {
            return exceptionUtility.HandleException(e);
        }
    }
}