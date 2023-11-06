using Domain.Primitives;
using System.Net;

namespace Shared.CustomException
{
    public class ModelBindingFailedException : BaseException
    {
        public const string ModelBindingFailedExceptionTitle = "unprocessable-entity";
        public const int ModelBindingFailedExceptionCode = (int)HttpStatusCode.UnprocessableEntity;

        public ModelBindingFailedException(string message, string details=null,string title = ModelBindingFailedExceptionTitle, int code= ModelBindingFailedExceptionCode) : base(title, message, details, code)
        {
        }
    }
}
