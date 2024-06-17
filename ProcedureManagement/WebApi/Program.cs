
using Infrastructure;
using Infrastructure.Data;
using Application;
using WebApi.Endpoints;
using WebApi.Infrastructure;
using Serilog;

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

            try
            {
                Log.Information("Starting web application...");

                var builder = WebApplication.CreateBuilder(args);

                // Add services to the container.
                builder.Services.AddApplicationServices();
                builder.Services.AddInfrastructureServices(builder.Configuration);

                builder.Services.AddExceptionHandler<CustomExceptionHandler>();

                builder.Services.AddHealthChecks();

                builder.Services.AddAuthorization();

                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                builder.Services.AddEndpointsApiExplorer();

                var app = builder.Build();

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    using var scope = app.Services.CreateScope();

                    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                    dbContext.Database.EnsureCreated();
                }

                app.UseSerilogRequestLogging();

                app.UseHealthChecks("/health");

                app.UseHttpsRedirection();

                app.UseAuthorization();

                app.UseExceptionHandler(options => { });

                // Endpoint mapping
                app.MapGroup("api/v1/procedures")
                   .MapProcedureEndpoints();

                app.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
