namespace SEP4DataWarehouse.Services.DwServices.Interfaces; 

public interface IDimTemperature {
    Task<float> GetTemperatureAverage(string boardId, DateTime timeFrom, DateTime timeTo);
}