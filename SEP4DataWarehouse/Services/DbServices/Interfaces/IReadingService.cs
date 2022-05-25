using SEP4DataWarehouse.DTO.DbDTO;

namespace SEP4DataWarehouse.Services.DbServices;

public interface IReadingService
{
    Task  AddReading(ReadingDto readingDto);
    
    

}