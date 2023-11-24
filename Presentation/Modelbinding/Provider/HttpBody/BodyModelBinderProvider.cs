using Microsoft.AspNetCore.Mvc.ModelBinding;
using Presentation.Modelbinding.Binder;
using Shared.DataFilter.Presentation;
using Shared.DataTransferObject;
using Shared.Reflection;

namespace Presentation.Modelbinding.Provider
{
    public class BodyModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            var type = context.Metadata.ModelType;
            var args = type.GetGenericArguments();


            if (context.Metadata.ModelType.GetInterfaces().Contains(typeof(IDataTransferObject)) 
                || (ListReflectionExtension. IsListAndGenericTypeImplementsT<IDataTransferObject>(type)))
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
