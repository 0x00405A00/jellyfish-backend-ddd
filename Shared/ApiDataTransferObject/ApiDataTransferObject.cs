using Domain.ValueObjects;
using Shared.ApiDataTransferObject.Object;
using Shared.Linq;
using System.Text.Json.Serialization;

namespace Shared.ApiDataTransferObject
{
    public class ApiDataTransferObject<T>
    {
        [JsonPropertyName("data")]
        public ApiData<T>? Data { get; set; }
        [JsonPropertyName("errors")]
        public List<ApiError>? Errors { get; set; }
        [JsonIgnore()]
        public bool HasErrors => Errors!=null&& Errors.Any();
        [JsonPropertyName("meta")]
        public ApiMeta? Meta { get; set; }
        public ApiDataTransferObject() 
        {

        }

        public static ApiDataTransferObject<T> Error(List<ApiError> apiError)
        {
            return Create(null, null, apiError);
        }
        public static ApiDataTransferObject<T> Create(Result<T> result, PaginationBase paginationBase = null, List<ApiError> apiError = null)
        {
            T input = default;
            if (result != null)
            {
                input = result.Value;
                if (!result.IsSuccess)
                {
                    if (apiError == null)
                    {
                        apiError = new List<ApiError>();
                    }
                    if (result.Error != null)
                    {
                        var errorObj = new ApiError();
                        errorObj.Message = result.Error.Message;
                        apiError.Add(errorObj);
                    }
                }
            }
            return Create(input,paginationBase, apiError);
        }
        public static ApiDataTransferObject<T> Create(T data, PaginationBase paginationBase = null, List<ApiError> apiError = null)
        {
            string type = "undefined";
            bool isList = ListExtension.IsGenericList(data);
            if (isList)
            {
                var t = typeof(T).GetGenericArguments();
                type = t.FirstOrDefault().Name;
            }
            else
            {
                if(data!= null)
                {
                    type = typeof(T).Name;
                }
            }

            var response = new ApiDataTransferObject<T>();
            response.Data = new ApiData<T> { Value = data,Type= type };
            response.Errors = apiError?.ToList();
            response.Meta = ApiMeta.Create();

            if(isList && paginationBase!=null)
            {
                var pagination = new Pagination();
                pagination.TotalItems = paginationBase.TotalItems;
                pagination.CurrentPage = paginationBase.CurrentPage;
                pagination.PrevPage = paginationBase.PrevPage;
                pagination.PerPage = paginationBase.PerPage;
                pagination.NextPage = paginationBase.NextPage;
                pagination.TotalPages = paginationBase.TotalPages;
                response.Meta.Pagination = pagination;
            }

            return response;
        }
    }
    public static class ApiDataTransferObjectExtension
    {
        public static ApiDataTransferObject<T> Create<T>(this T data, PaginationBase paginationBase = null, List<ApiError> apiError = null)
        {
            return ApiDataTransferObject<T>.Create(data, paginationBase, apiError);
        }
        public static ApiDataTransferObject<T> Create<T>(this Result<T> result, PaginationBase paginationBase = null, List<ApiError> apiError = null)
        {
            return ApiDataTransferObject<T>.Create(result, paginationBase, apiError);
        }
    }
}
