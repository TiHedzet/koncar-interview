namespace Koncar.Interview.Client.Presentation.Tui.Internal;

using Microsoft.Extensions.Logging;
using System;

internal static partial class Log
{
    [LoggerMessage(
        Level = LogLevel.Information,
        Message = "Cancelled operation {Operation}.")]
    public static partial void LogCancellation(
        this ILogger logger,
        string operation);

    [LoggerMessage(
        Level = LogLevel.Error,
        Message = "An unexpected error occurred.")]
    public static partial void LogUnhandledError(
        this ILogger logger,
        Exception ex);
}
