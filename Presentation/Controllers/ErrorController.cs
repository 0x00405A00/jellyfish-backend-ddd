using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Shared.CustomException;
using Shared.ApiDataTransferObject;

namespace Presentation.Controllers
{
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {
        public ErrorController() { 
        
        }

        [Route("error")]
        public async Task<ActionResult> Get(bool debug=false)
        {
            //INFO: Exceptions cant be serialized by asp, common error comes over by trying: Serialization and deserialization of 'System.Type' instances are not supported and should be avoided since they can lead to security issues
            IExceptionHandlerFeature context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            object exception = context.Error;
            System.Exception exceptionObject = (System.Exception)exception;
            string message = exceptionObject.Message;
            string details = debug ? exceptionObject.StackTrace : null;
            ObjectResult result =null;
            if (exception is ModelBindingFailedException modelBindingFailedException)
            {
                var error = new Shared.ApiDataTransferObject.Object.ApiError(modelBindingFailedException.Title, modelBindingFailedException.Message, modelBindingFailedException.Details, modelBindingFailedException.Code);
                ApiResponse<object> response = ApiResponse<object>.Create(new object(),null,new List<Shared.ApiDataTransferObject.Object.ApiError> { error });
                result = new UnprocessableEntityObjectResult(response);
            }
            else
            {
                var error = new Shared.ApiDataTransferObject.Object.ApiError("internal server error", message, details, 500);
                ApiResponse<object> response = ApiResponse<object>.Create(new object(), null, new List<Shared.ApiDataTransferObject.Object.ApiError> { error });
                object debugRespObj = exceptionObject.StackTrace;
                result = new ObjectResult(response);
                result.StatusCode = 500;
            }
            return result;
        }
        [Route("error-debug")]
        public async Task<ActionResult> Error()
        {
            return await Get(true);
        }
    }
}
