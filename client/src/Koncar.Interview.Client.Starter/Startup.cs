namespace Koncar.Interview.Client.Starter;

using Koncar.Interview.Client.Application;
using Koncar.Interview.Client.Infrastructure.Http;
using Koncar.Interview.Client.Presentation.Tui;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

internal static class Startup
{
    public static void ConfigureServices(
        IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddPresentationModule();
        services.AddApplicationModule();
        services.AddHttpProviderModule(configuration);
    }
}
