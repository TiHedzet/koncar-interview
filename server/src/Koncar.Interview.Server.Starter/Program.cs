using Koncar.Interview.Server.Starter.Common.Cosntants;
using Koncar.Interview.Server.Starter.Common.Extensions;
using Serilog;

public partial class Program
{
    private static async Task<int> Main(string[] args)
    {
        try
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        Log.Logger = new LoggerConfiguration()
            .ConfigureLogger(builder.Configuration)
            .CreateBootstrapLogger();

        
            Log.Information("Starting web host.");

            builder.

            return ExitCodes.Success;

        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Host terminated unexpectedly");
            return ExitCodes.Error;
        }
        finally
        {
            await Log.CloseAndFlushAsync();
        }
        builder.Services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}