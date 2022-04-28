using SEP4DataWarehouse.Models;

namespace SEP4DataWarehouse.Services.Interfaces;

public interface IHumidityService
{
    Task<ICollection<Humidity>> GetHumidity(int boardId);
    Task DeleteHumidity(int boardId);

    Task AddHumidityAsync(ICollection<Humidity> humidities);
}