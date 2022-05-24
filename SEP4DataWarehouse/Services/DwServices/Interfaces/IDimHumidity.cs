namespace SEP4DataWarehouse.Services.DwServices.Interfaces;

public interface IDimHumidity
{
     Task<float> GetHumidityAverage(string boardId, DateTime timeFrom, DateTime timeTo);
}