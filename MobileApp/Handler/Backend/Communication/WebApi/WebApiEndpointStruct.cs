using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.Handler.Backend.Communication.WebApi
{
    public static class WebApiEndpointStruct
    {
        public const string LoginSessionEndpoint = "authentification/login";
        public const string LogoutSessionEndpoint = "authentification/logout";
        public const string RefreshSessionEndpoint = "authentification/refresh/";
        public const string ValidateSessionEndpoint = "authentification/validate";
        public const string ConnectionTestEndpoint = "authentification/connection";
        public const string RegisterEndpoint = "user/register";
        public const string RegisterActivationEndpoint = "user/activation";
        public const string UserPasswordResetRequestEndpoint = "user/password/reset/request";
        public const string UserPasswordResetEndpoint = "user/password/reset/";
        public const string UserPasswordCodeConfirmationEndpoint = "user/password/reset/confirmation";

        static WebApiEndpointStruct()
        {
            
        }
    }
}
