using RestSharp;

namespace Shared.Infrastructure.Backend.Api
{
    public class WebApiHttpRequestResponseModel<T>
    {
        public T ApiResponseDeserialized { get; set; }
        public RestResponse DefaultResponse { get; set; } = new RestResponse() { StatusCode = System.Net.HttpStatusCode.NotFound };
        public bool IsSuccessStatusCode
        {
            get
            {
                return DefaultResponse != null && DefaultResponse.IsSuccessStatusCode;
            }
        }
        public bool HasDeserializedData => ApiResponseDeserialized != null;
    }
}
