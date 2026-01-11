namespace Koncar.Interview.Client.Infrastructure.Http;

using Koncar.Interview.Client.Application.Contracts.Adapters;
using Koncar.Interview.Client.Infrastructure.Http.Internal.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

public static class Module
{
    public static IServiceCollection AddHttpProviderModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        HttpAdapterSettings? httpAdapterSettings = configuration
            .GetRequiredSection(HttpAdapterSettings.Key)
            .Get<HttpAdapterSettings>();

        ArgumentNullException.ThrowIfNull(httpAdapterSettings);

        services.AddHttpClient<ICharacterServiceAdapter, CharacterServiceAdapter>(builder =>
        {
            builder.BaseAddress = new Uri(httpAdapterSettings.BaseUrl);
        });

        return services;
    }
}
