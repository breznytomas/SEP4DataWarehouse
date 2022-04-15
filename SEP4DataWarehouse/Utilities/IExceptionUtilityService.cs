using Microsoft.AspNetCore.Mvc;

namespace SEP4DataWarehouse.Utilities;

public interface IExceptionUtilityService
{
    ActionResult HandleException(Exception e);
}