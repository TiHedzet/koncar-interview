namespace Koncar.Interview.Client.Infrastructure.Http.Internal.Settings;

internal sealed class HttpAdapterSettings
{
    public const string Key = nameof(HttpAdapterSettings);

    public string BaseUrl { get; set; } = string.Empty;
}
