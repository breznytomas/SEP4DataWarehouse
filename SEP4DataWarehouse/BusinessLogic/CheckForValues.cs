using Microsoft.EntityFrameworkCore;
using SEP4DataWarehouse.DbContext;
using SEP4DataWarehouse.Models;
using SEP4DataWarehouse.Services;
using SEP4DataWarehouse.Services.Interfaces;

namespace SEP4DataWarehouse.BusinessLogic;

public class CheckForValues
{

    private readonly GreenHouseDbContext _context;


    public CheckForValues(GreenHouseDbContext context)
    {
        _context = context;
    }


    public async Task CheckForDeviations(ReadingDTO reading)
    {
        var board = _context.Boards.Include(b => b.EventList).ThenInclude(e =>e.TriggerList).First(b => b.Id.Equals(reading.BoardId));
        
        

        Event? humidityEvent = board.EventList.FirstOrDefault(e => e.EventTypes == EventTypes.Humidity);
        Event? temperatureEvent = board.EventList.FirstOrDefault(e => e.EventTypes.Equals(EventTypes.Temperature));
        Event? CO2Event = board.EventList.FirstOrDefault(e => e.EventTypes.Equals(EventTypes.CarbonDioxide));
        Event? lightevent = board.EventList.FirstOrDefault(e => e.EventTypes.Equals(EventTypes.Light));

        //for loop for humidity
        if (humidityEvent != null)
        {
            foreach (var humidity in reading.HumidityList)
            {
                if (humidity.Value > humidityEvent.Top)
                {
                    humidityEvent.TriggerList.Add(new Trigger()
                    {
                        Timestamp = humidity.Timestamp,
                        IsTop = true,
                        TriggerValue = humidity.Value,
                        LimitValue = humidityEvent.Top
                    });
                }

                else if (humidity.Value < humidityEvent.Bottom)
                {
                    humidityEvent.TriggerList.Add(new Trigger()
                    {
                        Timestamp = humidity.Timestamp,
                        IsTop = false,
                        TriggerValue = humidity.Value,
                        LimitValue = humidityEvent.Bottom

                    });
                }
            }

        }


        if (temperatureEvent != null)
        {
            foreach (var temperature in reading.TemperatureList)
            {
                if (temperature.Value > temperatureEvent.Top)
                {
                    temperatureEvent.TriggerList.Add(new Trigger()
                    {
                        Timestamp = temperature.Timestamp,
                        IsTop = true,
                        TriggerValue = temperature.Value,
                        LimitValue = temperatureEvent.Top
                    });
                }

                else if (temperature.Value < temperatureEvent.Bottom)
                {
                    temperatureEvent.TriggerList.Add(new Trigger()
                    {
                        Timestamp = temperature.Timestamp,
                        IsTop = false,
                        TriggerValue = temperature.Value,
                        LimitValue = temperatureEvent.Bottom

                    });
                }
            }
        }



        if (CO2Event!=null)
        {
            foreach (var co2 in reading.CarbonDioxideList)
            {
                if (co2.Value > CO2Event.Top)
                {
                    CO2Event.TriggerList.Add(new Trigger()
                    {
                        Timestamp = co2.Timestamp,
                        IsTop = true,
                        TriggerValue = co2.Value,
                        LimitValue = CO2Event.Top
                    });
                }

                else if (co2.Value < CO2Event.Bottom)
                {
                    CO2Event.TriggerList.Add(new Trigger()
                    {
                        Timestamp = co2.Timestamp,
                        IsTop = false,
                        TriggerValue = co2.Value,
                        LimitValue = CO2Event.Bottom

                    });
                }
            }
        }

        if (lightevent!=null)
        {
            foreach (var light in reading.LightLists)
            {
                if (light.Value > lightevent.Top)
                {
                    lightevent.TriggerList.Add(new Trigger()
                    {
                        Timestamp = light.Timestamp,
                        IsTop = true,
                        TriggerValue = light.Value,
                        LimitValue = lightevent.Top
                    });
                }

                else if (light.Value < lightevent.Bottom)
                {
                    lightevent.TriggerList.Add(new Trigger()
                    {
                        Timestamp = light.Timestamp,
                        IsTop = false,
                        TriggerValue = light.Value,
                        LimitValue = lightevent.Bottom

                    });
                }

           
            }
        }
        
        await _context.SaveChangesAsync();
        

    }
}



