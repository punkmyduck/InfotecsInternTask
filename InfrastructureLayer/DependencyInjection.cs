using InfotecsInternTask.DomainLayer.Interfaces;
using InfotecsInternTask.InfrastructureLayer.EfCoreDbContext;
using InfotecsInternTask.InfrastructureLayer.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InfotecsInternTask.InfrastructureLayer
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IResultRepository, ResultRepository>();
            services.AddScoped<IValueRepository, ValueRepository>();

            services.AddDbContext<ProcessesdbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }
    }
}
