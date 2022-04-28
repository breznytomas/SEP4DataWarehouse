using SEP4DataWarehouse.Models;

namespace SEP4DataWarehouse.Services.Interfaces;

public interface ITemperatureService
{
    Task<ICollection<Temperature>> GetTemperatureAsync(int boardId);
    Task DeleteTemperatureAsync(int boardId);
    
    Task AddTemperatureAsync(ICollection<Temperature> temperatures);
    
}