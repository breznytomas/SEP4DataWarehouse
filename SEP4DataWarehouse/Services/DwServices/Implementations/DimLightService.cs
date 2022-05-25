using SEP4DataWarehouse.Contexts.DwContext;
using SEP4DataWarehouse.DTO.DwDTO;
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
    
    public async Task<List<DimReadingDto>> GetEvents(string boardId, DateTime timeFrom, DateTime timeTo) {
        var from = Int32.Parse(timeFrom.ToString("yyyyMMdd"));
        var to = Int32.Parse(timeTo.ToString("yyyyMMdd"));

        try {
            var light = (from li in _dwContext.Dimlights
                    join factMeasure in _dwContext.Factmeasurements
                        on li.LId equals factMeasure.CdId
                    join dimBoard in _dwContext.Dimboards
                        on factMeasure.BId equals dimBoard.BId
                    orderby li.LId
                    select new
                    {
                        liId = li.LId,
                        measureDate = li.MeasureDate,
                        boardId = dimBoard.BoardId,
                        value = factMeasure.Lightvalue,
                        wasTriggered = li.Wastriggered,
                        isTop = li.Istop,
                        upperLimit = li.Upperlimit, 
                        lowerLimit = li.Lowerlimit
                    }
                ).Where(li => li.measureDate >= from && li.measureDate <= to
                ).Where(b => b.boardId.Equals(boardId)
                ).Where(li => li.wasTriggered.Equals("True")).ToList();

            var result = new List<DimReadingDto>();
            
            if (!light.Any()) {
                throw new Exception("No results available for provided search criteria");
            }
            
            foreach (var li in light) {
                result.Add(new DimReadingDto
                {
                    ID = li.liId,
                    MeasureDate = DateTime.ParseExact(li.measureDate.ToString(), "yyyyMMdd", null),
                    Value = li.value ?? -999,
                    TriggeredFrom = li.isTop.Equals("True") ? "Exceeded top limit" : "Exceeded bottom limit",
                    ExceededBy = li.isTop.Equals("True") ? (float)(li.value - li.upperLimit) : (float)(li.lowerLimit - li.value)
                });
            }

            return result;

        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw new Exception();
        }
    }
    
}