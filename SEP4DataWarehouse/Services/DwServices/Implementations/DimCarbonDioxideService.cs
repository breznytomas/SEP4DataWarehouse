using System.Text.Json;
using Microsoft.VisualBasic;
using SEP4DataWarehouse.Contexts.DwContext;
using SEP4DataWarehouse.DTO.DwDTO;
using SEP4DataWarehouse.Models.DwModels;
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
                        on factMeasure.BId equals dimBoard.BId
                    orderby cd.CdId
                    select new
                    {
                        cd.CdId,
                        measureDate = cd.MeasureDate,
                        factMeasure.Carbondioxidevalue,
                        dimBoard.BoardId
                    }
                ).Where(cd => cd.measureDate >= from && cd.measureDate <= to
                ).Where(b => b.BoardId.Equals(boardId)).Average(cd => cd.Carbondioxidevalue);


            float? result = carbonDioxide.HasValue
                ? (float?)Math.Round(carbonDioxide.Value, 3)
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
            var carbonDioxide = (from cd in _dwContext.Dimcarbondioxides
                    join factMeasure in _dwContext.Factmeasurements
                        on cd.CdId equals factMeasure.CdId
                    join dimBoard in _dwContext.Dimboards
                        on factMeasure.BId equals dimBoard.BId
                    orderby cd.CdId
                    select new
                    {
                        cdId = cd.CdId,
                        measureDate = cd.MeasureDate,
                        boardId = dimBoard.BoardId,
                        value = factMeasure.Carbondioxidevalue,
                        wasTriggered = cd.Wastriggered,
                        isTop = cd.Istop,
                        upperLimit = cd.Upperlimit, 
                        lowerLimit = cd.Lowerlimit
                    }
                ).Where(cd => cd.measureDate >= from && cd.measureDate <= to
                ).Where(b => b.boardId.Equals(boardId)
                ).Where(cd => cd.wasTriggered.Equals("True")).ToList();

            var result = new List<DimReadingDto>();
            
            if (!carbonDioxide.Any()) {
                throw new Exception("No results available for provided search criteria");
            }
            
            foreach (var cd in carbonDioxide) {
                result.Add(new DimReadingDto
                {
                    ID = cd.cdId,
                    MeasureDate = DateTime.ParseExact(cd.measureDate.ToString(), "yyyyMMdd", null),
                    Value = cd.value ?? -999,
                    TriggeredFrom = cd.isTop.Equals("True") ? "Exceeded top limit" : "Exceeded bottom limit",
                    ExceededBy = cd.isTop.Equals("True") ? (float)(cd.value - cd.upperLimit) : (float)(cd.lowerLimit - cd.value)
                });
            }

            return result;

        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw new Exception();
        }
    }

    public async Task<float> GetTriggerRatio(string boardId, DateTime timeFrom, DateTime timeTo) {
    var from = Int32.Parse(timeFrom.ToString("yyyyMMdd"));
        var to = Int32.Parse(timeTo.ToString("yyyyMMdd"));

        try {
            var carbonDioxideTriggered = (from cd in _dwContext.Dimcarbondioxides
                    join factMeasure in _dwContext.Factmeasurements
                        on cd.CdId equals factMeasure.CdId
                    join dimBoard in _dwContext.Dimboards
                        on factMeasure.BId equals dimBoard.BId
                    orderby cd.CdId
                    select new
                    {
                        cdId = cd.CdId,
                        measureDate = cd.MeasureDate,
                        boardId = dimBoard.BoardId,
                        value = factMeasure.Carbondioxidevalue,
                        wasTriggered = cd.Wastriggered
                    }
                ).Where(cd => cd.measureDate >= from && cd.measureDate <= to
                ).Where(b => b.boardId.Equals(boardId)
                ).Count(cd => cd.wasTriggered.Equals("True"));
            
            var carbonDioxideTotal = (from cd in _dwContext.Dimcarbondioxides
                    join factMeasure in _dwContext.Factmeasurements
                        on cd.CdId equals factMeasure.CdId
                    join dimBoard in _dwContext.Dimboards
                        on factMeasure.BId equals dimBoard.BId
                    orderby cd.CdId
                    select new
                    {
                        cdId = cd.CdId,
                        measureDate = cd.MeasureDate,
                        boardId = dimBoard.BoardId,
                        value = factMeasure.Carbondioxidevalue,
                        wasTriggered = cd.Wastriggered
                    }
                ).Where(cd => cd.measureDate >= from && cd.measureDate <= to
                ).Where(b => b.boardId.Equals(boardId)
                ).Count(cd => cd.wasTriggered.Equals("True") || cd.wasTriggered.Equals("False"));

            var result = (float)carbonDioxideTriggered / carbonDioxideTotal * 100;
          

            return result;

        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw new Exception();
        }    }
}