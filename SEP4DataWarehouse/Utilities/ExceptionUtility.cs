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
        return StatusCode(500, e.Message);
    }
}