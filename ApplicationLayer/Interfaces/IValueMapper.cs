using InfotecsInternTask.DomainLayer.DTO;
using InfotecsInternTask.DomainLayer.Entities;

namespace InfotecsInternTask.ApplicationLayer.Interfaces
{
    public interface IValueMapper
    {
        Value MapFromDto(CsvValueDto dto);
        IEnumerable<Value> MapFromDtoList(IEnumerable<CsvValueDto> dtos);
    }
}
