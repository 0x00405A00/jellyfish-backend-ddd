using Microsoft.AspNetCore.Mvc.ModelBinding;
using Presentation.Modelbinding.Binder;
using Shared.DataFilter.Presentation;
using Shared.DataTransferObject;

namespace Presentation.Modelbinding.Provider
{
    public class BodyModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context.Metadata.ModelType.GetInterfaces().Contains(typeof(IDataTransferObject)) 
                || (context.Metadata.ModelType ==typeof(List<>) &&context.Metadata.ModelType.GetGenericArguments().Where(x=>x.GetInterfaces().Contains(typeof(IDataTransferObject))).Any()))
            {
                return new ApiDataTransferObjectModelBinder();
            }
            else if (context.Metadata.ModelType == typeof(SearchParamsBody))
            {
                return new SearchParamBodyModelBinder();
            }
            return null;
        }
    }
}
