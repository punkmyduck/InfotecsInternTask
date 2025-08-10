using InfotecsInternTask.DomainLayer.DTO;
using InfotecsInternTask.DomainLayer.Entities;
using InfotecsInternTask.DomainLayer.Interfaces;
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
            using var transaction = await _processesdbContext.Database.BeginTransactionAsync();

            var existingResult = await _processesdbContext.Results
                .Include(r => r.Values)
                .FirstOrDefaultAsync(r => r.Filename == result.Filename);

            if (existingResult != null)
            {
                _processesdbContext.Values.RemoveRange(existingResult.Values);

                existingResult.Averageexecutiontime = result.Averageexecutiontime;
                existingResult.Averagevalue = result.Averagevalue;
                existingResult.Deltatime = result.Deltatime;
                existingResult.Maxvalue = result.Maxvalue;
                existingResult.Medianvalue = result.Medianvalue;
                existingResult.Mindate = result.Mindate;
                existingResult.Minvalue = result.Minvalue;

                existingResult.Values = result.Values;
            }
            else
            {
                _processesdbContext.Results.Add(result);
            }

            await _processesdbContext.SaveChangesAsync();
            await transaction.CommitAsync();
        }

        public async Task<IEnumerable<Result>> GetFilteredAsync(ResultFilterDto filter)
        {
            var query = _processesdbContext.Results.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.FileName))
            {
                query = query.Where(r => r.Filename == filter.FileName);
            }

            if (filter.MinStartDate.HasValue)
            {
                query = query.Where(r => r.Mindate >= filter.MinStartDate);
            }
            if (filter.MaxStartDate.HasValue)
            {
                query = query.Where(r => r.Mindate <= filter.MaxStartDate);
            }

            if (filter.MinAverageValue.HasValue)
                query = query.Where(r => r.Averagevalue >= filter.MinAverageValue);
            if (filter.MaxAverageValue.HasValue)
                query = query.Where(r => r.Averagevalue <= filter.MaxAverageValue);

            if (filter.MinAverageExecutionTime.HasValue)
                query = query.Where(r => r.Averageexecutiontime >= filter.MinAverageExecutionTime);
            if (filter.MaxAverageExecutionTime.HasValue)
                query = query.Where(r => r.Averageexecutiontime <= filter.MaxAverageExecutionTime);

            return await query.ToListAsync();
        }
    }
}
