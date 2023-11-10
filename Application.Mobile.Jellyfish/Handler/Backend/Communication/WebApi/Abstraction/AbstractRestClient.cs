using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace MobileApp.Handler.Backend.Communication.WebApi.Abstraction
{
    public abstract class AbstractRestClient : IRestClient
    {
        public static RestClient RestClient = null;
        public static Uri DefaultUrl = null;
        private List<KeyValuePair<string, string>> _permanentHeaders = new List<KeyValuePair<string, string>>();
        public List<KeyValuePair<string, string>> PermanentHeaders { get => _permanentHeaders; }

        public AbstractRestClient()
        {

        }
        public static RestClient Create(string defaultUrl = null)
        {
            if(Uri.TryCreate(defaultUrl,new UriCreationOptions(),out Uri uri))
            {
                DefaultUrl = uri;
            }
            RestClient = RestClient == null ? new RestClient() : RestClient;
            return RestClient;
        }

        public void AddPermanentHeaderValue(string header, string value)
        {
            if(String.IsNullOrEmpty(header) || String.IsNullOrEmpty(value))
            {
                return;
            }
            if(!_permanentHeaders.Exists( x=> x.Key.ToLower() == header.ToLower() && x.Value.ToLower() == value.ToLower()))
            {
                _permanentHeaders.Add(new KeyValuePair<string, string>(header,value));
            }
        }

        public virtual RestRequest CreateRequest(string urlOrEndpoint,RestSharp.Method httpMethod, ContentType contentType, string body = null, List<KeyValuePair<string, string>> query = null, List<KeyValuePair<string, string>> headerValues = null)
        {
            if(!Uri.TryCreate(urlOrEndpoint, new UriCreationOptions(),out Uri validationResult))
            {
                throw new Exception("uri not valid '"+urlOrEndpoint +"'");
            }
            Uri final = (DefaultUrl == null ?validationResult: new Uri(DefaultUrl,validationResult));
            var requ = new RestRequest(final);

            if (String.IsNullOrEmpty(contentType) || String.IsNullOrEmpty(urlOrEndpoint))
                throw new ArgumentNullException();

            if(body != null)
            {
                requ.AddHeader("Content-Type", contentType);
                requ.AddBody(body, contentType);
            }
            if(query != null)
            {
                foreach (var item in query)
                {
                    requ.AddQueryParameter(item.Key, item.Value);
                }
            }
            if(PermanentHeaders.Count > 0)
            {
                foreach (var item in PermanentHeaders)
                {
                    requ.AddHeader(item.Key, item.Value);
                }
            }
            if(headerValues != null)
            {
                foreach (var item in headerValues)
                {
                    requ.AddHeader(item.Key, item.Value);
                }
            }
            requ.Timeout = 5000;
            requ.Method = httpMethod;

            return requ;
        }

        public void DeletePermantentHeaderValue(string header)
        {
            if (String.IsNullOrEmpty(header))
            {
                return;
            }
            int index = -1;
            for(int i=0;i< _permanentHeaders.Count;i++)
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
            if(RestClient==null)
            {
                RestClient = Create();
            }
            var resp = await RestClient.ExecuteAsync(restRequest,cancellationToken);
            return resp;
        }
    }
}
