using Microsoft.AspNetCore.Mvc.ModelBinding;
using Shared.CustomException;
using Shared.DataFilter.Presentation;
using System.Text.Json;

namespace Presentation.Modelbinding.Binder
{
    public class SearchParamBodyModelBinder : IModelBinder
    {
        public static JsonSerializerOptions JsonSerializerOptions = new JsonSerializerOptions { WriteIndented = true, DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull };
        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }
            if (bindingContext.HttpContext.Request.Query.Any()&& (bindingContext.HttpContext.Request.ContentLength == null || bindingContext.HttpContext.Request.ContentLength == 0))
            {
                bindingContext.Result = ModelBindingResult.Success(null);
                return;
            }

            SearchParamsBody data= new SearchParamsBody(); 
            string pageSizeValueStr = null;
            string pageOffSetValueStr = null;
            string filtersValueStr = null;
            string orderValueStr = null;

            using (var reader = new StreamReader(bindingContext.HttpContext.Request.Body))
            {
                try
                {

                    var body = await reader.ReadToEndAsync();
                    if (String.IsNullOrEmpty(body))
                    {
                        throw new ModelBindingFailedException($"body is null");
                    }
                    data = JsonSerializer.Deserialize<SearchParamsBody>(body, JsonSerializerOptions);
                    pageSizeValueStr = data.page_size.ToString();
                    pageOffSetValueStr = data.page_offset.ToString();

                    var filtersStr = data.filters!=null?JsonSerializer.Serialize(data.filters, JsonSerializerOptions) :null;
                    var orderByStr = data.order_by!=null?JsonSerializer.Serialize(data.order_by, JsonSerializerOptions) : null;
                    filtersValueStr = filtersStr;
                    orderValueStr = orderByStr;
                }
                catch (Exception ex)
                {
                    throw new ModelBindingFailedException($"searchparams could not deserialized; {ex.Message}");
                }
            }

            SearchParams searchParams = null;
            try
            {
                searchParams = SearchParamQueryModelBinderHelpers.ExtractSearchParams(
                    bindingContext,
                    pageSizeValueStr,
                    pageOffSetValueStr,
                    filtersValueStr,
                    orderValueStr);
            }
            catch (Exception ex)
            {
                throw;
            }

            data.SearchParams = searchParams;

            bindingContext.Result = ModelBindingResult.Success(data);
        }
    }
}
