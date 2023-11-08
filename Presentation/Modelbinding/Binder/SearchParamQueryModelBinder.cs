using Microsoft.AspNetCore.Mvc.ModelBinding;
using Shared.CustomException;
using Shared.DataFilter.Presentation;
using System.Text.Json;

namespace Presentation.Modelbinding.Binder
{
    public class SearchParamQueryModelBinder : IModelBinder
    {
        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }
            string pageSizeValueStr = null;
            string pageOffSetValueStr = null;
            string filtersValueStr = null;
            string orderValueStr = null; 
            if (!bindingContext.HttpContext.Request.Query.Any())
            {
                bindingContext.Result = ModelBindingResult.Success(null);
                return;
            }
            var page_sizeValue = bindingContext.ValueProvider.GetValue(nameof(SearchParams.page_size));
            var page_offsetValue = bindingContext.ValueProvider.GetValue(nameof(SearchParams.page_offset));
            var filtersValue = bindingContext.ValueProvider.GetValue(nameof(SearchParams.filters));
            var order_byValue = bindingContext.ValueProvider.GetValue(nameof(SearchParams.order_by));
            pageSizeValueStr = page_sizeValue.FirstValue;
            pageOffSetValueStr = page_offsetValue.FirstValue;
            filtersValueStr = filtersValue.FirstValue;
            orderValueStr = order_byValue.FirstValue;
            if (page_sizeValue.FirstValue == null || page_offsetValue.FirstValue == null)
            {
                bindingContext.Result = ModelBindingResult.Failed();
                throw new ModelBindingFailedException($"{nameof(SearchParams.page_size)} or {nameof(SearchParams.page_offset)} is not given, both are required");
            }
            if (page_sizeValue.Count() > 1)
            {
                bindingContext.Result = ModelBindingResult.Failed();
                throw new ModelBindingFailedException($"{nameof(SearchParams.page_size)} is given more than one times");
            }

            if (page_offsetValue.Count() > 1)
            {
                bindingContext.Result = ModelBindingResult.Failed();
                throw new ModelBindingFailedException($"{nameof(SearchParams.page_offset)} is given more than one times");
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

            bindingContext.Result = ModelBindingResult.Success(searchParams);
        }
    }
}
