using Microsoft.AspNetCore.Mvc;
using SEP4DataWarehouse.BusinessLogic;
using SEP4DataWarehouse.DTO.DbDTO;
using SEP4DataWarehouse.Services.DbServices;
using SEP4DataWarehouse.Utilities;

namespace SEP4DataWarehouse.Controllers.DbControllers;

[ApiController]
[Route("api/[controller]")]
public class ReadingController : ControllerBase
{
    private readonly ICarbonDioxideService _carbonDioxideService;
    private readonly ILightService _lightService;
    private readonly IHumidityService _humidityService;
    private readonly ITemperatureService _temperatureService;
    private readonly ICheckForValuesService _checkForValues;
    private readonly IReadingService _readingService;

    private readonly IExceptionUtilityService exceptionUtility;

    public ReadingController(ICarbonDioxideService carbonDioxideService, ILightService lightService, IHumidityService humidityService, ITemperatureService temperatureService, ICheckForValuesService checkForValues, IReadingService readingService ,IExceptionUtilityService exceptionUtility)
    {
        _carbonDioxideService = carbonDioxideService;
        _lightService = lightService;
        _humidityService = humidityService;
        _temperatureService = temperatureService;
        this.exceptionUtility = exceptionUtility;
        this._checkForValues = checkForValues;
        _readingService = readingService;

    }


    
    [HttpPost]
    public async Task<ActionResult<int>> AddReading([FromBody] ReadingDto readingDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
            
        try
        {
            // for sending back how much should the window be open, depending on if any value went over the trigger limit
            var tempDeviationDegree = await _checkForValues.CheckForDeviations(readingDto);
            await _temperatureService.AddTemperatureAsync(readingDto.BoardId, readingDto.TemperatureList);
            await _lightService.AddLightAsync(readingDto.BoardId, readingDto.LightLists);
            await _humidityService.AddHumidityAsync(readingDto.BoardId, readingDto.HumidityList);
            await _carbonDioxideService.AddCarboDioxideAsync(readingDto.BoardId, readingDto.CarbonDioxideList);
            await _readingService.AddReading(readingDto);
            return Ok(tempDeviationDegree);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return exceptionUtility.HandleException(e);
        }
    }

}
