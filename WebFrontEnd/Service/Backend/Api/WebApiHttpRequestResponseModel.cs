using RestSharp;

namespace WebFrontEnd.Service.Backend.Api
{
    public class WebApiHttpRequestResponseModel<T>
    {
        public T ApiResponseDeserialized { get; set; }
        public RestResponse DefaultResponse { get; set; } = new RestResponse() { StatusCode = System.Net.HttpStatusCode.NotFound };
        public bool IsSuccess
        {
            get
            {
                return DefaultResponse != null && DefaultResponse.IsSuccessStatusCode;
            }
        }
    }
}
