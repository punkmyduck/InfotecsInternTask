namespace InfotecsInternTask.DomainLayer.DTO
{
    public class ResultFilterDto
    {
        public string? FileName { get; set; }
        public DateTime? MinStartDate { get; set; }
        public DateTime? MaxStartDate { get; set; }
        public double? MinAverageValue { get; set; }
        public double? MaxAverageValue { get; set; }
        public int? MinAverageExecutionTime { get; set; }
        public int? MaxAverageExecutionTime { get; set; }
    }
}
