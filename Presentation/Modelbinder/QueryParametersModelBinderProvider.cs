using Microsoft.AspNetCore.Mvc.ModelBinding;
using Shared.DataFilter.Presentation;

namespace Presentation.Modelbinder
{
    public class QueryParametersModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context.Metadata.ModelType == typeof(SearchParams))
            {
                return new SearchQueryParamModelBinder();
            }
            return null;
        }
    }
}
