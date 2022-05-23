using SEP4DataWarehouse.Models;

namespace SEP4DataWarehouse.Services;

public interface IReadingService
{
    Task  AddReading(ReadingDTO readingDto);
    
    

}