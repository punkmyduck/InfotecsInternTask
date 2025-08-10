using InfotecsInternTask.ApplicationLayer.Interfaces;
using InfotecsInternTask.ApplicationLayer.Services.Calculations;
using InfotecsInternTask.ApplicationLayer.Services.Orchestrators;
using InfotecsInternTask.ApplicationLayer.Services.QueryServices;
using InfotecsInternTask.ApplicationLayer.Services.Validation;
using InfotecsInternTask.DomainLayer.DTO;
using InfotecsInternTask.DomainLayer.Interfaces;
using InfotecsInternTask.InfrastructureLayer.Interfaces;
using InfotecsInternTask.InfrastructureLayer.Mapping;
using InfotecsInternTask.InfrastructureLayer.Parsing;

namespace InfotecsInternTask.ApplicationLayer
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            services.AddScoped<CsvProcessingService>();
            services.AddScoped<ICsvParser<CsvValueDto>, CsvParser>();
            services.AddScoped<ICsvValuesValidator, CsvValuesValidator>();
            services.AddScoped<IIntegralCalculator, IntegralCalculator>();
            services.AddScoped<IResultAggregateMapper, ResultAggregateMapper>();
            services.AddScoped<IResultsQueryService, ResultQueryService>();
            services.AddScoped<IValuesQueryService, ValueQueryService>();
            return services;
        }
    }
}
