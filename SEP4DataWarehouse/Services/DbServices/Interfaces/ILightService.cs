using SEP4DataWarehouse.Models.DbModels;

namespace SEP4DataWarehouse.Services.DbServices;

public interface ILightService
{
    
    Task<ICollection<Light>> GetLightAsync(string boardId);
    Task DeleteLightAsync(string boardId);

    Task AddLightAsync(string id, ICollection<Light> lights );
}