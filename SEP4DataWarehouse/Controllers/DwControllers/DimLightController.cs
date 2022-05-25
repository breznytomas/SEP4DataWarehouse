using Microsoft.AspNetCore.Mvc;
using SEP4DataWarehouse.Services.DwServices.Interfaces;
using SEP4DataWarehouse.Utilities;

namespace SEP4DataWarehouse.Controllers.DwControllers; 


[Route("api/[Controller]")]

public class DimLightController : ControllerBase {

    private readonly IDimLight _lightService;
    private readonly IExceptionUtilityService _exceptionUtility;

    public DimLightController(IDimLight lightService, IExceptionUtilityService exceptionUtility) {
        _lightService = lightService;
        _exceptionUtility = exceptionUtility;
    }


    [HttpGet]
    public async Task<ActionResult<float>> GetLightAverage(string boardId, DateTime timeFrom, DateTime timeTo)
    {
        try
        {
            var lightAverage = await _lightService.GetLightAverage(boardId, timeFrom, timeTo);
            return Ok(lightAverage);

        }
        catch (Exception e)
        {
            return _exceptionUtility.HandleException(e);
        }
    }
}