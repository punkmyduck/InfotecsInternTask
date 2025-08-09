using InfotecsInternTask.ApplicationLayer.Interfaces;
using InfotecsInternTask.DomainLayer.DTO;
using InfotecsInternTask.DomainLayer.Entities;

namespace InfotecsInternTask.InfrastructureLayer.Mapping
{
    public class ResultAggregateMapper : IResultAggregateMapper
    {
        public Result Map(IntegralResultDto resultDto, IEnumerable<CsvValueDto> valuesDto, string fileName)
        {
            var result = new Result
            {
                Filename = fileName,
                Deltatime = resultDto.DeltaTime,
                Mindate = resultDto.MinDate,
                Averageexecutiontime = resultDto.AverageExecutionTime,
                Averagevalue = resultDto.AverageValue,
                Medianvalue = resultDto.MedianValue,
                Maxvalue = resultDto.MaxValue,
                Minvalue = resultDto.MinValue,
                Values = valuesDto.Select(v => new Value
                {
                    Date = v.Date,
                    Executiontime = v.ExecutionTime,
                    Value1 = v.Value
                }).ToList()
            };

            return result;
        }
    }
}
