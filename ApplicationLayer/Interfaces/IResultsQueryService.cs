using InfotecsInternTask.DomainLayer.DTO;
using InfotecsInternTask.DomainLayer.Entities;

namespace InfotecsInternTask.ApplicationLayer.Interfaces
{
    public interface IResultsQueryService
    {
        Task<IEnumerable<Result?>> FilterResultsAsync(ResultFilterDto filterParams);
    }
}
