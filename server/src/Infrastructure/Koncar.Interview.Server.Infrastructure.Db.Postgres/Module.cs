namespace Koncar.Interview.Server.Infrastructure.Db.Postgres;

using Koncar.Interview.Server.Application.Contracts.Repositories;
using Koncar.Interview.Server.Infrastructure.Db.Postgres.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

public static class Module
{
    private const string PostgreSqlConnectionName = "koncarDbConnection";
    public static IHostApplicationBuilder AddKoncarDbModule(
        this IHostApplicationBuilder builder,
        IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(builder);

        string? connectionString = configuration.GetConnectionString(PostgreSqlConnectionName);

        ArgumentNullException.ThrowIfNull(connectionString);

        builder.Services.AddDbContext<DatabaseContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });

        builder.Services.AddScoped<ICharacterRepository, CharacterRepository>(); 

        return builder;
    }
}
