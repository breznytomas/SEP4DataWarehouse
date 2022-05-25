using SEP4DataWarehouse.Contexts.DwContext;
using SEP4DataWarehouse.Services.DwServices.Interfaces;

namespace SEP4DataWarehouse.Services.DwServices.Implementations; 

public class DimTemperatureService : IDimTemperature {

    private readonly GreenHouseDwContext _dwContext;
    

    public DimTemperatureService(GreenHouseDwContext dwContext) {
        _dwContext = dwContext;
    }
    
    
    public async Task<float> GetTemperatureAverage(string boardId, DateTime timeFrom, DateTime timeTo) {
        var from = Int32.Parse(timeFrom.ToString("yyyyMMdd"));
        var to = Int32.Parse(timeTo.ToString("yyyyMMdd"));
        
        try {
            var temperature = (from temp in _dwContext.Dimtemperatures
                    join factMeasure in _dwContext.Factmeasurements
                        on temp.TId equals factMeasure.TId
                    join dimBoard in _dwContext.Dimboards
                        on  factMeasure.BId equals dimBoard.BId
                    orderby temp.TId
                    select new
                    {
                        temp.TId,
                        measureDate = temp.MeasureDate, 
                        factMeasure.Temperaturevalue,
                        dimBoard.BoardId
                    }
                ).Where(temperature => temperature.measureDate >= from && temperature.measureDate <= to
                ).Where(b => b.BoardId.Equals(boardId)).Average(temp=> temp.Temperaturevalue);
            
            float? result = temperature.HasValue
                ? (float?)Math.Round(temperature.Value, 3)
                : null;
            
            return result ?? -999;

            
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw new Exception();
        }
    }
}