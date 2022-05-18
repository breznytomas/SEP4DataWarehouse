using SEP4DataWarehouse.Models;

namespace SEP4DataWarehouse.Services.Interfaces;

public interface IHumidityService
{
    Task<ICollection<Humidity>> GetHumidity(string boardId);
    Task DeleteHumidity(string boardId);

    Task AddHumidityAsync(string boardId, ICollection<Humidity> humidities);
}