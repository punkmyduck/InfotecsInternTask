using System.Globalization;
using InfotecsInternTask.DomainLayer.DTO;
using InfotecsInternTask.InfrastructureLayer.Interfaces;

namespace InfotecsInternTask.InfrastructureLayer.Parsing
{
    public class CsvParser : ICsvParser<CsvValueDto>
    {
        public List<CsvValueDto> Parse(Stream stream)
        {
            var values = new List<CsvValueDto>();

            using var reader = new StreamReader(stream);
            string? line;
            var lineNumber = 1;

            line = reader.ReadLine();
            if (line == null || !line.Trim().Equals("Date;ExecutionTime;Value", StringComparison.OrdinalIgnoreCase))
            {
                throw new FormatException("CSV header is invalid. Expected: 'Date;ExecutionTime;Value'");
            }

            while ((line = reader.ReadLine()) != null)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                lineNumber++;

                var parts = line.Split(';', StringSplitOptions.TrimEntries);
                if (parts.Length != 3)
                    throw new FormatException($"Line {lineNumber}: expected 3 fields, got {parts.Length}");

                if (!DateTime.TryParseExact(parts[0], "yyyy-MM-ddTHH:mm:ss.ffffZ", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out var date))
                    throw new FormatException($"Line {lineNumber}: Invalid Date '{parts[0]}'");

                if (!Int32.TryParse(parts[1], NumberStyles.Integer, CultureInfo.InvariantCulture, out var executionTime))
                    throw new FormatException($"Line {lineNumber}: Invalid ExecutionTime '{parts[1]}'");

                if (!float.TryParse(parts[2], NumberStyles.Float, CultureInfo.InvariantCulture, out var value))
                    throw new FormatException($"Line {lineNumber}: Invalid Value '{parts[2]}'");

                values.Add(new CsvValueDto
                {
                    Date = date,
                    ExecutionTime = executionTime,
                    Value = value
                });
            }

            return values;
        }
    }
}
