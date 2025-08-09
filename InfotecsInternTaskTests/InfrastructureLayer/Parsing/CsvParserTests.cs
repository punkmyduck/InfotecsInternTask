using System.Text;
using InfotecsInternTask.InfrastructureLayer.Parsing;

namespace InfotecsInternTask.InfotecsInternTaskTests.InfrastructureLayer.Parsing
{
    [TestClass()]
    public class CsvParserTests
    {
        private CsvParser _parser;

        [TestInitialize]
        public void Setup()
        {
            _parser = new CsvParser();
        }

        private Stream CreateStreamFromString(string content)
        {
            return new MemoryStream(Encoding.UTF8.GetBytes(content));
        }

        [TestMethod]
        public void Parse_ValidCsv_ReturnsListOfCsvValueDto()
        {
            // Arrange
            var csv =
@"Date;ExecutionTime;Value
2025-08-09T12:34:56.1234Z;123;45.67";

            using var stream = CreateStreamFromString(csv);

            // Act
            var result = _parser.Parse(stream);

            // Assert
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(new DateTime(2025, 8, 9, 12, 34, 56, 123, DateTimeKind.Utc).AddTicks(4000), result[0].Date);
            Assert.AreEqual(123, result[0].ExecutionTime);
            Assert.AreEqual(45.67f, result[0].Value);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void Parse_InvalidHeader_ThrowsFormatException()
        {
            var csv =
@"WrongHeader;ExecutionTime;Value
2025-08-09T12:34:56.1234Z;123;45.67";

            using var stream = CreateStreamFromString(csv);
            _parser.Parse(stream);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void Parse_NotEnoughFields_ThrowsFormatException()
        {
            var csv =
@"Date;ExecutionTime;Value
2025-08-09T12:34:56.1234Z;123";

            using var stream = CreateStreamFromString(csv);
            _parser.Parse(stream);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void Parse_InvalidDateFormat_ThrowsFormatException()
        {
            var csv =
@"Date;ExecutionTime;Value
09-08-2025 12:34:56;123;45.67";

            using var stream = CreateStreamFromString(csv);
            _parser.Parse(stream);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void Parse_InvalidExecutionTime_ThrowsFormatException()
        {
            var csv =
@"Date;ExecutionTime;Value
2025-08-09T12:34:56.1234Z;abc;45.67";

            using var stream = CreateStreamFromString(csv);
            _parser.Parse(stream);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void Parse_InvalidValue_ThrowsFormatException()
        {
            var csv =
@"Date;ExecutionTime;Value
2025-08-09T12:34:56.1234Z;123;abc";

            using var stream = CreateStreamFromString(csv);
            _parser.Parse(stream);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void Parse_EmptyLine_ThrowsFormatException()
        {
            string csv = @"";
            using var stream = CreateStreamFromString(csv);
            _parser.Parse(stream);
        }
    }
}