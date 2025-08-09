using InfotecsInternTask.DomainLayer.DTO;
using InfotecsInternTask.DomainLayer.Entities;

namespace InfotecsInternTask.ApplicationLayer.Interfaces
{
    public interface IResultAggregateMapper
    {
        Result Map(IntegralResultDto resultDto, IEnumerable<CsvValueDto> valuesDto, string fileName);
    }
}
