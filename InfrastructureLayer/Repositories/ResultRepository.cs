using InfotecsInternTask.ApplicationLayer.Interfaces;
using InfotecsInternTask.DomainLayer.Entities;
using InfotecsInternTask.InfrastructureLayer.EfCoreDbContext;

namespace InfotecsInternTask.InfrastructureLayer.Repositories
{
    public class ResultRepository : IResultRepository
    {
        private ProcessesdbContext _processesdbContext;
        public ResultRepository(ProcessesdbContext processesdbContext)
        {
            _processesdbContext = processesdbContext ?? throw new ArgumentNullException(nameof(processesdbContext));
        }
        public async Task AddResultWithValueAsync(Result result)
        {
            _processesdbContext.Results.Add(result);
            await _processesdbContext.SaveChangesAsync();
        }
    }
}
