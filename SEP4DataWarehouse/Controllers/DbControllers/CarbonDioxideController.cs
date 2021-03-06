using Microsoft.AspNetCore.Mvc;
using SEP4DataWarehouse.Models.DbModels;
using SEP4DataWarehouse.Services.DbServices;
using SEP4DataWarehouse.Utilities;

namespace SEP4DataWarehouse.Controllers.DbControllers;

[ApiController]
[Route("api/[controller]")]
public class CarbonDioxideController : ControllerBase
{
    private readonly ICarbonDioxideService _carbonDioxideService;
    private readonly IExceptionUtilityService exceptionUtility;

    public CarbonDioxideController(ICarbonDioxideService carbonDioxideService, IExceptionUtilityService exceptionUtility)
    {
        this._carbonDioxideService = carbonDioxideService;
        this.exceptionUtility = exceptionUtility;
    }
    
    [HttpGet]
    public async Task<ActionResult<IList<CarbonDioxide>>> GetCarbonDioxide(string boardId)
    {
        try
        {
           var carbonDioxideLogs = await _carbonDioxideService.GetCarbonDioxideAsync(boardId);
            return Ok(carbonDioxideLogs);
        }
        catch (Exception e)
        {
            return exceptionUtility.HandleException(e);
        }
    }

 

    [HttpDelete]
    public async Task<ActionResult> DeleteCarbonDioxide(string boardId)
    {
        try
        {
            await _carbonDioxideService.DeleteCarbonDioxideAsync(boardId);
            return Ok();
        }
        catch (Exception e)
        {
            return exceptionUtility.HandleException(e);
        }
    }
}