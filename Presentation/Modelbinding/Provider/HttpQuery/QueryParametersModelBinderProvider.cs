using Microsoft.AspNetCore.Mvc.ModelBinding;
using Presentation.Modelbinding.Binder;
using Shared.DataFilter.Presentation;

namespace Presentation.Modelbinding.Provider.HttpQuery
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
