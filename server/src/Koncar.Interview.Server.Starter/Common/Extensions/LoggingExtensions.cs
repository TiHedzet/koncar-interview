namespace Koncar.Interview.Server.Starter.Common.Extensions;

using Koncar.Interview.Server.Starter.Common.Logging;
using Serilog;
using Serilog.Configuration;

internal static class LoggingExtensions
{
    private const string LogFilePath = "Logs/log.txt";
    private const string LogOutputTemplate =
      "{Timestamp:o} [Thread:{ThreadId}] [{Level:u3}] [{Properties}] ({SourceContext}) {Message:lj}{_Exception}{NewLine}";

    public static LoggerConfiguration WithEscapedExceptionMessage(this LoggerEnrichmentConfiguration enrich)
    {
        return enrich.With<EscapedExceptionMessageEnricher>();
    }

    public static LoggerConfiguration ConfigureLogger(
        this LoggerConfiguration loggerConfiguration,
        IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(loggerConfiguration);


        return loggerConfiguration
            .ReadFrom.Configuration(configuration)
            .Enrich.FromLogContext()
            .Enrich.WithMachineName()
            .Enrich.WithEscapedExceptionMessage()
            .WriteTo.Async(static configuration =>
            {
                configuration.Console(outputTemplate: LogOutputTemplate);
                configuration.File(
                    LogFilePath,
                    rollingInterval: RollingInterval.Day,
                    outputTemplate: LogOutputTemplate);
            });
    }
}
