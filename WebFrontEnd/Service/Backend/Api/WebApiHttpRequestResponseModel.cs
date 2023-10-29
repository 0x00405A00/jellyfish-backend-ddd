using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
