using Microsoft.AspNetCore.Mvc;
using SEP4DataWarehouse.BusinessLogic;
using SEP4DataWarehouse.Models;
using SEP4DataWarehouse.Services;
using SEP4DataWarehouse.Services.Interfaces;
using SEP4DataWarehouse.Utilities;

namespace SEP4DataWarehouse.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReadingController : ControllerBase
{
    private readonly ICarbonDioxideService _carbonDioxideService;
    private readonly ILightService _lightService;
    private readonly IHumidityService _humidityService;
    private readonly ITemperatureService _temperatureService;
    private readonly CheckForValues _checkForValues;

    private readonly IExceptionUtilityService exceptionUtility;

    public ReadingController(ICarbonDioxideService carbonDioxideService, ILightService lightService, IHumidityService humidityService, ITemperatureService temperatureService, CheckForValues checkForValues ,IExceptionUtilityService exceptionUtility)
    {
        _carbonDioxideService = carbonDioxideService;
        _lightService = lightService;
        _humidityService = humidityService;
        _temperatureService = temperatureService;
        this.exceptionUtility = exceptionUtility;
        this._checkForValues = checkForValues;
    }


    
    [HttpPost]
    public async Task<ActionResult<BoardDTO>> AddReading([FromBody] ReadingDTO readingDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
            
        try
        {
            await _checkForValues.CheckForDeviations(readingDto);
            //TODO by tomas this is the version for skeleton, insert call to logic here to verity if measurements are out of bounds
            await _temperatureService.AddTemperatureAsync(readingDto.BoardId, readingDto.TemperatureList);
            await _lightService.AddLightAsync(readingDto.BoardId, readingDto.LightLists);
            await _humidityService.AddHumidityAsync(readingDto.BoardId, readingDto.HumidityList);
            await _carbonDioxideService.AddCarboDioxideAsync(readingDto.BoardId, readingDto.CarbonDioxideList);
            return Ok("Added successfully");


        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return exceptionUtility.HandleException(e);
        }
    }

}
