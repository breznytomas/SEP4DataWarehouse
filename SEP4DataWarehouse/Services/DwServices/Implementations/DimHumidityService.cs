using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SEP4DataWarehouse.Contexts.DbContext;
using SEP4DataWarehouse.Contexts.DwContext;
using SEP4DataWarehouse.DTO.DwDTO;
using SEP4DataWarehouse.Models.DwModels;
using SEP4DataWarehouse.Services.DwServices.Interfaces;

namespace SEP4DataWarehouse.Services.DwServices.Implementations;

public class DimHumidityService : IDimHumidity {
    private readonly GreenHouseDwContext _dwContext;


    public DimHumidityService(GreenHouseDwContext dwContext) {
        _dwContext = dwContext;
    }

    public async Task<float> GetHumidityAverage(string boardId, DateTime timeFrom, DateTime timeTo) {

        var from = Int32.Parse(timeFrom.ToString("yyyyMMdd"));
        var to = Int32.Parse(timeTo.ToString("yyyyMMdd"));
       
        
        try {
            var humidity = (from hum in _dwContext.Dimhumidities
                    join factHum in _dwContext.Factmeasurements
                        on hum.HId equals factHum.HId
                    join dimBoard in _dwContext.Dimboards
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
            
            float? result = humidity.HasValue
                ? (float?)Math.Round(humidity.Value, 3)
                : null;
            
            return result ?? -999;


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
    
    public async Task<List<DimReadingDto>> GetEvents(string boardId, DateTime timeFrom, DateTime timeTo) {
        var from = Int32.Parse(timeFrom.ToString("yyyyMMdd"));
        var to = Int32.Parse(timeTo.ToString("yyyyMMdd"));

        try {
            var humidity = (from hum in _dwContext.Dimhumidities
                    join factMeasure in _dwContext.Factmeasurements
                        on hum.HId equals factMeasure.HId
                    join dimBoard in _dwContext.Dimboards
                        on factMeasure.BId equals dimBoard.BId
                    orderby hum.HId
                    select new
                    {
                        hId = hum.HId,
                        measureDate = hum.MeasureDate,
                        boardId = dimBoard.BoardId,
                        value = factMeasure.Humidityvalue,
                        wasTriggered = hum.Wastriggered,
                        isTop = hum.Istop,
                        upperLimit = hum.Upperlimit, 
                        lowerLimit = hum.Lowerlimit
                    }
                ).Where(hum => hum.measureDate >= from && hum.measureDate <= to
                ).Where(b => b.boardId.Equals(boardId)
                ).Where(hum => hum.wasTriggered.Equals("True")).ToList();

            var result = new List<DimReadingDto>();
            
            if (!humidity.Any()) {
                throw new Exception("No results available for provided search criteria");
            }
            
            foreach (var hum in humidity) {
                result.Add(new DimReadingDto
                {
                    ID = hum.hId,
                    MeasureDate = DateTime.ParseExact(hum.measureDate.ToString(), "yyyyMMdd", null),
                    Value = hum.value ?? -999,
                    TriggeredFrom = hum.isTop.Equals("True") ? "Exceeded top limit" : "Exceeded bottom limit",
                    ExceededBy = hum.isTop.Equals("True") ? (float)(hum.value - hum.upperLimit) : (float)(hum.lowerLimit - hum.value)
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