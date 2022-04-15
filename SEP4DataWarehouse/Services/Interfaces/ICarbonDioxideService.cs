using SEP4DataWarehouse.Models;

namespace SEP4DataWarehouse.Services;

public interface ICarbonDioxideService
{
    Task<List<CarbonDioxide>> GetCarbonDioxideAsync();
    Task DeleteCarbonDioxideAsync();
}