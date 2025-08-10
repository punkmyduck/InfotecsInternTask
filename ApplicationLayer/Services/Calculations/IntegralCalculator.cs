using InfotecsInternTask.DomainLayer.DTO;
using InfotecsInternTask.DomainLayer.Interfaces;

namespace InfotecsInternTask.ApplicationLayer.Services.Calculations
{
    public class IntegralCalculator : IIntegralCalculator
    {
        public IntegralResultDto Calculate(List<CsvValueDto> values)
        {
            double sumExecTime = 0;
            double sumValue = 0;
            double minValue = double.MaxValue;
            double maxValue = double.MinValue;
            DateTime minDate = DateTime.MaxValue;
            DateTime maxDate = DateTime.MinValue;

            var valueList = new List<double>(values.Count);

            foreach (var v in values)
            {
                if (v.Date < minDate) minDate = v.Date;
                if (v.Date > maxDate) maxDate = v.Date;

                if (v.Value < minValue) minValue = v.Value;
                if (v.Value > maxValue) maxValue = v.Value;

                sumExecTime += v.ExecutionTime;
                sumValue += v.Value;

                valueList.Add(v.Value);
            }

            double avgExecTime = sumExecTime / values.Count;
            double avgValue = sumValue / values.Count;

            valueList.Sort();
            double median = (valueList.Count % 2 == 1)
                ? valueList[valueList.Count / 2]
                : (valueList[(valueList.Count / 2) - 1] + valueList[valueList.Count / 2]) / 2.0;

            return new IntegralResultDto
            {
                DeltaTime = (int)(maxDate - minDate).TotalSeconds,
                MinDate = DateTime.SpecifyKind(minDate, DateTimeKind.Unspecified),
                AverageExecutionTime = (int)avgExecTime,
                AverageValue = (float)avgValue,
                MedianValue = median,
                MaxValue = maxValue,
                MinValue = minValue
            };
        }
    }
}
