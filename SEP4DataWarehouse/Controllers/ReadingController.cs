using Microsoft.AspNetCore.Mvc;
using SEP4DataWarehouse.DbContext;
using SEP4DataWarehouse.Models;
using SEP4DataWarehouse.Services.Interfaces;
using SEP4DataWarehouse.Utilities;

namespace SEP4DataWarehouse.Controllers
{
    //just for testing if infrastructure works correctly, feel free to delete
    [ApiController]
    [Route("api/[controller]")]
    public class ReadingController : ControllerBase
    {
        private readonly IReadingService readingService;
        private readonly IExceptionUtilityService exceptionUtility;

        public ReadingController(IReadingService readingService, IExceptionUtilityService exceptionUtility)
        {
            this.readingService = readingService;
            this.exceptionUtility = exceptionUtility;
        }

        [HttpPost]
        public async Task<ActionResult> CreateReading([FromBody] Reading reading)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await readingService.CreateReadingAsync(reading);
                return Ok();
            }
            catch (Exception e)
            {
                return exceptionUtility.HandleException(e);
            }
        }
        

        // [HttpGet(Name = "GetReading")]
        // public async Task<String> Get()
        // {
        //     using DataWarehouseDbContext dataWarehouseDbContext = new DataWarehouseDbContext();
        //     List<Reading> readings = await dataWarehouseDbContext.Readings.ToListAsync();
        //     return readings.ToString();
        // }
        
    }
}