using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using SEP4DataWarehouse.BusinessLogic;
using SEP4DataWarehouse.DTO.DbDTO;
using SEP4DataWarehouse.Models.DbModels;
using Xunit;
using Xunit.Abstractions;

namespace Test.UnitTests;

public class CheckForValuesTest
{
    private readonly ITestOutputHelper testOutputHelper;

    public CheckForValuesTest(ITestOutputHelper testOutputHelper)
    {
        this.testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void CreateHighTriggersTest()
    {
        // prepare inputs
        var reading = LoadReadingFromJson("readingInputHigh.json");


        var co2Event = LoadEventFromJson("co2Event.json");
        var humidityEvent = LoadEventFromJson("humidityEvent.json");
        var lightEvent = LoadEventFromJson("lightEvent.json");
        var temperatureEvent = LoadEventFromJson("temperatureEvent.json");

        // get values

        var co2Triggers = CheckForValues.GetCo2Triggers(reading, co2Event);
        var humidityTriggers = CheckForValues.GetHumidityTriggers(reading, humidityEvent);
        var lightTriggers = CheckForValues.GetLightTriggers(reading, lightEvent);
        var temperatureTriggers = CheckForValues.GetTemperatureTriggers(reading, temperatureEvent);

        // prepare expected output

        var expected = LoadExpectedFromJson("highTrigger.json");
        var expectedJson = JsonSerializer.Serialize(expected);

        // assert

        foreach (var trigger in co2Triggers)
        {
            Assert.Equal(expectedJson, JsonSerializer.Serialize(trigger));
        }

        foreach (var trigger in humidityTriggers)
        {
            Assert.Equal(expectedJson, JsonSerializer.Serialize(trigger));
        }

        foreach (var trigger in lightTriggers)
        {
            Assert.Equal(expectedJson, JsonSerializer.Serialize(trigger));
        }

        foreach (var trigger in temperatureTriggers)
        {
            Assert.Equal(expectedJson, JsonSerializer.Serialize(trigger));
        }
    }

    [Fact]
    public void CreateLowTriggersTest()
    {
        // prepare inputs
        var reading = LoadReadingFromJson("readingInputLow.json");


        var co2Event = LoadEventFromJson("co2Event.json");
        var humidityEvent = LoadEventFromJson("humidityEvent.json");
        var lightEvent = LoadEventFromJson("lightEvent.json");
        var temperatureEvent = LoadEventFromJson("temperatureEvent.json");

        // get values

        var co2Triggers = CheckForValues.GetCo2Triggers(reading, co2Event);
        var humidityTriggers = CheckForValues.GetHumidityTriggers(reading, humidityEvent);
        var lightTriggers = CheckForValues.GetLightTriggers(reading, lightEvent);
        var temperatureTriggers = CheckForValues.GetTemperatureTriggers(reading, temperatureEvent);

        // prepare expected output

        var expected = LoadExpectedFromJson("lowTrigger.json");
        var expectedJson = JsonSerializer.Serialize(expected);

        // assert

        foreach (var trigger in co2Triggers)
        {
            Assert.Equal(expectedJson, JsonSerializer.Serialize(trigger));
        }

        foreach (var trigger in humidityTriggers)
        {
            Assert.Equal(expectedJson, JsonSerializer.Serialize(trigger));
        }

        foreach (var trigger in lightTriggers)
        {
            Assert.Equal(expectedJson, JsonSerializer.Serialize(trigger));
        }

        foreach (var trigger in temperatureTriggers)
        {
            Assert.Equal(expectedJson, JsonSerializer.Serialize(trigger));
        }
    }

    [Fact]
    public void DontCreateTriggersTestMiddle()
    {
        // prepare inputs
        var reading = LoadReadingFromJson("readingInputMiddle.json");


        var co2Event = LoadEventFromJson("co2Event.json");
        var humidityEvent = LoadEventFromJson("humidityEvent.json");
        var lightEvent = LoadEventFromJson("lightEvent.json");
        var temperatureEvent = LoadEventFromJson("temperatureEvent.json");

        // get values

        var co2Triggers = CheckForValues.GetCo2Triggers(reading, co2Event);
        var humidityTriggers = CheckForValues.GetHumidityTriggers(reading, humidityEvent);
        var lightTriggers = CheckForValues.GetLightTriggers(reading, lightEvent);
        var temperatureTriggers = CheckForValues.GetTemperatureTriggers(reading, temperatureEvent);


        // assert

        Assert.Empty(co2Triggers);
        Assert.Empty(humidityTriggers);
        Assert.Empty(lightTriggers);
        Assert.Empty(temperatureTriggers);
    }

    [Fact]
    public void DontCreateTriggersTestTopEdge()
    {
        // prepare inputs
        var reading = LoadReadingFromJson("readingInputTopEdge.json");


        var co2Event = LoadEventFromJson("co2Event.json");
        var humidityEvent = LoadEventFromJson("humidityEvent.json");
        var lightEvent = LoadEventFromJson("lightEvent.json");
        var temperatureEvent = LoadEventFromJson("temperatureEvent.json");

        // get values

        var co2Triggers = CheckForValues.GetCo2Triggers(reading, co2Event);
        var humidityTriggers = CheckForValues.GetHumidityTriggers(reading, humidityEvent);
        var lightTriggers = CheckForValues.GetLightTriggers(reading, lightEvent);
        var temperatureTriggers = CheckForValues.GetTemperatureTriggers(reading, temperatureEvent);


        // assert

        Assert.Empty(co2Triggers);
        Assert.Empty(humidityTriggers);
        Assert.Empty(lightTriggers);
        Assert.Empty(temperatureTriggers);
    }

    [Fact]
    public void DontCreateTriggersTestBottomEdge()
    {
        // prepare inputs
        var reading = LoadReadingFromJson("readingInputBottomEdge.json");


        var co2Event = LoadEventFromJson("co2Event.json");
        var humidityEvent = LoadEventFromJson("humidityEvent.json");
        var lightEvent = LoadEventFromJson("lightEvent.json");
        var temperatureEvent = LoadEventFromJson("temperatureEvent.json");

        // get values

        var co2Triggers = CheckForValues.GetCo2Triggers(reading, co2Event);
        var humidityTriggers = CheckForValues.GetHumidityTriggers(reading, humidityEvent);
        var lightTriggers = CheckForValues.GetLightTriggers(reading, lightEvent);
        var temperatureTriggers = CheckForValues.GetTemperatureTriggers(reading, temperatureEvent);


        // assert

        Assert.Empty(co2Triggers);
        Assert.Empty(humidityTriggers);
        Assert.Empty(lightTriggers);
        Assert.Empty(temperatureTriggers);
    }


    

    [Fact]
    public void Create25PercentDeviation()
    {
        // prepare inputs
        // var reading = LoadReadingFromJson("readingInputHigh.json");
        var reading = new ReadingDto()
        {
            BoardId = "0004A30B00259D2C",
            TemperatureList = new List<Temperature>()
            {
                new Temperature()
                {
                    Id = 0,
                    Timestamp = 1652995127,
                    Value = 62.5F
                }
            }
        };

        var temperatureEvent = new Event()
        {
            Id = 12,
            Name = "TemperatureEvent",
            EventTypes = 0,
            Top = 50,
            Bottom = 10,
            TriggerList = new List<Trigger>()
        };
        
        // get values
        var temperatureTriggers = CheckForValues.GetTemperatureTriggers(reading, temperatureEvent);

        float maxTempValue = 0;
        float tempLimitValue = 0;
        if (temperatureEvent != null)
            foreach (var trigger in temperatureTriggers)
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

        var deviationDegree = CheckForValues.GetDeviationDegree(maxTempValue, tempLimitValue);

        testOutputHelper.WriteLine(deviationDegree.ToString());
        Assert.Equal(100, deviationDegree);
    }

    [Fact]
    public void CreatePlus20Deviation()
    {
        // prepare inputs
        // var reading = LoadReadingFromJson("readingInputHigh.json");
        var reading = new ReadingDto()
        {
            BoardId = "0004A30B00259D2C",
            TemperatureList = new List<Temperature>()
            {
                new Temperature()
                {
                    Id = 0,
                    Timestamp = 1652995127,
                    Value = 60
                }
            }
        };

        var temperatureEvent = new Event()
        {
            Id = 12,
            Name = "TemperatureEvent",
            EventTypes = 0,
            Top = 50,
            Bottom = 10,
            TriggerList = new List<Trigger>()
        };

        // get values
        var temperatureTriggers = CheckForValues.GetTemperatureTriggers(reading, temperatureEvent);


        float maxTempValue = 0;
        float tempLimitValue = 0;
        if (temperatureEvent != null)
            foreach (var trigger in temperatureTriggers)
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

        var deviationDegree = CheckForValues.GetDeviationDegree(maxTempValue, tempLimitValue);

        testOutputHelper.WriteLine(deviationDegree.ToString());
        Assert.Equal(100, deviationDegree);
    }
    
    [Fact]
    public void CreatePlus15Deviation()
    {
        // prepare inputs
        // var reading = LoadReadingFromJson("readingInputHigh.json");
        var reading = new ReadingDto()
        {
            BoardId = "0004A30B00259D2C",
            TemperatureList = new List<Temperature>()
            {
                new Temperature()
                {
                    Id = 0,
                    Timestamp = 1652995127,
                    Value = 57.5F
                }
            }
        };

        var temperatureEvent = new Event()
        {
            Id = 12,
            Name = "TemperatureEvent",
            EventTypes = 0,
            Top = 50,
            Bottom = 10,
            TriggerList = new List<Trigger>()
        };

        // get values
        var temperatureTriggers = CheckForValues.GetTemperatureTriggers(reading, temperatureEvent);


        float maxTempValue = 0;
        float tempLimitValue = 0;
        if (temperatureEvent != null)
            foreach (var trigger in temperatureTriggers)
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

        var deviationDegree = CheckForValues.GetDeviationDegree(maxTempValue, tempLimitValue);

        testOutputHelper.WriteLine(deviationDegree.ToString());
        Assert.Equal(50, deviationDegree);
    }

    [Fact]
    public void CreatePlus10Deviation()
    {
        // prepare inputs
        // var reading = LoadReadingFromJson("readingInputHigh.json");
        var reading = new ReadingDto()
        {
            BoardId = "0004A30B00259D2C",
            TemperatureList = new List<Temperature>()
            {
                new Temperature()
                {
                    Id = 0,
                    Timestamp = 1652995127,
                    Value = 55
                }
            }
        };


        var temperatureEvent = new Event()
        {
            Id = 12,
            Name = "TemperatureEvent",
            EventTypes = 0,
            Top = 50,
            Bottom = 10,
            TriggerList = new List<Trigger>()
        };

        // get values
        var temperatureTriggers = CheckForValues.GetTemperatureTriggers(reading, temperatureEvent);


        float maxTempValue = 0;
        float tempLimitValue = 0;
        if (temperatureEvent != null)
            foreach (var trigger in temperatureTriggers)
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

        var deviationDegree = CheckForValues.GetDeviationDegree(maxTempValue, tempLimitValue);

        testOutputHelper.WriteLine(deviationDegree.ToString());
        Assert.Equal(0, deviationDegree);
    }

    [Fact]
    public void CreatePlus5Deviation()
    {
        // prepare inputs
        // var reading = LoadReadingFromJson("readingInputHigh.json");
        var reading = new ReadingDto()
        {
            BoardId = "0004A30B00259D2C",
            TemperatureList = new List<Temperature>()
            {
                new Temperature()
                {
                    Id = 0,
                    Timestamp = 1652995127,
                    Value = 52.5F
                }
            }
        };


        var temperatureEvent = new Event()
        {
            Id = 12,
            Name = "TemperatureEvent",
            EventTypes = 0,
            Top = 50,
            Bottom = 10,
            TriggerList = new List<Trigger>()
        };

        // get values
        var temperatureTriggers = CheckForValues.GetTemperatureTriggers(reading, temperatureEvent);


        float maxTempValue = 0;
        float tempLimitValue = 0;
        if (temperatureEvent != null)
            foreach (var trigger in temperatureTriggers)
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

        var deviationDegree = CheckForValues.GetDeviationDegree(maxTempValue, tempLimitValue);

        testOutputHelper.WriteLine(deviationDegree.ToString());
        Assert.Equal(-50, deviationDegree);
    }

    [Fact]
    public void CreateJustOverTriggerDeviation()
    {
        // prepare inputs
        // var reading = LoadReadingFromJson("readingInputHigh.json");
        var reading = new ReadingDto()
        {
            BoardId = "0004A30B00259D2C",
            TemperatureList = new List<Temperature>()
            {
                new Temperature()
                {
                    Id = 0,
                    Timestamp = 1652995127,
                    Value = 50.5F
                }
            }
        };


        var temperatureEvent = new Event()
        {
            Id = 12,
            Name = "TemperatureEvent",
            EventTypes = 0,
            Top = 50,
            Bottom = 10,
            TriggerList = new List<Trigger>()
        };

        // get values
        var temperatureTriggers = CheckForValues.GetTemperatureTriggers(reading, temperatureEvent);


        float maxTempValue = 0;
        float tempLimitValue = 0;
        if (temperatureEvent != null)
            foreach (var trigger in temperatureTriggers)
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

        var deviationDegree = CheckForValues.GetDeviationDegree(maxTempValue, tempLimitValue);

        testOutputHelper.WriteLine(deviationDegree.ToString());
        Assert.Equal(-90, deviationDegree);
    }

    [Fact]
    public void DontCreateDeviation()
    {
        // prepare inputs
        // var reading = LoadReadingFromJson("readingInputHigh.json");
        var reading = new ReadingDto()
        {
            BoardId = "0004A30B00259D2C",
            TemperatureList = new List<Temperature>()
            {
                new Temperature()
                {
                    Id = 0,
                    Timestamp = 1652995127,
                    Value = 50
                },
                new Temperature()
                {
                    Id = 0,
                    Timestamp = 1652995127,
                    Value = 1
                }
            }
        };


        var temperatureEvent = new Event()
        {
            Id = 12,
            Name = "TemperatureEvent",
            EventTypes = 0,
            Top = 50,
            Bottom = 10,
            TriggerList = new List<Trigger>()
        };

        // get values
        var temperatureTriggers = CheckForValues.GetTemperatureTriggers(reading, temperatureEvent);


        float maxTempValue = 0;
        float tempLimitValue = 0;
        if (temperatureEvent != null)
            foreach (var trigger in temperatureTriggers)
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

        var deviationDegree = CheckForValues.GetDeviationDegree(maxTempValue, tempLimitValue);

        testOutputHelper.WriteLine(deviationDegree.ToString());
        Assert.Equal(-100, deviationDegree);
    }

    private ReadingDto LoadReadingFromJson(string filename)
    {
        // string path = "../../../InputObjects/";
        string path = "./UnitTests/InputObjects/";

        using StreamReader r = new StreamReader(path + filename);

        string json = r.ReadToEnd();
        var reading = JsonSerializer.Deserialize<ReadingDto>(json);

        testOutputHelper.WriteLine("read from: " + path + filename);
        r.Close();
        return reading;
    }

    private Event LoadEventFromJson(string filename)
    {
        // string path = "../../../InputObjects/";
        string path = "./UnitTests/InputObjects/";

        using StreamReader r = new StreamReader(path + filename);

        string json = r.ReadToEnd();
        var e = JsonSerializer.Deserialize<Event>(json);


        testOutputHelper.WriteLine("read from: " + path + filename);
        r.Close();
        return e;
    }

    private Trigger LoadExpectedFromJson(string filename)
    {
        // string path = "../../../ExpectedOutput/";
        string path = "./UnitTests/ExpectedOutput/";

        using StreamReader r = new StreamReader(path + filename);

        string json = r.ReadToEnd();
        var trigger = JsonSerializer.Deserialize<Trigger>(json);


        testOutputHelper.WriteLine("read from: " + path + filename);
        r.Close();
        return trigger;
    }
}