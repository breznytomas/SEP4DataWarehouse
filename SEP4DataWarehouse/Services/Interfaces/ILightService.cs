using SEP4DataWarehouse.Models;

namespace SEP4DataWarehouse.Services.Interfaces;

public interface ILightService
{
    
    Task<ICollection<Light>> GetLightAsync(int boardId);
    Task DeleteLightAsync(int boardId);

    Task AddLightAsync(long id, ICollection<Light> lights );
}