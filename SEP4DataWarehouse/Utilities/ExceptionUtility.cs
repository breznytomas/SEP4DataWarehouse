using System.Data;
using Microsoft.AspNetCore.Mvc;

namespace SEP4DataWarehouse.Utilities;

public class ExceptionUtility : ControllerBase, IExceptionUtilityService
{
/**
 * <returns> Returns a specific ActionResult(including Http error code + message) based on the specific exception </returns>
 * so far there is nothing just basic plain 500 every time(internal server error) + the exception message
 */
    public ActionResult HandleException(Exception e)
    {

        Console.WriteLine(e);
        
        if (e.GetType() == typeof(KeyNotFoundException))
        {
           
            return StatusCode(404, e.Message);
        }

        if (e.GetType() == typeof(ConstraintException))
        {
            return StatusCode(409, e.Message);
        }
      
        
        return StatusCode(500, e.Message);
        
        
    }
}