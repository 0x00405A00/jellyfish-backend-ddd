using Microsoft.AspNetCore.Mvc.ModelBinding;
using Shared.CustomException;
using Shared.DataFilter.Presentation;

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
            if (!bindingContext.HttpContext.Request.Query.Any())
            {
                bindingContext.Result = ModelBindingResult.Success(null);
                return;
            }
            var page_sizeValue = bindingContext.ValueProvider.GetValue(nameof(SearchParams.page_size));
            var page_offsetValue = bindingContext.ValueProvider.GetValue(nameof(SearchParams.page_offset));
            var filtersValue = bindingContext.ValueProvider.GetValue(nameof(SearchParams.filters));
            var order_byValue = bindingContext.ValueProvider.GetValue(nameof(SearchParams.order_by));

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

            string pageSizeValueStr = page_sizeValue.FirstValue;
            string pageOffSetValueStr = page_offsetValue.FirstValue;
            string filtersValueStr = filtersValue.FirstValue;
            string orderValueStr = order_byValue.FirstValue;

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
            catch(Exception ex)
            {
                throw;
            }

            bindingContext.Result = ModelBindingResult.Success(searchParams);
        }
    }
}
