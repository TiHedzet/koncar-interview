namespace Koncar.Interview.Client.Application;

using Microsoft.Extensions.DependencyInjection;

public static class Module
{
    public static IServiceCollection AddApplicationModule(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(typeof(Module).Assembly);
        });

        return services;
    }
}
