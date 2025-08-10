using InfotecsInternTask.DomainLayer.Entities;
using InfotecsInternTask.DomainLayer.Interfaces;
using InfotecsInternTask.InfrastructureLayer.EfCoreDbContext;
using Microsoft.EntityFrameworkCore;

namespace InfotecsInternTask.InfrastructureLayer.Repositories
{
    public class ValueRepository : IValueRepository
    {
        private readonly ProcessesdbContext _context;

        public ValueRepository(ProcessesdbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Value>> GetLast10ByFileAsync(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentException("File name must be provided.", nameof(fileName));

            return await _context.Values
                .Where(v => v.Result.Filename == fileName)
                .OrderByDescending(v => v.Date)
                .Take(10)
                .ToListAsync();
        }
    }
}
