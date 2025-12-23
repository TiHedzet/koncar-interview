namespace Koncar.Interview.Server.Starter.Common.Extensions;

internal static class StringExtensions
{
    public static string EscapeNewLine(
        this string value,
        string substitue = "\\r\\n")
    => value.Replace(Environment.NewLine, substitue);
}
