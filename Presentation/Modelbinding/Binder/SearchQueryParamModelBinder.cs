using Microsoft.AspNetCore.Mvc.ModelBinding;
using Shared.CustomException;
using Shared.DataFilter.Presentation;

namespace Presentation.Modelbinding.Binder
{
    public class SearchQueryParamModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }
            var page_sizeValue = bindingContext.ValueProvider.GetValue(nameof(SearchParams.page_size));
            var page_offsetValue = bindingContext.ValueProvider.GetValue(nameof(SearchParams.page_offset));
            var filtersValue = bindingContext.ValueProvider.GetValue(nameof(SearchParams.filters));
            var order_byValue = bindingContext.ValueProvider.GetValue(nameof(SearchParams.order_by));

            int page_size = -1;
            int page_offset = -1;
            string filters = filtersValue.Any() && filtersValue.Count() == 1 ? filtersValue.FirstValue : null;
            string order_by = order_byValue.Any() && order_byValue.Count() == 1 ? order_byValue.FirstValue : null;

            if (page_sizeValue.Any() || page_offsetValue.Any())
            {
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
                bool pageSizeParse = int.TryParse(page_sizeValue.FirstValue, out page_size);
                bool pageOffSetParse = int.TryParse(page_offsetValue.FirstValue, out page_offset);
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
            return Task.CompletedTask;
        }
    }
}
