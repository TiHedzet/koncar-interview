namespace Koncar.Interview.Client.Presentation.Tui.Internal;

internal static class CancellationTokenProvider
{
    public static CancellationTokenSource Create(CancellationToken? parentToken = null)
    {
        if (parentToken is null)
        {
           return new CancellationTokenSource();
        }

        return CancellationTokenSource.CreateLinkedTokenSource(parentToken.Value);
    }
}
