using Microsoft.AspNetCore.Mvc;
using SEP4DataWarehouse.Services.DwServices.Interfaces;
using SEP4DataWarehouse.Utilities;

namespace SEP4DataWarehouse.Controllers.DwControllers; 

public class DimCarbonDioxideController : ControllerBase{
    private readonly IDimCarbonDioxide _dimCarbonDioxideService;
    private readonly IExceptionUtilityService _exceptionUtility;

    public DimCarbonDioxideController(IDimCarbonDioxide dimCarbonDioxideService, IExceptionUtilityService exceptionUtility)
    {
        _dimCarbonDioxideService = dimCarbonDioxideService;
        _exceptionUtility = exceptionUtility;
    }
    
    
    [HttpGet]
    public async Task<ActionResult<float>> GetCdAverage(string boardId, DateTime timeFrom, DateTime timeTo)
    {
        try
        {
            var cdAverageValue = await _dimCarbonDioxideService.GetCDAverage(boardId, timeFrom, timeTo);
            return Ok(cdAverageValue);

        }
        catch (Exception e)
        {
            return _exceptionUtility.HandleException(e);
        }
    }
}