namespace Shared.Http
{
    public static class Extension
    {
        public static bool IsValidUrl(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }
        public static Uri? Create(string url)
        {
            var uri = Uri.TryCreate(url, UriKind.Absolute, out var uriResult) ? uriResult : null;
            return uri;
        }
    }
}
