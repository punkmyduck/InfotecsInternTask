using InfotecsInternTask.DomainLayer.DTO;

namespace InfotecsInternTask.ApplicationLayer.Interfaces
{
    public interface IIntegralCalculator
    {
        IntegralResultDto Calculate(List<CsvValueDto> values);
    }
}
