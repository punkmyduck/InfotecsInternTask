using InfotecsInternTask.DomainLayer.Entities;

namespace InfotecsInternTask.ApplicationLayer.Interfaces
{
    public interface IResultRepository
    {
        Task AddResultWithValueAsync(Result result);
    }
}
