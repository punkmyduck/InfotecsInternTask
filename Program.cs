using InfotecsInternTask.ApplicationLayer.Services.Calculations;
using InfotecsInternTask.ApplicationLayer.Services.Validation;
using InfotecsInternTask.DomainLayer.DTO;
using InfotecsInternTask.InfrastructureLayer.Mapping;
using InfotecsInternTask.InfrastructureLayer.Parsing;
using InfotecsInternTask.InfrastructureLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using InfotecsInternTask.InfrastructureLayer.EfCoreDbContext;
using InfotecsInternTask.DomainLayer.Interfaces;
using InfotecsInternTask.ApplicationLayer.Interfaces;
using InfotecsInternTask.InfrastructureLayer.Interfaces;
using InfotecsInternTask.ApplicationLayer.Services.Orchestrators;
using InfotecsInternTask.ApplicationLayer.Services;

namespace InfotecsInternTask
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddScoped<CsvProcessingService>();
            builder.Services.AddScoped<ICsvParser<CsvValueDto>, CsvParser>();
            builder.Services.AddScoped<ICsvValuesValidator, CsvValuesValidator>();
            builder.Services.AddScoped<IIntegralCalculator, IntegralCalculator>();
            builder.Services.AddScoped<IResultAggregateMapper, ResultAggregateMapper>();

            builder.Services.AddScoped<IResultRepository, ResultRepository>();

            builder.Services.AddScoped<IResultsQueryService, ResultQueryService>();

            builder.Services.AddDbContext<ProcessesdbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
