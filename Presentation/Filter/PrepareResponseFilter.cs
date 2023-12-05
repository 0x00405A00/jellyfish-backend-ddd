using Domain.Primitives;
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

                if (currentResult is IValidationResult result)
                {
                    HandleValidationResult(context, result);
                }
                else if (currentResult is Result result2)
                {
                    HandleDefaultResult(context, result2);
                }
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {

        }

        #region Methods

        private void HandleValidationResult(ActionExecutedContext context, IValidationResult result)
        {
            var apiResponseTypeGeneric = CreateApiResponseTypeGeneric(result.GetType());

            var createMethod = apiResponseTypeGeneric.GetMethod("ValidationResult");
            var responseFromMethod = createMethod.Invoke(Activator.CreateInstance(apiResponseTypeGeneric), new object[] { result });

            HandleApiResponse(context, responseFromMethod);
        }

        private void HandleDefaultResult(ActionExecutedContext context, Result result)
        {
            var apiResponseTypeGeneric = CreateApiResponseTypeGeneric(result.GetType());

            var createMethod = apiResponseTypeGeneric.GetMethods()
                .FirstOrDefault(x => x.Name == "Create" && x.GetParameters().First().Name == "result");

            if (createMethod != null)
            {
                var paginationBase = new PaginationBase(result.Meta);
                var responseFromMethod = createMethod.Invoke(Activator.CreateInstance(apiResponseTypeGeneric), new object[] { result, paginationBase, null });

                HandleApiResponse(context, responseFromMethod);
            }
        }

        private Type CreateApiResponseTypeGeneric(Type currentResultType)
        {
            var apiResponseTypeUnspecificGenericType = typeof(ApiDataTransferObject<>);
            var genericArgs = currentResultType.GetGenericArguments();
            return apiResponseTypeUnspecificGenericType.MakeGenericType(genericArgs);
        }

        private void HandleApiResponse(ActionExecutedContext context, object response)
        {
            var hasErrorsProp = response.GetType().GetProperty("HasErrors");
            var propValue = hasErrorsProp?.GetValue(response);
            bool hasErrorsPropValue = (bool)(propValue != null ? false : propValue);

            context.Result = !hasErrorsPropValue
                ? new OkObjectResult(response)
                : new BadRequestObjectResult(response);
        }
        #endregion

    }
}
