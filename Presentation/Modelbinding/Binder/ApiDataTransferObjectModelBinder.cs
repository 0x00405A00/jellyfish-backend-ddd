using Microsoft.AspNetCore.Mvc.ModelBinding;
using Shared.ApiDataTransferObject;
using Shared.ApiDataTransferObject.Object;
using Shared.CustomException;
using Shared.DataTransferObject;
using System.Reflection;
using System.Text.Json;

namespace Presentation.Modelbinding.Binder
{
    public class ApiDataTransferObjectModelBinder : IModelBinder
    {
        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
                throw new ArgumentNullException(nameof(bindingContext));

            using (var reader = new StreamReader(bindingContext.HttpContext.Request.Body))
            {
                try
                {

                    var jsonOpt = new JsonSerializerOptions { MaxDepth = 12, WriteIndented = true };
                    var body = await reader.ReadToEndAsync();
                    if(String.IsNullOrEmpty(body))
                    {
                        throw new ModelBindingFailedException($"body is null");
                    }
                    var data = JsonSerializer.Deserialize<ApiDataTransferObject<object>>(body, jsonOpt);
                    if(data==null||String.IsNullOrEmpty(data.Data.Type))
                    {

                        throw new ModelBindingFailedException($"{(nameof(ApiData<object>.Type))} is null or empty");
                    }
                    var types = Assembly.GetAssembly(typeof(Shared.DataTransferObject.IDataTransferObject))
                                             .GetTypes();
                    Type modelType = null;
                    try
                    {
                        modelType = types
                                             .Where(x => x.IsPublic && x.GetInterfaces().Contains(typeof(IDataTransferObject)) && x.Name.ToLower() == data.Data.Type.ToLower())
                                             .First();
                    }
                    catch(Exception ex)
                    {
                        throw new ModelBindingFailedException($"given {(nameof(ApiData<object>.Type))} is not found");
                    }
                    if(modelType == null)
                    {

                        throw new ModelBindingFailedException($"given {(nameof(ApiData<object>.Type))} is not found" );
                    }
                    var targetModelTypeFromAction = bindingContext.ModelMetadata.ModelType;
                    bool targetModelIsCollection = ((targetModelTypeFromAction.IsGenericType && (targetModelTypeFromAction.GetGenericTypeDefinition() == typeof(List<>))));

                    Type targetModel = targetModelIsCollection ? typeof(List<>).MakeGenericType(modelType) : modelType;

                    Type returnType = typeof(ApiDataTransferObject<>).MakeGenericType(targetModel);
                    object finalData =null;
                    try
                    {
                        finalData=JsonSerializer.Deserialize(body, returnType, jsonOpt);
                    }
                    catch(Exception ex)
                    {
                        throw new ModelBindingFailedException($"json deserializing not possible");
                    }

                    var dataProp = finalData.GetType().GetProperty(nameof(ApiDataTransferObject<object>.Data));
                    var dataPropValue = dataProp.GetValue(finalData);
                    var dataValueProp = dataPropValue.GetType().GetProperty(nameof(ApiData<object>.Value));
                    var dataValuePropValue = dataValueProp.GetValue(dataPropValue);
                    object model = dataValuePropValue;
                    

                    bindingContext.Result = ModelBindingResult.Success(model);
                }
                catch (Exception ex)
                {
                    throw;
                }

            }
        }
    }
}
