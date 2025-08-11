using InfotecsInternTask.ApplicationLayer;
using InfotecsInternTask.InfrastructureLayer;
using InfotecsInternTask.InfrastructureLayer.EfCoreDbContext;
using Microsoft.EntityFrameworkCore;

namespace InfotecsInternTask
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<ProcessesdbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("ProcessesDb")));

            builder.Services.AddApplicationLayer();
            builder.Services.AddInfrastructureLayer(builder.Configuration);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ProcessesdbContext>();
                db.Database.EnsureCreated();
            }

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
