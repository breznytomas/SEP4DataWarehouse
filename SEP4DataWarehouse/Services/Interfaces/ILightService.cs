using SEP4DataWarehouse.Models;

namespace SEP4DataWarehouse.Services.Interfaces;

public interface ILightService
{
    
    Task<ICollection<Light>> GetLightAsync(string boardId);
    Task DeleteLightAsync(string boardId);

    Task AddLightAsync(string id, ICollection<Light> lights );
}