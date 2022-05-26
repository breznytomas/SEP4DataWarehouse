using Microsoft.AspNetCore.Mvc;
using SEP4DataWarehouse.DTO.DwDTO;
using SEP4DataWarehouse.Services.DwServices.Interfaces;
using SEP4DataWarehouse.Utilities;

namespace SEP4DataWarehouse.Controllers.DwControllers;

[Route("api/[controller]")]
public class DimTemperatureController : ControllerBase {
    private readonly IDimTemperature _dimTemperatureService;
    private readonly IExceptionUtilityService _exceptionUtility;


    public DimTemperatureController(IDimTemperature dimTemperatureService, IExceptionUtilityService exceptionUtility) {
        _dimTemperatureService = dimTemperatureService;
        _exceptionUtility = exceptionUtility;
    }

    [HttpGet]
    public async Task<ActionResult<float>> GetTemperatureAverage(string boardId, DateTime timeFrom, DateTime timeTo) {
        try {
            var temperatureAverage = await _dimTemperatureService.GetTemperatureAverage(boardId, timeFrom, timeTo);
            return Ok(temperatureAverage);
        }
        catch (Exception e) {
            return _exceptionUtility.HandleException(e);
        }
    }
    
    [HttpGet("/TemperatureEventValues")]
    public async Task<ActionResult<List<DimReadingDto>>> GetEventValues(string boardId, DateTime timeFrom,
        DateTime timeTo) {
        try {
            var eventValues = await _dimTemperatureService.GetEvents(boardId, timeFrom, timeTo);
            return Ok(eventValues);
        }
        catch (Exception e) {
            return _exceptionUtility.HandleException(e);
        }
    }

    [HttpGet("/TemperatureTriggerRatio")]

    public async Task<ActionResult<float>> GetTriggerRatio(string boardId, DateTime timeFrom, DateTime timeTo) {
        try {
            var ratio = await _dimTemperatureService.GetTriggerRatio(boardId, timeFrom, timeTo);
            return Ok(ratio);
        }
        catch (Exception e) {
            return _exceptionUtility.HandleException(e);
        }
    }

}