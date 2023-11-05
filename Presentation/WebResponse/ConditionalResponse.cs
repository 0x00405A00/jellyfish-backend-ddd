using Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using Shared.ApiDataTransferObject;

namespace Presentation.WebResponse
{
    public static class ConditionalResponse
    {
        public static IActionResult PrepareResponse<T>(this Result<T> response)
        {
            /*BadRequestObjectResult badRequestResult = null;
            OkObjectResult okRequestResult = null;
            var responseForPresentation = ApiResponse<T>.Create(response);

            if(responseForPresentation != null)
            {
                if(response.IsSuccess)
                {
                    okRequestResult = new OkObjectResult(responseForPresentation);
                    return okRequestResult;
                }

            }
            badRequestResult = new BadRequestObjectResult(responseForPresentation);
            return badRequestResult;*/
            return new ObjectResult(response);
        }
    }
}
