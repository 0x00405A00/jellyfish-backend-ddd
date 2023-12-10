using Microsoft.AspNetCore.Mvc.ModelBinding;
using Shared.CustomException;
using Shared.DataFilter.Presentation;

internal static class SearchParamQueryModelBinderHelpers
{

    public static SearchParams ExtractSearchParams(ModelBindingContext bindingContext, string pageSizeValueStr, string pageOffSetValueStr, string filtersValueStr, string orderValueStr)
    {

        if ((pageSizeValueStr is null && pageOffSetValueStr is not null) || (pageSizeValueStr is not null && pageOffSetValueStr is null))
        {
            bindingContext.Result = ModelBindingResult.Failed();
            throw new ModelBindingFailedException($"{nameof(SearchParams.page_size)} or {nameof(SearchParams.page_offset)} is not given, both are required");
        }

        int page_size = -1;
        int page_offset = -1;
        string filters = filtersValueStr;
        string order_by = orderValueStr;

        if (pageSizeValueStr != null || pageOffSetValueStr != null)
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

        return searchParams;
    }
}