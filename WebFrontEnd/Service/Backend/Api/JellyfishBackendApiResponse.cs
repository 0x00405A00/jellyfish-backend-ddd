using Shared.ApiDataTransferObject;
using Shared.ApiDataTransferObject.Object;

namespace WebFrontEnd.Service.Backend.Api
{
    public partial class JellyfishBackendApi
    {
        public class JellyfishBackendApiResponse<T> 
        {
            public WebApiHttpRequestResponseModel<ApiDataTransferObject<T>> DefaultResponse { get; set; }
            public bool IsSuccess => DefaultResponse != null && DefaultResponse.IsSuccess;
            public bool HasErrors => DefaultResponse.HasDeserializedData && DefaultResponse.ApiResponseDeserialized.HasErrors;
            public List<ApiError> Errors
            {
                get
                {
                    if (!HasErrors)
                        return new List<ApiError>();
                    return DefaultResponse.ApiResponseDeserialized.Errors;
                }
            }
            public T Data
            {
                get
                {
                    if (!DefaultResponse.HasDeserializedData)
                    {
                        return default;
                    }
                    return DefaultResponse.ApiResponseDeserialized.Data.Value;
                }
            }

            public static JellyfishBackendApiResponse<T> CreateFromWebApiResponseModel(WebApiHttpRequestResponseModel<ApiDataTransferObject<T>> response)
            {
                JellyfishBackendApiResponse<T> backendApiResponse = new JellyfishBackendApi.JellyfishBackendApiResponse<T>();
                backendApiResponse.DefaultResponse = response;
                return backendApiResponse;
            }

        }
    }
}
