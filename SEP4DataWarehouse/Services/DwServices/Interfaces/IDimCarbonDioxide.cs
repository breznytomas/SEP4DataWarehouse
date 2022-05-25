using SEP4DataWarehouse.DTO.DwDTO;
using SEP4DataWarehouse.Models.DwModels;

namespace SEP4DataWarehouse.Services.DwServices.Interfaces; 

public interface IDimCarbonDioxide {
    Task<float> GetCDAverage(string boardId, DateTime timeFrom, DateTime timeTo);
    Task<List<DimReadingDto>> GetEvents(string boardId, DateTime timeFrom, DateTime timeTo);
}