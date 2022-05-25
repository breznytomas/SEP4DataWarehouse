using SEP4DataWarehouse.Contexts.DwContext;
using SEP4DataWarehouse.Services.DbServices;
using SEP4DataWarehouse.Services.DwServices.Interfaces;

namespace SEP4DataWarehouse.Services.DwServices.Implementations; 

public class DimCarbonDioxideService : IDimCarbonDioxide {

    private readonly GreenHouseDwContext _dwContext;

    public DimCarbonDioxideService(GreenHouseDwContext dwContext) {
        _dwContext = dwContext;
    }
    
    public async Task<float> GetCDAverage(string boardId, DateTime timeFrom, DateTime timeTo) {
        var from = Int32.Parse(timeFrom.ToString("yyyyMMdd"));
        var to = Int32.Parse(timeTo.ToString("yyyyMMdd"));
        
        try {
            var carbonDioxide = (from cd in _dwContext.Dimcarbondioxides
                    join factMeasure in _dwContext.Factmeasurements
                        on cd.CdId equals factMeasure.CdId
                    join dimBoard in _dwContext.Dimboards
                        on  factMeasure.BId equals dimBoard.BId
                    orderby cd.CdId
                    select new
                    {
                        cd.CdId,
                        measureDate = cd.MeasureDate, 
                        factMeasure.Carbondioxidevalue,
                        dimBoard.BoardId
                    }
                ).Where(cd => cd.measureDate >= from && cd.measureDate <= to
                ).Where(b => b.BoardId.Equals(boardId)).Average(cd=> cd.Carbondioxidevalue);
            
            return carbonDioxide ?? -999;

            
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw new Exception();
        }
    }
}