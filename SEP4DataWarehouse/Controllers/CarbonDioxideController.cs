using Microsoft.AspNetCore.Mvc;
using SEP4DataWarehouse.Models;
using SEP4DataWarehouse.Services;
using SEP4DataWarehouse.Utilities;

namespace SEP4DataWarehouse.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CarbonDioxideController : ControllerBase
{
    private readonly ICarbonDioxideService temperatureService;
    private readonly IExceptionUtilityService exceptionUtility;

    public CarbonDioxideController(ICarbonDioxideService temperatureService, IExceptionUtilityService exceptionUtility)
    {
        this.temperatureService = temperatureService;
        this.exceptionUtility = exceptionUtility;
    }
    
    [HttpGet]
    public async Task<ActionResult<IList<CarbonDioxide>>> GetCarbonDioxide()
    {
        try
        {
            IList<CarbonDioxide> carbonDioxideLogs = await temperatureService.GetCarbonDioxideAsync();
            return Ok(carbonDioxideLogs);
        }
        catch (Exception e)
        {
            return exceptionUtility.HandleException(e);
        }
    }

 

    [HttpDelete]
    public async Task<ActionResult> DeleteCarbonDioxide()
    {
        try
        {
            await temperatureService.DeleteCarbonDioxideAsync();
            return Ok();
        }
        catch (Exception e)
        {
            return exceptionUtility.HandleException(e);
        }
    }
}