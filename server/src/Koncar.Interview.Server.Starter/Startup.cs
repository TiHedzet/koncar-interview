namespace Koncar.Interview.Server.Starter;

using Koncar.Interview.Server.Application;
using Koncar.Interview.Server.Infrastructure.Db.Postgres;
using Koncar.Interview.Server.Presentation.Api;
using Koncar.Interview.Server.Starter.Common.ExceptionHandlers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Scalar.AspNetCore;
using Serilog;
using System;

internal sealed class Startup
{
    public static void ConfigureServices(WebApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.AddPresentationModule();
        builder.AddApplicationModule();
        builder.AddKoncarDbModule(builder.Configuration);

        builder.Services.AddProblemDetails();
        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
        builder.Services.AddSerilog();
        builder.Services.AddHealthChecks();
    }

    public static void Configure(WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseHsts();
        }

        app.UseRouting();
        app.UseSerilogRequestLogging();
        app.UseExceptionHandler();
        app.UseStatusCodePages();

        app.MapControllers();
        app.MapHealthChecks("/health");
        
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference();
        }
    }
}
