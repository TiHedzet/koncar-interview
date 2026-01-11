namespace Koncar.Interview.Client.Presentation.Tui;

using Koncar.Interview.Client.Presentation.Tui.Contracts.Presenters;
using Koncar.Interview.Client.Presentation.Tui.Contracts.Windows;
using Koncar.Interview.Client.Presentation.Tui.Presenters;
using Koncar.Interview.Client.Presentation.Tui.Windows;
using Microsoft.Extensions.DependencyInjection;
using Terminal.Gui.App;

public static class Module
{
    public static IServiceCollection AddPresentationModule(this IServiceCollection services)
    {
        services.AddSingleton(_ => Application.Create().Init(driverName: "dotnet"));
        services.AddSingleton<IMainWindow, MainWindow>();
        services.AddSingleton<ICharacterPresenter, CharacterPresenter>();

        return services;
    } 
}
