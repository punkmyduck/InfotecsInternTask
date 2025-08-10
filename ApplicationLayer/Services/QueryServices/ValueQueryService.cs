using InfotecsInternTask.ApplicationLayer.Interfaces;
using InfotecsInternTask.DomainLayer.Entities;
using InfotecsInternTask.DomainLayer.Interfaces;

namespace InfotecsInternTask.ApplicationLayer.Services.QueryServices
{
    public class ValueQueryService : IValuesQueryService
    {
        private IValueRepository _valueRepository;
        public ValueQueryService(IValueRepository valueRepository)
        {
            _valueRepository = valueRepository;
        }
        public async Task<IEnumerable<Value>> GetLast10ByFileAsync(string fileName)
        {
            return await _valueRepository.GetLast10ByFileAsync(fileName);
        }
    }
}
