using Microsoft.AspNetCore.Mvc;
using SEP4DataWarehouse.DTO.DwDTO;
using SEP4DataWarehouse.Services.DwServices.Interfaces;
using SEP4DataWarehouse.Utilities;

namespace SEP4DataWarehouse.Controllers.DwControllers;

[Route("api/[controller]")]
public class DimCarbonDioxideController : ControllerBase {
    private readonly IDimCarbonDioxide _dimCarbonDioxideService;
    private readonly IExceptionUtilityService _exceptionUtility;

    public DimCarbonDioxideController(IDimCarbonDioxide dimCarbonDioxideService, IExceptionUtilityService exceptionUtility) {
        _dimCarbonDioxideService = dimCarbonDioxideService;
        _exceptionUtility = exceptionUtility;
    }


    [HttpGet]
    public async Task<ActionResult<float>> GetCdAverage(string boardId, DateTime timeFrom, DateTime timeTo) {
        try {
            var cdAverageValue = await _dimCarbonDioxideService.GetCDAverage(boardId, timeFrom, timeTo);
            return Ok(cdAverageValue);
        }
        catch (Exception e) {
            return _exceptionUtility.HandleException(e);
        }
    }

    [HttpGet("/CarbonDioxideEventValues")]
    public async Task<ActionResult<List<DimReadingDto>>> GetEventValues(string boardId, DateTime timeFrom, DateTime timeTo) {
        try {
            var eventValues = await _dimCarbonDioxideService.GetEvents(boardId, timeFrom, timeTo);
            return Ok(eventValues);
        }
        catch (Exception e) {
            return _exceptionUtility.HandleException(e);
        }
    }
}