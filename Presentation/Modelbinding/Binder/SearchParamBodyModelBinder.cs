using Microsoft.AspNetCore.Mvc.ModelBinding;
using Shared.CustomException;
using Shared.DataFilter.Presentation;
using System.Text.Json;

namespace Presentation.Modelbinding.Binder
{
    public class SearchParamBodyModelBinder : IModelBinder
    {
        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }
            SearchParamsBody data= new SearchParamsBody();  
            string pageSizeValueStr = null;
            string pageOffSetValueStr = null;
            string filtersValueStr = null;
            string orderValueStr = null;
            if (bindingContext.HttpContext.Request.Query.Count()==0 &&bindingContext.HttpContext.Request.ContentLength!=0)
            {
                using (var reader = new StreamReader(bindingContext.HttpContext.Request.Body))
                {
                    try
                    {
                        var jsonOpt = new JsonSerializerOptions { WriteIndented = true };
                        var body = await reader.ReadToEndAsync();
                        if (String.IsNullOrEmpty(body))
                        {
                            throw new ModelBindingFailedException($"body is null");
                        }
                        data = JsonSerializer.Deserialize<SearchParamsBody>(body, jsonOpt);
                        pageSizeValueStr = data.page_size.ToString();
                        pageOffSetValueStr = data.page_offset.ToString();

                        var filtersStr =JsonSerializer.Serialize(data.filters);
                        var orderByStr = JsonSerializer.Serialize(data.order_by);
                        filtersValueStr = filtersStr;
                        orderValueStr = orderByStr;
                    }
                    catch (Exception ex)
                    {
                        throw new ModelBindingFailedException($"searchparams could not deserialized; {ex.Message}");
                    }
                }
            }

            int page_size = -1;
            int page_offset = -1;
            string filters = filtersValueStr;
            string order_by = orderValueStr;

            if (pageSizeValueStr!=null || pageOffSetValueStr!=null)
            {
                
                bool pageSizeParse = int.TryParse(pageSizeValueStr, out page_size);
                bool pageOffSetParse = int.TryParse(pageOffSetValueStr, out page_offset);
                if (!pageSizeParse)
                {
                    bindingContext.Result = ModelBindingResult.Failed();
                    throw new ModelBindingFailedException($"{nameof(SearchParams.page_size)} is not a valid integer value");
                }
                if (!pageOffSetParse)
                {
                    bindingContext.Result = ModelBindingResult.Failed();
                    throw new ModelBindingFailedException($"{nameof(SearchParams.page_offset)} is not a valid integer value");
                }
                if (page_size < 1)
                {
                    bindingContext.Result = ModelBindingResult.Failed();
                    throw new ModelBindingFailedException($"{nameof(SearchParams.page_size)} is not valid");
                }
                if (page_offset < 1)
                {
                    bindingContext.Result = ModelBindingResult.Failed();
                    throw new ModelBindingFailedException($"{nameof(SearchParams.page_offset)} is not valid");
                }

            }
            SearchParams searchParams = new SearchParams();
            searchParams.page_size = page_size;
            searchParams.page_offset = page_offset;
            searchParams.filters = filters;
            searchParams.order_by = order_by;

            data.SearchParams = searchParams;

            bindingContext.Result = ModelBindingResult.Success(data);
        }
    }
}
