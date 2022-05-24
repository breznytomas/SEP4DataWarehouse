using Microsoft.AspNetCore.Mvc;
using SEP4DataWarehouse.Contexts.DbContext;
using SEP4DataWarehouse.Contexts.DwContext;
using SEP4DataWarehouse.Models.DwModels;
using SEP4DataWarehouse.Services.DwServices.Interfaces;

namespace SEP4DataWarehouse.Services.DwServices.Implementations;

public class DimHumidityService : IDimHumidity
{
    private readonly GreenHouseDwContext _Dwcontext;


    public DimHumidityService(GreenHouseDwContext dwcontext)
    {
        _Dwcontext = dwcontext;
    }
    
    public async Task<float> GetHumidityAverage(string boardId, DateTime timeFrom, DateTime timeTo)
    {
        try
        {
            /*var humidity = (from hum in _Dwcontext.Dimhumidities
                    join factHum in _Dwcontext.Factmeasurements
                        on hum.HId equals factHum.HId
                    orderby hum.HId
                    select new
                    {
                        hum.HId,
                        dateTime = DateTime.Parse(hum.MeasureDate.ToString()),
                        factHum.Humidityvalue,
                    }
                ).Where(query => query.dateTime > timeFrom && query.dateTime < timeTo).Average(value=> value.Humidityvalue);*/


            var humidity = (from hum in _Dwcontext.Dimhumidities
                    join factHum in _Dwcontext.Factmeasurements
                        on hum.HId equals factHum.HId
                    orderby hum.HId
                    select new
                    {
                        hum.HId,
                        factHum.Humidityvalue,
                    }
                ).Average(average => average.Humidityvalue);

           
           



            //DateTime = DateTime.ParseExact(Convert.ToString(hum.MeasureDate),"YYYYmmdd",null)



            return (float) humidity;




            /*var board = await _context.Boards.Include(b =>b.HumidityList ).FirstAsync(board => board.Id.Equals(boardId));
            //todo by tomas null pointer possible reference below has to be addressed better 
            if (board.HumidityList != null) return board.HumidityList;
            throw new Exception();*/
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception();
        }
    }
}