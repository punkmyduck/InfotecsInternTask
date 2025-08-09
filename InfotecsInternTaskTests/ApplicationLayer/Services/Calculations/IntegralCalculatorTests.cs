using InfotecsInternTask.DomainLayer.DTO;

namespace InfotecsInternTask.ApplicationLayer.Services.Calculations.Tests
{
    [TestClass()]
    public class IntegralCalculatorTests
    {
        private IntegralCalculator _calculator;

        [TestInitialize]
        public void Setup()
        {
            _calculator = new IntegralCalculator();
        }

        private CsvValueDto CreateDto(DateTime date, int execTime, float value)
        {
            return new CsvValueDto
            {
                Date = date,
                ExecutionTime = execTime,
                Value = value
            };
        }

        [TestMethod]
        public void Calculate_OddCount_CorrectMedian()
        {
            var values = new List<CsvValueDto>
            {
                CreateDto(new DateTime(2025, 1, 1), 100, 10f),
                CreateDto(new DateTime(2025, 1, 2), 200, 20f),
                CreateDto(new DateTime(2025, 1, 3), 300, 30f)
            };

            var result = _calculator.Calculate(values);

            Assert.AreEqual(172800, result.DeltaTime);
            Assert.AreEqual(new DateTime(2025, 1, 1), result.MinDate);
            Assert.AreEqual(200, result.AverageExecutionTime);
            Assert.AreEqual(20f, result.AverageValue);
            Assert.AreEqual(20f, result.MedianValue);
            Assert.AreEqual(30f, result.MaxValue);
            Assert.AreEqual(10f, result.MinValue);
        }

        [TestMethod]
        public void Calculate_EvenCount_CorrectMedian()
        {
            var values = new List<CsvValueDto>
            {
                CreateDto(new DateTime(2025, 1, 1), 10, 10f),
                CreateDto(new DateTime(2025, 1, 2), 20, 20f),
                CreateDto(new DateTime(2025, 1, 3), 30, 30f),
                CreateDto(new DateTime(2025, 1, 4), 40, 40f)
            };

            var result = _calculator.Calculate(values);

            Assert.AreEqual((20f + 30f) / 2, result.MedianValue);
            Assert.AreEqual(25f, result.AverageValue);
            Assert.AreEqual(25, result.AverageExecutionTime);
            Assert.AreEqual(10f, result.MinValue);
            Assert.AreEqual(40f, result.MaxValue);
        }
    }
}