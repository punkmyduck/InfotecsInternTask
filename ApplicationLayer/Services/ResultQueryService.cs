using InfotecsInternTask.ApplicationLayer.Interfaces;
using InfotecsInternTask.DomainLayer.DTO;
using InfotecsInternTask.DomainLayer.Entities;
using InfotecsInternTask.DomainLayer.Interfaces;

namespace InfotecsInternTask.ApplicationLayer.Services
{
    public class ResultQueryService : IResultsQueryService
    {
        private IResultRepository _resultRepository;
        public ResultQueryService(IResultRepository resultRepository)
        {
            _resultRepository = resultRepository;
        }
        public async Task<IEnumerable<Result?>> FilterResultsAsync(ResultFilterDto filterParams)
        {
            return await _resultRepository.GetFilteredAsync(filterParams);
        }
    }
}
