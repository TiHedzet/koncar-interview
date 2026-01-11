namespace Koncar.Interview.Server.Presentation.Api.Common;

using Microsoft.AspNetCore.Routing;
using System.Globalization;
using System.Text.RegularExpressions;

internal partial class SlugifyParameterTransformer : IOutboundParameterTransformer
{
    private const string SlugifyRegexPattern = "([a-z])([A-Z])";
    private const string SlugifyRegexReplacement = "$1-$2";

    public string? TransformOutbound(object? value)
    {
        string? valueString = value?.ToString();

        return !string.IsNullOrWhiteSpace(valueString)
            ? SlugifyRegex().Replace(valueString, SlugifyRegexReplacement).ToLower(CultureInfo.InvariantCulture)
            : null;
    }

    [GeneratedRegex(SlugifyRegexPattern)]
    private static partial Regex SlugifyRegex();
}
