using InfotecsInternTask.ApplicationLayer.Interfaces;
using InfotecsInternTask.DomainLayer.DTO;

namespace InfotecsInternTask.ApplicationLayer.Services.Validation
{
    public class CsvValuesValidator : ICsvValuesValidator
    {
        private const int MaxRows = 10_000;
        private const int MinRows = 1;
        private readonly DateTime _lowerBound = new DateTime(2000, 1, 1);

        public bool IsValid(IEnumerable<CsvValueDto> values)
        {
            var linesCount = values.Count();
            if (linesCount < MinRows || linesCount > MaxRows)
                return false;

            var upperBound = DateTime.UtcNow;

            foreach (var v in values)
            {
                if (v.Date < _lowerBound || v.Date > upperBound) return false;
                if (v.ExecutionTime < 0) return false;
                if (v.Value < 0) return false;
            }

            return true;
        }
    }
}
