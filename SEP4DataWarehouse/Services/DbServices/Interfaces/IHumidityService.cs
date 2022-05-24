using SEP4DataWarehouse.Models.DbModels;

namespace SEP4DataWarehouse.Services.DbServices;

public interface IHumidityService
{
    Task<ICollection<Humidity>> GetHumidity(string boardId);
    Task DeleteHumidity(string boardId);

    Task AddHumidityAsync(string boardId, ICollection<Humidity> humidities);
}