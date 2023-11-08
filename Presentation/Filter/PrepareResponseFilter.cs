using Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Shared.ApiDataTransferObject;
using Shared.ApiDataTransferObject.Object;

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
                if(currentResult is Result result)
                {
                    var currentResultType = currentResult.GetType();

                    var apiResponseTypeUnspecificGenericType = typeof(ApiDataTransferObject<>);
                    var genericArgs = currentResultType.GetGenericArguments();
                    var apiResponseTypeGeneric = apiResponseTypeUnspecificGenericType.MakeGenericType(genericArgs);
                    var apiResponseTypeGenericInstance = Activator.CreateInstance(apiResponseTypeGeneric);
                    var createMethode = apiResponseTypeGeneric.GetMethods().Where(x => x.Name == "Create" && x.GetParameters().First().Name == "result").First();

                    var paginationBase = new PaginationBase(result.Meta);

                    var responseFromMethod = createMethode.Invoke(apiResponseTypeGenericInstance, new object[] { currentResult, paginationBase, null });
                    var hasErrorsProp = responseFromMethod.GetType().GetProperties().Where(x => x.Name == "HasErrors").First();
                    bool hasErrorsPropValue = (bool)hasErrorsProp.GetValue(responseFromMethod);

                    context.Result = !hasErrorsPropValue ? new OkObjectResult(responseFromMethod) : new BadRequestObjectResult(responseFromMethod);
                }
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {

        }


    }
}
