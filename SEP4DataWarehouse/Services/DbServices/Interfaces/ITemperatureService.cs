using SEP4DataWarehouse.Models.DbModels;

namespace SEP4DataWarehouse.Services.DbServices;

public interface ITemperatureService
{
    Task<ICollection<Temperature>> GetTemperatureAsync(string boardId);
    Task DeleteTemperatureAsync(string boardId);
    
    Task AddTemperatureAsync(string id, ICollection<Temperature> temperatures);
    
}