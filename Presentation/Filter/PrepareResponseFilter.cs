using Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Shared.ApiDataTransferObject;

namespace Presentation.Filter
{
    public class PrepareResponseFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            var currentResponse = context.Result;
            if (currentResponse is ObjectResult)
            {

                var currentResult = ((ObjectResult)currentResponse).Value;
                if(currentResult is Result)
                {
                    var currentResultType = currentResult.GetType();

                    var apiResponseTypeUnspecificGenericType = typeof(ApiResponse<>);
                    var genericArgs = currentResultType.GetGenericArguments();
                    var apiResponseTypeGeneric = apiResponseTypeUnspecificGenericType.MakeGenericType(genericArgs);
                    var apiResponseTypeGenericInstance = Activator.CreateInstance(apiResponseTypeGeneric);
                    var createMethode = apiResponseTypeGeneric.GetMethods().Where(x => x.Name == "Create" && x.GetParameters().First().Name == "result").First();

                    var responseFromMethod = createMethode.Invoke(apiResponseTypeGenericInstance, new object[] { currentResult, null });
                    var data = responseFromMethod.GetType().GetProperties().Where(x => x.Name == "HasErrors").First();
                    bool hasErrors = (bool)data.GetValue(responseFromMethod);
                    context.Result = !hasErrors ? new OkObjectResult(responseFromMethod) : new BadRequestObjectResult(responseFromMethod);
                }
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {

        }


    }
}
