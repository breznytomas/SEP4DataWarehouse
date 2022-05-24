using SEP4DataWarehouse.Models.DbModels;

namespace SEP4DataWarehouse.Services.DbServices;

public interface ICarbonDioxideService
{
    Task<ICollection<CarbonDioxide>> GetCarbonDioxideAsync(string boardId);
    Task DeleteCarbonDioxideAsync(string boardId);

    Task AddCarboDioxideAsync(string id,ICollection<CarbonDioxide> carbonDioxides);
}