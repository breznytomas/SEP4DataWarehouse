using SEP4DataWarehouse.Models;

namespace SEP4DataWarehouse.Services.Interfaces;

public interface ITemperatureService
{
    Task<ICollection<Temperature>> GetTemperatureAsync(string boardId);
    Task DeleteTemperatureAsync(string boardId);
    
    Task AddTemperatureAsync(string id, ICollection<Temperature> temperatures);
    
}