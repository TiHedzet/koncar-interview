namespace Koncar.Interview.Server.Presentation.Api;

using Asp.Versioning;
using Koncar.Interview.Server.Presentation.Api.Common;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public static class Module
{
    public static IHostApplicationBuilder AddPresentationModule(this IHostApplicationBuilder builder)
    {
        builder.Services
            .AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
            })
            .AddMvc();

        builder.Services.AddOpenApi();
        builder.Services.AddControllers(options =>
        {
            options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
        });

        return builder;
    }
}

