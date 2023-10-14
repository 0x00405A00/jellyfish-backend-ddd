using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Presentation.Controllers
{
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {
        public ErrorController() { 
        
        }

        [Route("error")]
        public async Task<ActionResult> Get()
        {
            //INFO: Exceptions cant be serialized by asp, common error comes over by trying: Serialization and deserialization of 'System.Type' instances are not supported and should be avoided since they can lead to security issues
            IExceptionHandlerFeature context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            object exception = context.Error;
            HttpStatusCode code = HttpStatusCode.InternalServerError;
            string preDefinedMessage = "Internal Server Error";
            string debugMsg = null;
            Exception exception1 = (Exception)exception;
            object debugRespObj = exception1.StackTrace;
            var result = new ObjectResult(exception1.Message);
            result.StatusCode = 500;
            return result;
        }
        [Route("error-debug")]
        public async Task<ActionResult> Error()
        {
            return await Get();
        }
    }
}
