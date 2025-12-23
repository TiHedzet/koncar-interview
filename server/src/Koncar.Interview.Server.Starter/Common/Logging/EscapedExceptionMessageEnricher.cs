namespace Koncar.Interview.Server.Starter.Common.Logging;

using Koncar.Interview.Server.Starter.Common.Extensions;
using Serilog.Core;
using Serilog.Events;

public sealed class EscapedExceptionMessageEnricher : ILogEventEnricher
{
    public void Enrich(
        LogEvent logEvent,
        ILogEventPropertyFactory propertyFactory)
    {
        if (logEvent.Exception is null)
        {
            return;
        }

        LogEventProperty logEventProperty = propertyFactory.CreateProperty(
            name: "_Exception",
            value: logEvent.Exception
                .ToString()
                .EscapeNewLine());

        logEvent.AddPropertyIfAbsent(logEventProperty);
    }
}
