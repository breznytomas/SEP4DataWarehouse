using SEP4DataWarehouse.Models;

namespace SEP4DataWarehouse.Services;

public interface ICarbonDioxideService
{
    Task<ICollection<CarbonDioxide>> GetCarbonDioxideAsync(int boardId);
    Task DeleteCarbonDioxideAsync(int boardId);

    Task AddCarboDioxideAsync(long id,ICollection<CarbonDioxide> carbonDioxides);
}