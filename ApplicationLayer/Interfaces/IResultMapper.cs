using InfotecsInternTask.DomainLayer.DTO;
using InfotecsInternTask.DomainLayer.Entities;

namespace InfotecsInternTask.ApplicationLayer.Interfaces
{
    public interface IResultMapper
    {
        Result MapFromDto(IntegralResultDto dto);
    }
}
