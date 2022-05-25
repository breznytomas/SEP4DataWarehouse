using SEP4DataWarehouse.Contexts.DwContext;
using SEP4DataWarehouse.Services.DwServices.Interfaces;
using SEP4DataWarehouse.Utilities;

namespace SEP4DataWarehouse.Services.DwServices.Implementations; 

public class DimLightService : IDimLight {

    private readonly GreenHouseDwContext _dwContext;
    private readonly IExceptionUtilityService _exceptionUtility;

    public DimLightService(GreenHouseDwContext dwContext, IExceptionUtilityService exceptionUtility) {
        _dwContext = dwContext;
        _exceptionUtility = exceptionUtility;
    }

    public async Task<float> GetLightAverage(string boardId, DateTime timeFrom, DateTime timeTo) {
        var from = Int32.Parse(timeFrom.ToString("yyyyMMdd"));
        var to = Int32.Parse(timeTo.ToString("yyyyMMdd"));
        
        try {
            var light = (from li in _dwContext.Dimlights
                    join factMeasure in _dwContext.Factmeasurements
                        on li.LId equals factMeasure.LId
                    join dimBoard in _dwContext.Dimboards
                        on  factMeasure.BId equals dimBoard.BId
                    orderby li.LId
                    select new
                    {
                        li.LId,
                        measureDate = li.MeasureDate, 
                        factMeasure.Lightvalue,
                        dimBoard.BoardId
                    }
                ).Where(li => li.measureDate >= from && li.measureDate <= to
                ).Where(b => b.BoardId.Equals(boardId)).Average(li=> li.Lightvalue);
            
            float? result = light.HasValue
                ? (float?)Math.Round(light.Value, 3)
                : null;
            
            return result ?? -999;

            
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw new Exception();
        }
    }
}