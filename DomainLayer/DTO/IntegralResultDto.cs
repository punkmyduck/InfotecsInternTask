using InfotecsInternTask.DomainLayer.Entities;

namespace InfotecsInternTask.DomainLayer.DTO
{
    public class IntegralResultDto
    {
        public int DeltaTime { get; set; }

        public DateTime MinDate { get; set; }

        public int AverageExecutionTime { get; set; }

        public double AverageValue { get; set; }

        public double MedianValue { get; set; }

        public double MaxValue { get; set; }

        public double MinValue { get; set; }
    }
}
