namespace Koncar.Interview.Client.Infrastructure.Http.Internal.Common;

internal static class Endpoints
{
    private const string Api = "api";

    public static class V1
    {
        private const string Version = "v1";

        public static class Characters
        {
            public const string Route = $"{Api}/{Version}/characters";
        }
    }
}
