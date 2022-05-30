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


    [HttpGet("/api/CarbonDioxide/Average")]
    public async Task<ActionResult<float>> GetCdAverage(string boardId, DateTime timeFrom, DateTime timeTo) {
        try {
            var cdAverageValue = await _dimCarbonDioxideService.GetCDAverage(boardId, timeFrom, timeTo);
            return Ok(cdAverageValue);
        }
        catch (Exception e) {
            return _exceptionUtility.HandleException(e);
        }
    }

    [HttpGet("/api/CarbonDioxide/EventValues")]
    public async Task<ActionResult<List<DimReadingDto>>> GetEventValues(string boardId, DateTime timeFrom, DateTime timeTo) {
        try {
            var eventValues = await _dimCarbonDioxideService.GetEvents(boardId, timeFrom, timeTo);
            return Ok(eventValues);
        }
        catch (Exception e) {
            return _exceptionUtility.HandleException(e);
        }
    }
    
    [HttpGet("/api/CarbonDioxide/TriggerRatio")]

    public async Task<ActionResult<float>> GetTriggerRatio(string boardId, DateTime timeFrom, DateTime timeTo) {
        try {
            var ratio = await _dimCarbonDioxideService.GetTriggerRatio(boardId, timeFrom, timeTo);
            return Ok(ratio);
        }
        catch (Exception e) {
            return _exceptionUtility.HandleException(e);
        }
    }
}