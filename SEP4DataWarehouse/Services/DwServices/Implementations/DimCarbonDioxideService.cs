using SEP4DataWarehouse.Services.DbServices;
using SEP4DataWarehouse.Services.DwServices.Interfaces;

namespace SEP4DataWarehouse.Services.DwServices.Implementations; 

public class DimCarbonDioxideService : IDimCarbonDioxide {
    public Task<float> GetCDAverage(string boardId, DateTime timeFrom, DateTime timeTo) {
        var from = Int32.Parse(timeFrom.ToString("yyyyMMdd"));
        var to = Int32.Parse(timeTo.ToString("yyyyMMdd"));
        
        
    }
}