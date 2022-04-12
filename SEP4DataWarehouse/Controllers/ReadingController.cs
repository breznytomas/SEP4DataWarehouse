using System;
using Microsoft.AspNetCore.Mvc;
using SEP4DataWarehouse.DbContext;

namespace SEP4DataWarehouse.Controllers
{
    //just for testing
    [ApiController]
    [Route("[controller]")]
    public class ReadingController : ControllerBase
    {
        [HttpGet(Name = "GetReading")]
        public String Get()
        {
            return "API works";
        }

    }
}