namespace SEP4DataWarehouse.Services.DwServices.Interfaces; 

public interface IDimCarbonDioxide {
    Task<float> GetCDAverage(string boardId, DateTime timeFrom, DateTime timeTo);
}