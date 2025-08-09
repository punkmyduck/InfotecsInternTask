using InfotecsInternTask.ApplicationLayer.Interfaces;
using InfotecsInternTask.DomainLayer.DTO;

namespace InfotecsInternTask.ApplicationLayer.Services.Calculations
{
    public class IntegralCalculator : IIntegralCalculator
    {
        public IntegralResultDto Calculate(List<CsvValueDto> values)
        {
            DateTime maxDate = values.Max(v => v.Date);
            DateTime minDate = values.Min(v => v.Date);
            double minValue = values.Min(v => v.Value);
            double maxValue = values.Max(v => v.Value);
            double averageExecutionTime = values.Average(v => v.ExecutionTime);
            double averageValue = values.Average(v => v.Value);

            var valuesList = values.Select(v => v.Value).ToList();
            double median;
            if (valuesList.Count % 2 == 1)
            {
                median = valuesList[valuesList.Count / 2];
            }
            else
            {
                double middle1 = valuesList[(valuesList.Count / 2) - 1];
                double middle2 = valuesList[valuesList.Count / 2];
                median = (middle1 + middle2) / 2;
            }

            return new IntegralResultDto
            {
                DeltaTime = (int)(maxDate - minDate).TotalSeconds,
                MinDate = DateTime.SpecifyKind(minDate, DateTimeKind.Unspecified),
                AverageExecutionTime = (int)averageExecutionTime,
                AverageValue = (float)averageValue,
                MedianValue = median,
                MaxValue = maxValue,
                MinValue = minValue
            };
        }
    }
}
