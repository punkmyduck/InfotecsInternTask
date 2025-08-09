using InfotecsInternTask.DomainLayer.DTO;

namespace InfotecsInternTask.ApplicationLayer.Services.Validation.Tests
{
    [TestClass()]
    public class CsvValuesValidatorTests
    {
        private CsvValuesValidator _validator;

        [TestInitialize]
        public void Setup()
        {
            _validator = new CsvValuesValidator();
        }

        private CsvValueDto CreateDto(DateTime date, int execTime = 1, float value = 1f)
        {
            return new CsvValueDto
            {
                Date = date,
                ExecutionTime = execTime,
                Value = value
            };
        }

        [TestMethod]
        public void IsValid_ValidValues_ReturnsTrue()
        {
            var list = new List<CsvValueDto>
            {
                CreateDto(DateTime.UtcNow.AddDays(-1)),
                CreateDto(DateTime.UtcNow.AddDays(-100))
            };

            var result = _validator.IsValid(list);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsValid_TooFewRows_ReturnsFalse()
        {
            var list = new List<CsvValueDto>();
            var result = _validator.IsValid(list);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsValid_TooManyRows_ReturnsFalse()
        {
            var list = new List<CsvValueDto>();
            for (int i = 0; i < 10001; i++)
                list.Add(CreateDto(DateTime.UtcNow.AddDays(-1)));

            var result = _validator.IsValid(list);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsValid_DateBeforeLowerBound_ReturnsFalse()
        {
            var list = new List<CsvValueDto>
            {
                CreateDto(new DateTime(1999, 12, 31))
            };

            var result = _validator.IsValid(list);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsValid_DateAfterUpperBound_ReturnsFalse()
        {
            var list = new List<CsvValueDto>
            {
                CreateDto(DateTime.UtcNow.AddDays(1))
            };

            var result = _validator.IsValid(list);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsValid_NegativeExecutionTime_ReturnsFalse()
        {
            var list = new List<CsvValueDto>
            {
                CreateDto(DateTime.UtcNow.AddDays(-1), execTime: -5)
            };

            var result = _validator.IsValid(list);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsValid_NegativeValue_ReturnsFalse()
        {
            var list = new List<CsvValueDto>
            {
                CreateDto(DateTime.UtcNow.AddDays(-1), value: -10f)
            };

            var result = _validator.IsValid(list);

            Assert.IsFalse(result);
        }
    }
}