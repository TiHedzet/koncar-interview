namespace Koncar.Interview.Server.Application;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public static class Module
{
    public static IHostApplicationBuilder AddApplicationModule(this IHostApplicationBuilder builder)
    {
        builder.Services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(typeof(Module).Assembly);
        });

        return builder;
    }
}
