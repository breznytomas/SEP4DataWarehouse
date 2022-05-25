using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SEP4DataWarehouse.Contexts.DbContext;
using SEP4DataWarehouse.Contexts.DwContext;
using SEP4DataWarehouse.Models.DwModels;
using SEP4DataWarehouse.Services.DwServices.Interfaces;

namespace SEP4DataWarehouse.Services.DwServices.Implementations;

public class DimHumidityService : IDimHumidity {
    private readonly GreenHouseDwContext _Dwcontext;


    public DimHumidityService(GreenHouseDwContext dwcontext) {
        _Dwcontext = dwcontext;
    }

    public async Task<float> GetHumidityAverage(string boardId, DateTime timeFrom, DateTime timeTo) {

        var from = Int32.Parse(timeFrom.ToString("yyyyMMdd"));
        var to = Int32.Parse(timeTo.ToString("yyyyMMdd"));
       
        
        try {
            var humidity = (from hum in _Dwcontext.Dimhumidities
                    join factHum in _Dwcontext.Factmeasurements
                        on hum.HId equals factHum.HId
                    join dimBoard in _Dwcontext.Dimboards
                        on  factHum.BId equals dimBoard.BId
                    orderby hum.HId
                    select new
                    {
                        hum.HId,
                        dateTime = hum.MeasureDate, 
                        factHum.Humidityvalue,
                        dimBoard.BoardId
                    }
                ).Where(h => h.dateTime >= from && h.dateTime <= to
                ).Where(b => b.BoardId.Equals(boardId)).Average(h=> h.Humidityvalue);
            
            return humidity ?? -999;


            /*var board = await _context.Boards.Include(b =>b.HumidityList ).FirstAsync(board => board.Id.Equals(boardId));gi
            //todo by tomas null pointer possible reference below has to be addressed better (I have made simple if ~ tymon)
            if (board.HumidityList != null) return board.HumidityList;
            throw new Exception();*/
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw new Exception();
        }
    }
}