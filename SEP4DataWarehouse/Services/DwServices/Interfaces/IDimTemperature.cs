using SEP4DataWarehouse.DTO.DwDTO;

namespace SEP4DataWarehouse.Services.DwServices.Interfaces;

public interface IDimTemperature {
    Task<float> GetTemperatureAverage(string boardId, DateTime timeFrom, DateTime timeTo);
    Task<List<DimReadingDto>> GetEvents(string boardId, DateTime timeFrom, DateTime timeTo);
    Task<float> GetTriggerRatio(string boardId, DateTime timeFrom, DateTime timeTo);
}