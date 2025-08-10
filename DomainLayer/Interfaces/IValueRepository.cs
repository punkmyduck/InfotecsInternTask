using InfotecsInternTask.DomainLayer.Entities;

namespace InfotecsInternTask.DomainLayer.Interfaces
{
    public interface IValueRepository
    {
        Task<IEnumerable<Value>> GetLast10ByFileAsync(string fileName);
    }
}
