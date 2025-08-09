using System.ComponentModel.DataAnnotations;
using InfotecsInternTask.ApplicationLayer.Interfaces;
using InfotecsInternTask.DomainLayer.DTO;

namespace InfotecsInternTask.ApplicationLayer.Services
{
    public class CsvProcessingService
    {
        private readonly ICsvParser<CsvValueDto> _parser;
        private readonly ICsvValuesValidator _validator;
        private readonly IIntegralCalculator _integralCalculator;
        private readonly IResultAggregateMapper _resultAggregateMapper;
        private readonly IResultRepository _resultRepository;

        public CsvProcessingService(
            ICsvParser<CsvValueDto> parser, 
            ICsvValuesValidator validator, 
            IIntegralCalculator integralCalculator, 
            IResultAggregateMapper resultAggregateMapper,
            IResultRepository resultRepository)
        {
            _parser = parser;
            _validator = validator;
            _integralCalculator = integralCalculator;
            _resultAggregateMapper = resultAggregateMapper;
            _resultRepository = resultRepository;
        }

        public async Task Process(IFormFile file)
        {
            using var fileStream = file.OpenReadStream();
            var valuesDto = _parser.Parse(fileStream);
            if (!_validator.IsValid(valuesDto))
                throw new ValidationException("Invalid data in CSV file.");
            var resultDto = _integralCalculator.Calculate(valuesDto);
            var result = _resultAggregateMapper.Map(resultDto, valuesDto, file.FileName) ?? throw new Exception();
            await _resultRepository.AddResultWithValueAsync(result);
        }
    }
}
