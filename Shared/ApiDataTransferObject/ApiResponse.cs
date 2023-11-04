using Domain.ValueObjects;
using Shared.ApiDataTransferObject.Object;
using Shared.Linq;
using System.Text.Json.Serialization;

namespace Shared.ApiDataTransferObject
{
    public class ApiResponse<T>
    {
        [JsonPropertyName("data")]
        public ApiData<T> Data { get; set; }
        [JsonPropertyName("errors")]
        public List<ApiError> Errors { get; set; }
        [JsonPropertyName("meta")]
        public ApiMeta Meta { get; set; }
        public ApiResponse() 
        {

        }

        public static ApiResponse<T> Create(Result<T> result, params ApiError[] apiError)
        {
            var input = result.Value;
            if(!result.IsSuccess)
            {
                if(result.Error != null)
                {
                    var errorObj = new ApiError();
                    errorObj.Message = result.Error.Message;
                    apiError.Append(errorObj);
                }
            }
            return Create(input, apiError);
        }
        public static ApiResponse<T> Create(T data, params ApiError[] apiError)
        {
            string type = null;
            if(ListExtension.IsGenericList(data))
            {
                var t = typeof(T).GetGenericArguments();
                type = t.FirstOrDefault().Name;
            }
            else
            {

                type =nameof(data); 
            }

            var response = new ApiResponse<T>();
            response.Data = new ApiData<T> { Value = data,Type= type };
            response.Errors = apiError?.ToList();
            response.Meta = ApiMeta.Create();
            return response;
        }
    }
}
