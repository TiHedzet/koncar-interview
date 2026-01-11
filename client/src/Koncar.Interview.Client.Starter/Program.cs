namespace Koncar.Interview.Client.Starter;

using Koncar.Interview.Client.Presentation.Tui.Contracts.Presenters;
using Koncar.Interview.Client.Presentation.Tui.Contracts.Windows;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using Terminal.Gui.App;
using Terminal.Gui.Views;

internal static class Program
{
    static void Main(string[] args)
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.Development.json", optional: true)
            .AddJsonFile("appsettings.json")
            .AddUserSecrets(typeof(Program).Assembly, optional: true)
            .Build();

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .Enrich.FromLogContext()
            .WriteTo.File(
                "Logs/log.txt", 
                rollingInterval: RollingInterval.Day)
            .CreateLogger();

        try
        {
            Log.Logger.Information("Application starting...");

            IServiceCollection serviceCollection = new ServiceCollection();

            serviceCollection.AddSingleton(configuration);
            serviceCollection.AddLogging(builder =>
            {
                builder.AddSerilog(Log.Logger);
            });

            Startup.ConfigureServices(serviceCollection, configuration);

            using ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

            IApplication application = serviceProvider.GetRequiredService<IApplication>();
            IMainWindow main = serviceProvider.GetRequiredService<IMainWindow>();

            _ = serviceProvider.GetRequiredService<ICharacterPresenter>();

            application.Run((Window)main, ExceptionHandler);
            Log.Logger.Information("Application shutting down...");
        }
        catch (Exception ex)
        {
            Log.Logger.Error("Something went wrong during startup.", ex);
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    private static bool ExceptionHandler(Exception ex)
    {
        Log.Logger.Error("An unexpected error occurred", ex);
        return true;
    }
}
