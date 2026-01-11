namespace Koncar.Interview.Client.Application.Internal;

using Microsoft.Extensions.Logging;
using System;

internal static partial class Log
{
    [LoggerMessage(
        Level = LogLevel.Error,
        Message = "An unexpected error occurred.")]
    public static partial void LogUnexpectedError(
        this ILogger logger,
        Exception ex);
}
