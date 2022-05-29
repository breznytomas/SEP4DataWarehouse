using Microsoft.AspNetCore.Mvc;
using SEP4DataWarehouse.DTO.DwDTO;
using SEP4DataWarehouse.Services.DwServices.Interfaces;
using SEP4DataWarehouse.Utilities;

namespace SEP4DataWarehouse.Controllers.DwControllers;

[ApiController]
[Route("api/[controller]")]
public class DimHumidityController : ControllerBase {
    private readonly IDimHumidity _dimHumidityService;
    private readonly IExceptionUtilityService _exceptionUtility;

    public DimHumidityController(IDimHumidity dimHumidityService, IExceptionUtilityService exceptionUtility) {
        _dimHumidityService = dimHumidityService;
        this._exceptionUtility = exceptionUtility;
    }

    [HttpGet("/api/Humidity/Dim")]
    public async Task<ActionResult<float>> GetHumidityAverage(string boardId, DateTime timeFrom, DateTime timeTo) {
        try {
            var humAverageValue = await _dimHumidityService.GetHumidityAverage(boardId, timeFrom, timeTo);
            return Ok(humAverageValue);
        }
        catch (Exception e) {
            return _exceptionUtility.HandleException(e);
        }
    }

  

    [HttpGet("/api/Humidity/EventValues")]
    public async Task<ActionResult<List<DimReadingDto>>> GetEventValues(string boardId, DateTime timeFrom,
        DateTime timeTo) {
        try {
            var eventValues = await _dimHumidityService.GetEvents(boardId, timeFrom, timeTo);
            return Ok(eventValues);
        }
        catch (Exception e) {
            return _exceptionUtility.HandleException(e);
        }
    }
    
    [HttpGet("/api/Humidity/TriggerRatio")]

    public async Task<ActionResult<float>> GetTriggerRatio(string boardId, DateTime timeFrom, DateTime timeTo) {
        try {
            var ratio = await _dimHumidityService.GetTriggerRatio(boardId, timeFrom, timeTo);
            return Ok(ratio);
        }
        catch (Exception e) {
            return _exceptionUtility.HandleException(e);
        }
    }
}