using SEP4DataWarehouse.Models;

namespace SEP4DataWarehouse.Services;

public interface ICarbonDioxideService
{
    Task<ICollection<CarbonDioxide>> GetCarbonDioxideAsync(string boardId);
    Task DeleteCarbonDioxideAsync(string boardId);

    Task AddCarboDioxideAsync(string id,ICollection<CarbonDioxide> carbonDioxides);
}