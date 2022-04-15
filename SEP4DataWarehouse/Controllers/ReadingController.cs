using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SEP4DataWarehouse.DbContext;

namespace SEP4DataWarehouse.Controllers
{
    //just for testing if infrastructure works correctly, feel free to delete
    [ApiController]
    [Route("[controller]")]
    public class ReadingController : ControllerBase
    {
        [HttpGet(Name = "GetReading")]
        public async Task<String> Get()
        {
            using DataWarehouseContext dataWarehouseContext = new DataWarehouseContext();
            List<Reading> readings = await dataWarehouseContext.Readings.ToListAsync();
            return readings.ToString();
        }

        //just for testing if xUnit works correctly, feel free to delete
        public int AddFunction(int x, int y)
        {
            return x + y;
        }
    }
    

}