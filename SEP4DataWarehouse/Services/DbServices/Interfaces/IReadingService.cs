using SEP4DataWarehouse.DTO;

namespace SEP4DataWarehouse.Services.DbServices;

public interface IReadingService
{
    Task  AddReading(ReadingDto readingDto);
    
    

}