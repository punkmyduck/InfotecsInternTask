using InfotecsInternTask.ApplicationLayer.Interfaces;
using InfotecsInternTask.DomainLayer.Entities;
using InfotecsInternTask.InfrastructureLayer.EfCoreDbContext;
using Microsoft.EntityFrameworkCore;

namespace InfotecsInternTask.InfrastructureLayer.Repositories
{
    public class ResultRepository : IResultRepository
    {
        private ProcessesdbContext _processesdbContext;
        public ResultRepository(ProcessesdbContext processesdbContext)
        {
            _processesdbContext = processesdbContext ?? throw new ArgumentNullException(nameof(processesdbContext));
        }
        public async Task AddOrReplaceResultAsync(Result result)
        {
            var existingResult = await _processesdbContext.Results.Include(r => r.Values).FirstOrDefaultAsync(r => r.Filename == result.Filename);

            if (existingResult != null)
            {
                _processesdbContext.Values.RemoveRange(existingResult.Values);
                _processesdbContext.Results.Remove(existingResult);
                await _processesdbContext.SaveChangesAsync();
            }

            _processesdbContext.Results.Add(result);
            await _processesdbContext.SaveChangesAsync();
        }
    }
}
