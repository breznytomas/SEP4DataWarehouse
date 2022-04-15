using SEP4DataWarehouse.Models;

namespace SEP4DataWarehouse.Services.Interfaces;

public interface IHumidityService
{
    Task<List<Humidity>> GetHumidityAsync();
    Task DeleteHumidityAsync();
}