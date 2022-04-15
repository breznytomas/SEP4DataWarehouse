using SEP4DataWarehouse.Models;

namespace SEP4DataWarehouse.Services.Interfaces;

public interface ITemperatureService
{
    Task<List<Temperature>> GetTemperatureAsync();
    Task DeleteTemperatureAsync();
}