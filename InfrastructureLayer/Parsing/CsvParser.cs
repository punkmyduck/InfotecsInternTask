using System.Globalization;
using InfotecsInternTask.DomainLayer.DTO;
using InfotecsInternTask.InfrastructureLayer.Interfaces;

namespace InfotecsInternTask.InfrastructureLayer.Parsing
{
    public class CsvParser : ICsvParser<CsvValueDto>
    {
        private const string ExpectedHeader = "Date;ExecutionTime;Value";
        private const string DateFormat = "yyyy-MM-ddTHH:mm:ss.ffffZ";

        public List<CsvValueDto> Parse(Stream stream)
        {
            if (stream == null) throw new ArgumentNullException(nameof(stream));

            var values = new List<CsvValueDto>();

            using var reader = new StreamReader(stream);
            string? line;
            var lineNumber = 1;

            line = reader.ReadLine();
            if (line == null || !line.Trim().Equals(ExpectedHeader, StringComparison.OrdinalIgnoreCase))
                throw new FormatException($"CSV header is invalid. Expected: '{ExpectedHeader}'");

            while ((line = reader.ReadLine()) != null)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                lineNumber++;
                var parts = line.Split(';', StringSplitOptions.TrimEntries);

                if (parts.Length != 3)
                    throw new FormatException($"Line {lineNumber}: expected 3 fields, got {parts.Length}");

                var date = ParseDate(parts[0], lineNumber);
                var execTime = ParseInt(parts[1], "ExecutionTime", lineNumber);
                var value = ParseFloat(parts[2], "Value", lineNumber);

                values.Add(new CsvValueDto
                {
                    Date = date,
                    ExecutionTime = execTime,
                    Value = value
                });
            }

            return values;
        }

        private DateTime ParseDate(string input, int lineNumber)
        {
            if (!DateTime.TryParseExact(input, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out var date))
                throw new FormatException($"Line {lineNumber}: Invalid Date '{input}'");
            return DateTime.SpecifyKind(date, DateTimeKind.Unspecified);
        }

        private int ParseInt(string input, string fieldName, int lineNumber)
        {
            if (!int.TryParse(input, NumberStyles.Integer, CultureInfo.InvariantCulture, out var result))
                throw new FormatException($"Line {lineNumber}: Invalid {fieldName} '{input}'");
            return result;
        }

        private float ParseFloat(string input, string fieldName, int lineNumber)
        {
            if (!float.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out var result))
                throw new FormatException($"Line {lineNumber}: Invalid {fieldName} '{input}'");
            return result;
        }
    }
}
