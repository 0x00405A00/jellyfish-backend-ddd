using Microsoft.AspNetCore.Mvc;
using Shared.ApiDataTransferObject ;
using System.Net;

namespace Presentation.Extension
{
    public static class JsonApiResultExtension
    {
        private static ObjectResult JsonApiError(string title, string message, string details, HttpStatusCode code)
        {
            int codeInt = (int)code;
            if (!IsClientError(code) &&!IsServerError(code))
            {
                throw new InvalidOperationException($"{codeInt} is not a valid http response code. (code< 400 && code> 599)");
            }
            ApiDataTransferObject<object> response = ApiDataTransferObject<object>.Error(new List<Shared.ApiDataTransferObject.Object.ApiError> { new Shared.ApiDataTransferObject.Object.ApiError(title, message, details, codeInt) });


            ObjectResult objectResult = new ObjectResult(response);
            objectResult.StatusCode = codeInt;
            return objectResult;
        }
        public static bool IsSuccessCode(this HttpStatusCode code)
        {
            int codeInt = (int)code;
            return (codeInt >= 200 && codeInt < 300);
        }
        public static bool IsInfoCode(this HttpStatusCode code)
        {
            int codeInt = (int)code;
            return (codeInt >= 100 && codeInt < 200);
        }
        public static bool IsRedirectCode(this HttpStatusCode code)
        {
            int codeInt = (int)code;
            return (codeInt >= 300 && codeInt < 400);
        }
        public static bool IsClientError(this HttpStatusCode code)
        {
            int codeInt = (int)code;
            return (codeInt >= 400 && codeInt < 500);
        }
        public static bool IsServerError(this HttpStatusCode code)
        {
            int codeInt = (int)code;
            return (codeInt >= 500 && codeInt < 600);
        }
        public struct ConstErrorMessags
        {
            public struct Client
            {
                public const string BadRequest = "Bad Request";
                public const string Unauthorized = "Unauthorized";
                public const string PaymentRequired = "Payment Required";
                public const string Forbidden = "Forbidden";
                public const string NotFound = "Not Found";
                public const string MethodNotAllowed = "Method Not Allowed";
                public const string NotAcceptable = "Not Acceptable";
                public const string ProxyAuthenticationRequired = "Proxy Authentication Required";
                public const string RequestTimeout = "Request Timeout";
                public const string Conflict = "Conflict";
                public const string Gone = "Gone";
                public const string LengthRequired = "Length Required";
                public const string PreconditionFailed = "Precondition Failed";
                public const string PayloadTooLarge = "Payload Too Large";
                public const string UnsupportedMediaType = "Unsupported Media Type";
                public const string RequestedRangeNotSatisfiable = "Requested Range Not Satisfiable";
                public const string ExpectationFailed = "Expectation Failed";
            }

            public struct Server
            {
                public const string InternalServerError = "Internal Server Error";
                public const string NotImplemented = "Not Implemented";
                public const string BadGateway = "Bad Gateway";
                public const string ServiceUnavailable = "Service Unavailable";
                public const string GatewayTimeout = "Gateway Timeout";
                public const string HTTPVersionNotSupported = "HTTP Version Not Supported";
                public const string LoopDetected = "LoopDetected";
            }
        }
        public static ObjectResult ErrorByHttpStatusCode(string message, string detail, HttpStatusCode httpStatusCode)
        {
            ObjectResult objectResult = null;
            switch (httpStatusCode)
            {
                case HttpStatusCode.BadRequest:
                    objectResult = Client.BadRequest(message, detail);
                    break;

                case HttpStatusCode.Unauthorized:
                    objectResult = Client.Unauthorized(message, detail);
                    break;

                case HttpStatusCode.PaymentRequired:
                    objectResult = Client.PaymentRequired(message, detail);
                    break;

                case HttpStatusCode.Forbidden:
                    objectResult = Client.Forbidden(message, detail);
                    break;

                case HttpStatusCode.NotFound:
                    objectResult = Client.NotFound(message, detail);
                    break;

                case HttpStatusCode.MethodNotAllowed:
                    objectResult = Client.MethodNotAllowed(message, detail);
                    break;

                case HttpStatusCode.NotAcceptable:
                    objectResult = Client.NotAcceptable(message, detail);
                    break;

                case HttpStatusCode.ProxyAuthenticationRequired:
                    objectResult = Client.ProxyAuthenticationRequired(message, detail);
                    break;

                case HttpStatusCode.RequestTimeout:
                    objectResult = Client.RequestTimeout(message, detail);
                    break;

                case HttpStatusCode.Conflict:
                    objectResult = Client.Conflict(message, detail);
                    break;

                case HttpStatusCode.Gone:
                    objectResult = Client.Gone(message, detail);
                    break;

                case HttpStatusCode.LengthRequired:
                    objectResult = Client.LengthRequired(message, detail);
                    break;

                case HttpStatusCode.PreconditionFailed:
                    objectResult = Client.PreconditionFailed(message, detail);
                    break;

                case HttpStatusCode.RequestEntityTooLarge:
                    objectResult = Client.PayloadTooLarge(message, detail);
                    break;

                case HttpStatusCode.UnsupportedMediaType:
                    objectResult = Client.UnsupportedMediaType(message, detail);
                    break;

                case HttpStatusCode.RequestedRangeNotSatisfiable:
                    objectResult = Client.RequestedRangeNotSatisfiable(message, detail);
                    break;

                case HttpStatusCode.ExpectationFailed:
                    objectResult = Client.ExpectationFailed(message, detail);
                    break;

                case HttpStatusCode.InternalServerError:
                    objectResult = Server.InternalServerError(message, detail);
                    break;

                case HttpStatusCode.NotImplemented:
                    objectResult = Server.NotImplemented(message, detail);
                    break;

                case HttpStatusCode.BadGateway:
                    objectResult = Server.BadGateway(message, detail);
                    break;

                case HttpStatusCode.ServiceUnavailable:
                    objectResult = Server.ServiceUnavailable(message, detail);
                    break;

                case HttpStatusCode.GatewayTimeout:
                    objectResult = Server.GatewayTimeout(message, detail);
                    break;

                case HttpStatusCode.HttpVersionNotSupported:
                    objectResult = Server.HTTPVersionNotSupported(message, detail);
                    break;

                case HttpStatusCode.LoopDetected:
                    objectResult = Server.LoopDetected(message, detail);
                    break;


                default:
                    objectResult = Client.BadRequest(message, detail);
                    break;
            }
            return objectResult;
        }
        public static class Client
        {


            public static ObjectResult BadRequest(string message, string details, string title = ConstErrorMessags.Client.BadRequest)
            {
                return JsonApiError(title, message, details, HttpStatusCode.BadRequest);
            }

            public static ObjectResult Unauthorized(string message, string details, string title = ConstErrorMessags.Client.Unauthorized)
            {
                return JsonApiError(title, message, details, HttpStatusCode.Unauthorized);
            }

            public static ObjectResult PaymentRequired(string message, string details, string title = ConstErrorMessags.Client.PaymentRequired)
            {
                return JsonApiError(title, message, details, HttpStatusCode.PaymentRequired);
            }

            public static ObjectResult Forbidden(string message, string details, string title = ConstErrorMessags.Client.Forbidden)
            {
                return JsonApiError(title, message, details, HttpStatusCode.Forbidden);
            }

            public static ObjectResult NotFound(string message, string details, string title = ConstErrorMessags.Client.NotFound)
            {
                return JsonApiError(title, message, details, HttpStatusCode.NotFound);
            }

            public static ObjectResult MethodNotAllowed(string message, string details, string title = ConstErrorMessags.Client.MethodNotAllowed)
            {
                return JsonApiError(title, message, details, HttpStatusCode.MethodNotAllowed);
            }

            public static ObjectResult NotAcceptable(string message, string details, string title = ConstErrorMessags.Client.NotAcceptable)
            {
                return JsonApiError(title, message, details, HttpStatusCode.NotAcceptable);
            }

            public static ObjectResult ProxyAuthenticationRequired(string message, string details, string title = ConstErrorMessags.Client.ProxyAuthenticationRequired)
            {
                return JsonApiError(title, message, details, HttpStatusCode.ProxyAuthenticationRequired);
            }

            public static ObjectResult RequestTimeout(string message, string details, string title = ConstErrorMessags.Client.RequestTimeout)
            {
                return JsonApiError(title, message, details, HttpStatusCode.RequestTimeout);
            }

            public static ObjectResult Conflict(string message, string details, string title = ConstErrorMessags.Client.Conflict)
            {
                return JsonApiError(title, message, details, HttpStatusCode.Conflict);
            }

            public static ObjectResult Gone(string message, string details, string title = ConstErrorMessags.Client.Gone)
            {
                return JsonApiError(title, message, details, HttpStatusCode.Gone);
            }

            public static ObjectResult LengthRequired(string message, string details, string title = ConstErrorMessags.Client.LengthRequired)
            {
                return JsonApiError(title, message, details, HttpStatusCode.LengthRequired);
            }

            public static ObjectResult PreconditionFailed(string message, string details, string title = ConstErrorMessags.Client.PreconditionFailed)
            {
                return JsonApiError(title, message, details, HttpStatusCode.PreconditionFailed);
            }

            public static ObjectResult PayloadTooLarge(string message, string details, string title = ConstErrorMessags.Client.PayloadTooLarge)
            {
                return JsonApiError(title, message, details, HttpStatusCode.RequestEntityTooLarge);
            }

            public static ObjectResult UnsupportedMediaType(string message, string details, string title = ConstErrorMessags.Client.UnsupportedMediaType)
            {
                return JsonApiError(title, message, details, HttpStatusCode.UnsupportedMediaType);
            }

            public static ObjectResult RequestedRangeNotSatisfiable(string message, string details, string title = ConstErrorMessags.Client.RequestedRangeNotSatisfiable)
            {
                return JsonApiError(title, message, details, HttpStatusCode.RequestedRangeNotSatisfiable);
            }

            public static ObjectResult ExpectationFailed(string message, string details, string title = ConstErrorMessags.Client.ExpectationFailed)
            {
                return JsonApiError(title, message, details, HttpStatusCode.ExpectationFailed);
            }


            public static ObjectResult NotImplemented(string message, string details, string title = ConstErrorMessags.Server.NotImplemented)
            {
                return JsonApiError(title, message, details, HttpStatusCode.NotImplemented);
            }


        }
        public static class Server
        {

            public static ObjectResult BadGateway(string message, string details, string title = ConstErrorMessags.Server.BadGateway)
            {
                return JsonApiError(title, message, details, HttpStatusCode.BadGateway);
            }
            public static ObjectResult InternalServerError(string message, string details, string title = ConstErrorMessags.Server.InternalServerError)
            {
                return JsonApiError(title, message, details, HttpStatusCode.InternalServerError);
            }
            public static ObjectResult ServiceUnavailable(string message, string details, string title = ConstErrorMessags.Server.ServiceUnavailable)
            {
                return JsonApiError(title, message, details, HttpStatusCode.ServiceUnavailable);
            }

            public static ObjectResult GatewayTimeout(string message, string details, string title = ConstErrorMessags.Server.GatewayTimeout)
            {
                return JsonApiError(title, message, details, HttpStatusCode.GatewayTimeout);
            }

            public static ObjectResult HTTPVersionNotSupported(string message, string details, string title = ConstErrorMessags.Server.HTTPVersionNotSupported)
            {
                return JsonApiError(title, message, details, HttpStatusCode.HttpVersionNotSupported);
            }

            public static ObjectResult NotImplemented(string message, string details, string title = ConstErrorMessags.Server.NotImplemented)
            {
                return JsonApiError(title, message, details, HttpStatusCode.NotImplemented);
            }

            public static ObjectResult LoopDetected(string message, string details, string title = ConstErrorMessags.Server.LoopDetected)
            {
                return JsonApiError(title, message, details, HttpStatusCode.LoopDetected);
            }

        }
    }
}
