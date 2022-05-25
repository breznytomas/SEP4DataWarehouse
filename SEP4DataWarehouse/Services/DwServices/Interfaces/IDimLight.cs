namespace SEP4DataWarehouse.Services.DwServices.Interfaces; 

public interface IDimLight {
    Task<float> GetLightAverage(string boardId, DateTime timeFrom, DateTime timeTo);

}