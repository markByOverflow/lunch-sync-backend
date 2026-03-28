using LunchSync.Core;
using LunchSync.Infrastructure;
using LunchSync.Api.Middleware;

using Microsoft.OpenApi.Models;

namespace LunchSync.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add Layers
            builder.Services.AddCore();
            builder.Services.AddInfrastructure(builder.Configuration);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new()
                {
                    Title = "LunchSync API",
                    Version = "v1",
                    Description = "API for LunchSync - Group Lunch Decision App"
                });
            });
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "LunchSync API v1");
                    c.RoutePrefix = string.Empty; // serve UI at application root
                });
            }

            app.UseHttpsRedirection();

            app.UseGlobalExceptionHandler();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
