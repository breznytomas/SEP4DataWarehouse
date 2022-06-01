using System.Text.Json;
using DotLiquid.Tags;
using Microsoft.EntityFrameworkCore;
using SEP4DataWarehouse.Contexts.DbContext;
using SEP4DataWarehouse.DTO.DbDTO;
using SEP4DataWarehouse.Models.DbModels;
using SEP4DataWarehouse.Services;

namespace SEP4DataWarehouse.BusinessLogic;

public class CheckForValues : ICheckForValuesService
{
    private readonly GreenHouseDbContext _context;


    public CheckForValues(GreenHouseDbContext context)
    {
        _context = context;
    }
    

    public async Task<int> CheckForDeviations(ReadingDto reading)
    {
        var board = _context.Boards.Include(b => b.EventList).ThenInclude(e => e.TriggerList)
            .First(b => b.Id.Equals(reading.BoardId));


        Event? humidityEvent = board.EventList.FirstOrDefault(e => e.EventTypes == EventTypes.Humidity);
        Event? temperatureEvent = board.EventList.FirstOrDefault(e => e.EventTypes.Equals(EventTypes.Temperature));
        Event? CO2Event = board.EventList.FirstOrDefault(e => e.EventTypes.Equals(EventTypes.CarbonDioxide));
        Event? lightEvent = board.EventList.FirstOrDefault(e => e.EventTypes.Equals(EventTypes.Light));

        if (humidityEvent != null)
            foreach (var trigger in GetHumidityTriggers(reading, humidityEvent))
            {
                humidityEvent.TriggerList.Add(trigger);
            }

        float maxTempValue = 0;
        float tempLimitValue = 0;
        if (temperatureEvent != null)
            foreach (var trigger in GetTemperatureTriggers(reading, temperatureEvent))
            {
                temperatureEvent.TriggerList.Add(trigger);

                if (trigger.IsTop)
                {
                    if (trigger.TriggerValue > maxTempValue)
                    {
                        maxTempValue = trigger.TriggerValue;
                        tempLimitValue = trigger.LimitValue;
                    }
                }
            }
        

        if (CO2Event != null)
            foreach (var trigger in GetCo2Triggers(reading, CO2Event))
            {
                CO2Event.TriggerList.Add(trigger);
            }
            
        if (lightEvent != null)
            foreach (var trigger in GetLightTriggers(reading, lightEvent))
            {
                lightEvent.TriggerList.Add(trigger);
            }

        await _context.SaveChangesAsync();

        // for sending back how much should the window be open
        return GetDeviationDegree(maxTempValue, tempLimitValue);
    }

    public static int GetDeviationDegree(float maxTempValue, float tempLimitValue)
    {
        // generate a number 0-200
        // 0 for no deviation - window closed
        // 200 for 20% over or more - window fully open
        var deviationPoints = 0; 
        
        if (maxTempValue > 0)
        {
            var tempDeviation = (maxTempValue - tempLimitValue);
            var percentualDeviation = (int) (tempDeviation / tempLimitValue * 100);
            
            
            if (percentualDeviation > 20)
                deviationPoints = 200;
             
            else
                deviationPoints = percentualDeviation * 10;     
             
        }
        
        // get a number within the desired range (0 to 200)
        return deviationPoints;
    }


    public static ICollection<Trigger> GetHumidityTriggers(ReadingDto reading, Event humidityEvent)
    {
        ICollection<Trigger> triggers = new List<Trigger>();

        foreach (var humidity in reading.HumidityList)
        {
            if (humidity.Value > humidityEvent.Top)
            {
                triggers.Add(new Trigger()
                {
                    Timestamp = humidity.Timestamp,
                    IsTop = true,
                    TriggerValue = humidity.Value,
                    LimitValue = humidityEvent.Top
                });
            }

            else if (humidity.Value < humidityEvent.Bottom)
            {
                triggers.Add(new Trigger()
                {
                    Timestamp = humidity.Timestamp,
                    IsTop = false,
                    TriggerValue = humidity.Value,
                    LimitValue = humidityEvent.Bottom
                });
            }
        }

        return triggers;
    }

    public static ICollection<Trigger> GetTemperatureTriggers(ReadingDto reading, Event temperatureEvent)
    {
        ICollection<Trigger> triggers = new List<Trigger>();
        
        foreach (var temperature in reading.TemperatureList)
        {
            if (temperature.Value > temperatureEvent.Top)
            {
                triggers.Add(new Trigger()
                {
                    Timestamp = temperature.Timestamp,
                    IsTop = true,
                    TriggerValue = temperature.Value,
                    LimitValue = temperatureEvent.Top
                });
            }

            else if (temperature.Value < temperatureEvent.Bottom)
            {
                triggers.Add(new Trigger()
                {
                    Timestamp = temperature.Timestamp,
                    IsTop = false,
                    TriggerValue = temperature.Value,
                    LimitValue = temperatureEvent.Bottom
                });
            }
        }

        return triggers;
    }

    public static ICollection<Trigger> GetCo2Triggers(ReadingDto reading, Event co2Event)
    {
        ICollection<Trigger> triggers = new List<Trigger>();
        
        foreach (var co2 in reading.CarbonDioxideList)
        {
            if (co2.Value > co2Event.Top)
            {
                triggers.Add(new Trigger()
                {
                    Timestamp = co2.Timestamp,
                    IsTop = true,
                    TriggerValue = co2.Value,
                    LimitValue = co2Event.Top
                });
            }

            else if (co2.Value < co2Event.Bottom)
            {
                triggers.Add(new Trigger()
                {
                    Timestamp = co2.Timestamp,
                    IsTop = false,
                    TriggerValue = co2.Value,
                    LimitValue = co2Event.Bottom
                });
            }
        }

        return triggers;
    }
    
    public static ICollection<Trigger> GetLightTriggers(ReadingDto reading, Event lightEvent)
    {
        ICollection<Trigger> triggers = new List<Trigger>();
        
        foreach (var light in reading.LightLists)
        {
            if (light.Value > lightEvent.Top)
            {
                triggers.Add(new Trigger()
                {
                    Timestamp = light.Timestamp,
                    IsTop = true,
                    TriggerValue = light.Value,
                    LimitValue = lightEvent.Top
                });
            }

            else if (light.Value < lightEvent.Bottom)
            {
                triggers.Add(new Trigger()
                {
                    Timestamp = light.Timestamp,
                    IsTop = false,
                    TriggerValue = light.Value,
                    LimitValue = lightEvent.Bottom
                });
            }
        }

        return triggers;
    }
}