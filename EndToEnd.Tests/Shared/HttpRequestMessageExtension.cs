namespace EndToEnd.Tests.Shared
{
    public static class HttpRequestMessageExtension
    {
        public static HttpRequestMessage AddUri(this HttpRequestMessage request, HttpMethod httpMethod, string uri)
        {
            try
            {
                request.RequestUri = new Uri(uri, UriKind.RelativeOrAbsolute);
                request.Method = httpMethod;
                return request;
            }
            catch (Exception e) 
            {
                throw;
            }
        }
        public static HttpRequestMessage PrepareRequest(this HttpRequestMessage request, string token = null)
        {
            request.Headers.Add("User-Agent", "NETHTTPCL");
            request.Headers.Add("Accept", "application/json");
            if (token != null)
                request.Headers.Add("Authorization", ("Bearer "+ token));

            return request;
        }
    }
}
