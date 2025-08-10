using InfotecsInternTask.DomainLayer.Entities;

namespace InfotecsInternTask.ApplicationLayer.Interfaces
{
    public interface IValuesQueryService
    {
        Task<IEnumerable<Value>> GetLast10ByFileAsync(string fileName);
    }
}
