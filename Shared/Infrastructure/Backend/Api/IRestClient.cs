﻿using RestSharp;

namespace Shared.Infrastructure.Backend.Api
{
    public interface IRestClient
    {
        public List<KeyValuePair<string, string>> PermanentHeaders { get; }
        public RestRequest CreateRequest(string urlOrEndpoint, Method httpMethod, ContentType contentType, string body = null, List<KeyValuePair<string, string>> query = null, List<KeyValuePair<string, string>> headerValues = null);
        public Task<RestResponse> Send(RestRequest restRequest, CancellationToken cancellationToken);
        public void AddPermanentHeaderValue(string header, string value);
        public void DeletePermantentHeaderValue(string header);
    }
}
