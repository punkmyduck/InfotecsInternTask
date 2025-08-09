using InfotecsInternTask.DomainLayer.DTO;

namespace InfotecsInternTask.ApplicationLayer.Interfaces
{
    public interface ICsvValuesValidator
    {
        bool IsValid(IEnumerable<CsvValueDto> values);
    }
}
