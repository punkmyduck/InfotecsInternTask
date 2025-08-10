using InfotecsInternTask.DomainLayer.DTO;
using InfotecsInternTask.DomainLayer.Entities;

namespace InfotecsInternTask.DomainLayer.Interfaces
{
    public interface IResultRepository
    {
        Task AddOrReplaceResultAsync(Result result);
        Task<IEnumerable<Result>> GetFilteredAsync(ResultFilterDto filter);
    }
}
