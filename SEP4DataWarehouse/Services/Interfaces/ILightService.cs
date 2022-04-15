using SEP4DataWarehouse.Models;

namespace SEP4DataWarehouse.Services.Interfaces;

public interface ILightService
{
    Task<List<Light>> GetLightAsync();
    Task DeleteLightAsync();
}