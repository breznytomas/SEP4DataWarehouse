using Microsoft.AspNetCore.Mvc;
using SEP4DataWarehouse.DTO.DwDTO;
using SEP4DataWarehouse.Services.DwServices.Interfaces;
using SEP4DataWarehouse.Utilities;

namespace SEP4DataWarehouse.Controllers.DwControllers;

[Route("api/[Controller]")]
public class DimLightController : ControllerBase {
    private readonly IDimLight _DimLightService;
    private readonly IExceptionUtilityService _exceptionUtility;

    public DimLightController(IDimLight dimLightService, IExceptionUtilityService exceptionUtility) {
        _DimLightService = dimLightService;
        _exceptionUtility = exceptionUtility;
    }

    [HttpGet("/api/Light/Average")]
    public async Task<ActionResult<float>> GetLightAverage(string boardId, DateTime timeFrom, DateTime timeTo) {
        try {
            var lightAverage = await _DimLightService.GetLightAverage(boardId, timeFrom, timeTo);
            return Ok(lightAverage);
        }
        catch (Exception e) {
            return _exceptionUtility.HandleException(e);
        }
    }
    
    [HttpGet("/api/Light/EventValues")]
    public async Task<ActionResult<List<DimReadingDto>>> GetEventValues(string boardId, DateTime timeFrom,
        DateTime timeTo) {
        try {
            var eventValues = await _DimLightService.GetEvents(boardId, timeFrom, timeTo);
            return Ok(eventValues);
        }
        catch (Exception e) {
            return _exceptionUtility.HandleException(e);
        }
    }
    
    [HttpGet("/api/Light/TriggerRatio")]

    public async Task<ActionResult<float>> GetTriggerRatio(string boardId, DateTime timeFrom, DateTime timeTo) {
        try {
            var ratio = await _DimLightService.GetTriggerRatio(boardId, timeFrom, timeTo);
            return Ok(ratio);
        }
        catch (Exception e) {
            return _exceptionUtility.HandleException(e);
        }
    }
}