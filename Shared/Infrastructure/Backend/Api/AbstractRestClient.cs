using Microsoft.Extensions.Logging;
using RestSharp;
using Shared.Logger;

namespace Shared.Infrastructure.Backend.Api
{
    public abstract class AbstractRestClient : IRestClient
    {
        /// <summary>
        /// Maximum time till request abort
        /// </summary>
        public const int RequestTimeout = 5000;
        public static RestClient RestClient = null;
        public static Uri DefaultUrl = null;
        private List<KeyValuePair<string, string>> _permanentHeaders = new List<KeyValuePair<string, string>>();
        private readonly ILogger<AbstractRestClient> logger;

        public List<KeyValuePair<string, string>> PermanentHeaders { get => _permanentHeaders; }

        public AbstractRestClient(ILogger<AbstractRestClient> logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// Create a new singleton instance of RestClient
        /// Lifetime: ApplicationLifetime
        /// 
        /// </summary>
        /// <param name="defaultUrl"></param>
        /// <returns></returns>
        [Obsolete("will be remove in short time and will be changed to 'TypedHttpClient' over dependency injection")]
        public static RestClient Create(string defaultUrl = null)
        {
            if (Uri.TryCreate(defaultUrl, new UriCreationOptions(), out Uri uri))
            {
                DefaultUrl = uri;
            }
            RestClient = RestClient == null ? new RestClient() : RestClient;
            return RestClient;
        }

        public void AddPermanentHeaderValue(string header, string value)
        {
            if (string.IsNullOrEmpty(header) || string.IsNullOrEmpty(value))
            {
                return;
            }
            if (!_permanentHeaders.Exists(x => x.Key.ToLower() == header.ToLower() && x.Value.ToLower() == value.ToLower()))
            {
                _permanentHeaders.Add(new KeyValuePair<string, string>(header, value));
            }
        }

        public virtual RestRequest CreateRequest(string urlOrEndpoint, Method httpMethod, ContentType contentType, string body = null, List<KeyValuePair<string, string>> query = null, List<KeyValuePair<string, string>> headerValues = null)
        {
            if (!Uri.TryCreate(urlOrEndpoint, new UriCreationOptions(), out Uri validationResult))
            {
                string exceptionMessage = "uri not valid '" + urlOrEndpoint + "'";
                logger.LogMethod(exceptionMessage);
                throw new Exception(exceptionMessage);
            }
            Uri final = DefaultUrl == null ? validationResult : new Uri(DefaultUrl, validationResult);
            var requestObject = new RestRequest(final);

            if (string.IsNullOrEmpty(contentType) || string.IsNullOrEmpty(urlOrEndpoint))
            {
                string exceptionMessage = $"{nameof(contentType)} or {nameof(urlOrEndpoint)} is null or empty";
                logger.LogMethod(exceptionMessage);
                throw new ArgumentNullException(exceptionMessage);
            }

            if (body != null)
            {
                logger.LogMethod($"set body with content-type {contentType}: {body}");
                requestObject.AddHeader("Content-Type", contentType);
                requestObject.AddBody(body, contentType);
            }
            if (query != null)
            {
                logger.LogMethod($"start to add query parameters");
                foreach (var item in query)
                {
                    logger.LogMethod($"query-parameter: {item.Key}={item.Value}");
                    requestObject.AddQueryParameter(item.Key, item.Value);
                }
            }
            if (PermanentHeaders.Count > 0)
            {
                logger.LogMethod($"permanent header values are given, add them");
                foreach (var item in PermanentHeaders)
                {
                    logger.LogMethod($"perm-header: {item.Key}={item.Value}");
                    requestObject.AddHeader(item.Key, item.Value);
                }
            }
            if (headerValues != null)
            {
                logger.LogMethod($"header values are given (temporary), add them");
                foreach (var item in headerValues)
                {
                    logger.LogMethod($"temp-header: {item.Key}={item.Value}");
                    requestObject.AddHeader(item.Key, item.Value);
                }
            }
            logger.LogMethod($"request timeout: {RequestTimeout}");
            requestObject.Timeout = RequestTimeout;
            requestObject.Method = httpMethod;

            return requestObject;
        }

        public void DeletePermantentHeaderValue(string header)
        {
            if (string.IsNullOrEmpty(header))
            {
                return;
            }
            int index = -1;
            for (int i = 0; i < _permanentHeaders.Count; i++)
            {
                var kvP = _permanentHeaders[i];
                if (kvP.Key.ToLower() == header.ToLower())
                {
                    index = i;
                    break;
                }
            }
            if (index != -1)
            {
                _permanentHeaders.RemoveAt(index);
            }
        }

        public virtual async Task<RestResponse> Send(RestRequest restRequest, CancellationToken cancellationToken)
        {
            if (RestClient == null)
            {
                RestClient = Create();
                logger.LogMethod($"create restclient (httpclient) instance as singleton (static var for store instance)");
            }
            logger.LogMethod($"{nameof(RestClient.ExecuteAsync)}: start execution");
            var response = await RestClient.ExecuteAsync(restRequest, cancellationToken);
            logger.LogMethod($"{nameof(RestClient.ExecuteAsync)}: executed");
            return response;
        }
    }
}
