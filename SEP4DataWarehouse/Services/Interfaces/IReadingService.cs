using SEP4DataWarehouse.Models;

namespace SEP4DataWarehouse.Services.Interfaces;

public interface IReadingService
{
    Task CreateReadingAsync(Reading reading);
}