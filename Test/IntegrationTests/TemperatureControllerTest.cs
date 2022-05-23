using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SEP4DataWarehouse.Controllers;
using SEP4DataWarehouse.Models;
using SEP4DataWarehouse.Services.Interfaces;
using SEP4DataWarehouse.Utilities;
using Xunit;
using Xunit.Abstractions;

namespace Test.IntegrationTests;

public class TemperatureControllerTest
{
    private readonly ITestOutputHelper testOutputHelper;

    public TemperatureControllerTest(ITestOutputHelper testOutputHelper)
    {
        this.testOutputHelper = testOutputHelper;
    }

    [Fact]
    public async Task ReturnTemperatureValues()
    {
        // setup
        var temperatureService = new Mock<ITemperatureService>();
        var exceptionService = new Mock<IExceptionUtilityService>();

        var boardId = "0004A30B00259D2C";

        temperatureService.Setup(repo => repo.GetTemperatureAsync(boardId)).Returns(Multiple());
        
        var controller = new TemperatureController(temperatureService.Object, exceptionService.Object);

        // retrieve data
        var result = await controller.GetTemperature(boardId);
        if (result == null)
            Assert.True(false);

        var resultResult = (OkObjectResult) result.Result!;
        if (resultResult == null)
            Assert.True(false);

        var value = (List<Temperature>) resultResult.Value!;
        if (value == null)
            Assert.True(false);
        
        // assert
        
        foreach (var temps in value)
        {
            testOutputHelper.WriteLine(temps.Value.ToString());
        }
        
        Assert.Equal(3, value.Count);
    }

    private async Task<ICollection<Temperature>> Multiple()
    {
        var tempSet = new List<Temperature>();
        
        tempSet.Add(new Temperature()
        {
            Id = 1,
            Timestamp = 1652977001,
            Value = 50
        });
        tempSet.Add(new Temperature()
        {
            Id = 2,
            Timestamp = 1652977002,
            Value = 60
        });
        tempSet.Add(new Temperature()
        {
            Id = 2,
            Timestamp = 1652977003,
            Value = 70
        });

        return tempSet;
    }
}