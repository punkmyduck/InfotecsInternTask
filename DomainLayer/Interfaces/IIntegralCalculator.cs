using InfotecsInternTask.DomainLayer.DTO;

namespace InfotecsInternTask.DomainLayer.Interfaces
{
    public interface IIntegralCalculator
    {
        IntegralResultDto Calculate(List<CsvValueDto> values);
    }
}
