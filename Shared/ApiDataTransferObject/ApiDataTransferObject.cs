using Domain.ValueObjects;
using Shared.ApiDataTransferObject.Object;
using Shared.Linq;
using System.Text.Json.Serialization;

namespace Shared.ApiDataTransferObject
{
    public partial class ApiDataTransferObject<T>
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

        public static ApiDataTransferObject<T> Create(Result<T> result, PaginationBase paginationBase, List<ApiError> apiError)
        {
            var input = result.Value;
            if(!result.IsSuccess)
            {
                if(apiError==null)
                {
                    apiError =new List<ApiError>();
                }
                if(result.Error != null)
                {
                    var errorObj = new ApiError();
                    errorObj.Message = result.Error.Message;
                    apiError.Add(errorObj);
                }
            }
            return Create(input,paginationBase, apiError);
        }
        public static ApiDataTransferObject<T> Create(T data, PaginationBase paginationBase, List<ApiError> apiError)
        {
            string type = null;
            bool isList = ListExtension.IsGenericList(data);
            if (isList)
            {
                var t = typeof(T).GetGenericArguments();
                type = t.FirstOrDefault().Name;
            }
            else
            {
                type =nameof(data); 
            }

            var response = new ApiDataTransferObject<T>();
            response.Data = new ApiData<T> { Value = data,Type= type };
            response.Errors = apiError?.ToList();
            response.Meta = ApiMeta.Create();

            if(isList)
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
}
