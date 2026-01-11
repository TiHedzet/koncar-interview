namespace Koncar.Interview.Server.Starter;

using Koncar.Interview.Server.Starter.Common.Cosntants;
using Koncar.Interview.Server.Starter.Common.Extensions;
using Microsoft.AspNetCore.Builder;
using Serilog;
using System;
using System.Threading.Tasks;

public class Program
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

            Startup.ConfigureServices(builder);

            await using WebApplication app = builder.Build();

            Startup.Configure(app);

            await app.RunAsync();

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
    }
}