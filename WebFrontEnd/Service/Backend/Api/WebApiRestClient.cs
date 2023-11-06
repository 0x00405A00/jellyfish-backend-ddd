using RestSharp;
using Shared.ApiDataTransferObject;
using Shared.DataTransferObject;
using System.Text.Json;

namespace WebFrontEnd.Service.Backend.Api
{
    public class WebApiRestClient : AbstractRestClient,IDisposable
    {
        private bool _isInit;
        public AuthDTO CurrentWebApiSession = null;
        public string BaseUrl { get; set; }
        public string LoginSessionEndpoint { get; private set; }
        public string LogoutSessionEndpoint { get; private set; }
        public string RefreshSessionEndpoint { get; private set; }
        public string ValidateSessionEndpoint { get; private set; }
        public string ConnectionTestEndpoint { get; private set; }
        public string RegistrationActivateEndpoint { get; private set; }
        public bool RefreshLogin { get; private set; }
        public static uint MaxRequestRetries { get; private set; } = 3;
        public string User { get; private set; }
        public string Password { get; private set; }
        public string RegisterEndpoint { get; private set; }
        public string PasswordResetRequestDTOEndpoint { get; private set; }
        public string PasswordResetDataTransferModelEndpoint { get; private set; }
        private bool disposedValue;

        public WebApiRestClient(IConfiguration configuration)
        {
            var infrastructureApiSection = configuration.GetSection("Infrastructure:Api");
            string baseUrl = infrastructureApiSection.GetValue<string>("BaseUrl");
            string loginSessionEndpoint = infrastructureApiSection.GetValue<string>("LoginEndpoint");
            string logoutSessionEndpoint = infrastructureApiSection.GetValue<string>("LogoutEndpoint");
            string validateSessionEndpoint = infrastructureApiSection.GetValue<string>("ValidateEndpoint");
            string refreshSessionEndpoint = infrastructureApiSection.GetValue<string>("RefreshSessionEndpoint");
            string connectionTestEndpoint = infrastructureApiSection.GetValue<string>("ConnectionTestEndpoint");
            string passwordResetEndpoint = infrastructureApiSection.GetValue<string>("PasswordResetEndpoint");
            string passwordResetRequestEndpoint = infrastructureApiSection.GetValue<string>("PasswordResetRequestEndpoint");
            Init(
                baseUrl,
                loginSessionEndpoint,
                logoutSessionEndpoint,
                validateSessionEndpoint,
                refreshSessionEndpoint,
                connectionTestEndpoint,
                passwordResetEndpoint,
                passwordResetRequestEndpoint);
        }
        public void Init(
            string baseUrl,
            string loginSessionEndpoint,
            string logoutSessionEndpoint,
            string validateSessionEndpoint,
            string refreshSessionEndpoint,
            string connectionTestEndpoint,
                string passwordResetEndpoint,
                string passwordResetRequestEndpoint)
        {
            BaseUrl = baseUrl;
            LoginSessionEndpoint = BuildUrl(loginSessionEndpoint);
            LogoutSessionEndpoint = BuildUrl(logoutSessionEndpoint);
            RefreshSessionEndpoint = BuildUrl(refreshSessionEndpoint);
            ValidateSessionEndpoint = BuildUrl(validateSessionEndpoint);
            ConnectionTestEndpoint = BuildUrl(connectionTestEndpoint);
            PasswordResetRequestDTOEndpoint = BuildUrl(passwordResetRequestEndpoint);
            PasswordResetDataTransferModelEndpoint = BuildUrl(passwordResetEndpoint);
            _isInit = true;
        }
        private void SetCredentials(string user, string password, bool sessionAutoRefresh = true)
        {
            User = user;
            Password = password;
            RefreshLogin = sessionAutoRefresh;
        }
        public string BuildUrl(string endPoint)
        {
            if(BaseUrl == null)
            {
                throw new ArgumentNullException();
            }
            return BaseUrl + endPoint;
        }
        public bool IsInit
        {
            get { return _isInit; }
        }


        public virtual async Task<AuthDTO> Authentificate(string userName, string password, CancellationToken cancellationToken)
        {
            if (!IsInit)
            {
                throw new InvalidOperationException("please initialize the handler correctly via method: " + nameof(Init) + "");
            }
            SetCredentials(userName, password);
            var postData = new { email = userName, password };
            RestResponse resp = await Request<AuthDTO>(LoginSessionEndpoint, Method.Post, cancellationToken, postData, null, null, true);
            if (resp != null)
            {
                AuthDTO response;
                response = JsonSerializer.Deserialize<AuthDTO>(resp.Content);
                return response;
            }
            return null;
        }
        public async Task<WebApiHttpRequestResponseModel<UserDTO>> Activate(string base64Token, UserActivationDataTransferModel userActivationDataTransferModel, CancellationToken cancellationToken)
        {
            if (!IsInit)
            {
                throw new InvalidOperationException("please initialize the handler correctly via method: " + nameof(Init) + "");
            }
            var resp = await Request<UserDTO, object>(RegistrationActivateEndpoint + "/" + base64Token, Method.Post, cancellationToken, userActivationDataTransferModel, null, null, true);
            if (resp != null)
            {
                return resp;
            }
            return null;
        }
        public async Task<WebApiHttpRequestResponseModel<RegisterUserDTO>> Register(RegisterUserDTO registerDataTransferModel, CancellationToken cancellationToken)
        {
            if (!IsInit)
            {
                throw new InvalidOperationException("please initialize the handler correctly via method: " + nameof(Init) + "");
            }
            var resp = await Request<RegisterUserDTO, object>(RegisterEndpoint, Method.Post, cancellationToken, registerDataTransferModel, null, null, true);
            if (resp != null)
            {
                return resp;
            }
            return null;
        }
        /// <summary>
        /// Sends the confirmation secure code from users mail to backend to confirm the password request action
        /// </summary>
        /// <param name="passwordResetRequestDataTransferModel"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<WebApiHttpRequestResponseModel<PasswordResetRequestDTO>> ResetPasswordRequest(PasswordResetRequestDTO passwordResetRequestDTO, CancellationToken cancellationToken)
        {
            if (!IsInit)
            {
                throw new InvalidOperationException("please initialize the handler correctly via method: " + nameof(Init) + "");
            }
            var resp = await Request<PasswordResetRequestDTO, object>(PasswordResetRequestDTOEndpoint, Method.Post, cancellationToken, passwordResetRequestDTO, null, null, true);
            if (resp != null)
            {
                return resp;
            }
            return null;
        }
        /// <summary>
        /// Reset the password, previously the password reset must be requested and confirmed by secure code 
        /// </summary>
        /// <param name="passwordResetDataTransferModel"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<WebApiHttpRequestResponseModel<PasswordResetDataTransferModel>> ResetPassword(PasswordResetDataTransferModel passwordResetDataTransferModel, CancellationToken cancellationToken)
        {
            if (!IsInit)
            {
                throw new InvalidOperationException("please initialize the handler correctly via method: " + nameof(Init) + "");
            }
            
            string url = PasswordResetDataTransferModelEndpoint + passwordResetDataTransferModel.Base64Token;
            var resp = await Request<PasswordResetDataTransferModel, object>(url, Method.Post, cancellationToken, passwordResetDataTransferModel, null, null, true);
            if (resp != null)
            {
                return resp;
            }
            return null;
        }
        public string BuildRefreshTokenUrl()
        {
            if (!IsInit)
            {
                throw new InvalidOperationException("please initialize the handler correctly via method: " + nameof(Init) + "");
            }
            return RefreshSessionEndpoint + CurrentWebApiSession.RefreshToken;
        }
        public async Task<AuthDTO> RefreshToken(CancellationToken cancellationToken)
        {
            if (!IsInit)
            {
                throw new InvalidOperationException("please initialize the handler correctly via method: " + nameof(Init) + "");
            }
            if (string.IsNullOrEmpty(User) || string.IsNullOrEmpty(Password))
            {
                return null;
            }
            string refreshTokenUrl = BuildRefreshTokenUrl();
            var resp = await Request<AuthDTO, object>(RefreshSessionEndpoint, Method.Post, cancellationToken, null, null, null, true);
            if (resp != null)
            {
                var d = resp.ApiResponseDeserialized;
                CurrentWebApiSession = d;
                return d;
            }
            return null;
        }
        public async Task<bool> ConnectionTest(CancellationToken cancellationToken)
        {
            if (!IsInit)
            {
                throw new InvalidOperationException("please initialize the handler correctly via method: " + nameof(Init) + "");
            }


            var resp = await Request<object>(ConnectionTestEndpoint, Method.Get, cancellationToken, null, null, null, true);
            if (resp != null)
            {

                return resp.IsSuccessStatusCode;
            }
            return false;
        }

        public async Task<System.Net.HttpStatusCode> Logout(CancellationToken cancellationToken)
        {
            if (!IsInit)
            {
                throw new InvalidOperationException("please initialize the handler correctly via method: " + nameof(Init) + "");
            }
            if (CurrentWebApiSession != null && !CurrentWebApiSession.IsTokenExpired && !CurrentWebApiSession.IsRefreshTokenExpired)
            {
                var headers = new List<KeyValuePair<string, string>>();

                var requ = CreateRequest(LogoutSessionEndpoint, Method.Post, ContentType.Json, null, null, headers);
                var resp = await Send(requ, cancellationToken);
                return resp.StatusCode;
            }
            return System.Net.HttpStatusCode.Forbidden;
        }
        public async Task<RestResponse> Request<T2>(string url, Method method, CancellationToken cancellationToken, object bodyObject = null, List<KeyValuePair<string, string>> query = null, List<KeyValuePair<string, string>> headers = null, bool donttryagain = true)
        {
            if (!IsInit)
            {
                throw new InvalidOperationException("please initialize the handler correctly via method: " + nameof(Init) + "");
            }
            string body = null;
            if (headers == null)
            {
                headers = new List<KeyValuePair<string, string>>();
            }
            if (bodyObject != null)
            {
                body = JsonSerializer.Serialize(bodyObject);

            }
            int retries = 0;
            bool reauth = false;
            RestResponse response = null;
            do
            {
                url = url.ToLower().StartsWith("http") ? url : BuildUrl(url);
                var requ = CreateRequest(url, method, ContentType.Json, body, query, headers);
                response = await Send(requ, cancellationToken);
                if (!response.IsSuccessStatusCode)
                {
                    if (RefreshLogin)
                    {
                        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                        {
                            if (!reauth)
                            {
                                var responseAuth = await Authentificate(User, Password, cancellationToken);
                                if (responseAuth != null)
                                {
                                    CurrentWebApiSession = responseAuth;
                                    headers.Add(new KeyValuePair<string, string>("Authorization", CurrentWebApiSession.Token));


                                }
                                reauth = true;
                            }
                            else//da bereits die erste auth fehlgeschlagen ist werden die nächsten folgen d.h. do-while abort
                            {
                                donttryagain = true;
                            }
                        }

                    }
                    if (!reauth)
                    {
                        if (!response.IsSuccessStatusCode && response.StatusCode != System.Net.HttpStatusCode.Unauthorized)
                            donttryagain = true;
                    }

                    retries++;
                }
                else
                {
                    donttryagain = true;
                }
            }
            while (retries < MaxRequestRetries && !donttryagain && !response.IsSuccessStatusCode);
            return response;
        }
        public async Task<WebApiHttpRequestResponseModel<T1>> Request<T1, T2>(
            string url,
            Method method,
            CancellationToken cancellationToken,
            T2 bodyObject = default,
            List<KeyValuePair<string, string>> query = null,
            List<KeyValuePair<string, string>> headers = null,
            bool donttryagain = true)
        {
            WebApiHttpRequestResponseModel<T1> responseModel = new WebApiHttpRequestResponseModel<T1>();
            var resp = await Request<T2>(url, method, cancellationToken, bodyObject, query, headers, donttryagain);
            responseModel.DefaultResponse = resp;
            if (resp != null && resp.Content != null && !string.IsNullOrEmpty(resp.Content))
            {
                try
                {
                    string responseJson = resp.Content;
                    var apiResponseModel = JsonSerializer.Deserialize<T1>(responseJson);
                    responseModel.ApiResponseDeserialized = apiResponseModel;
                }
                catch (Exception ex)
                {

                }
            }
            return responseModel;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    CurrentWebApiSession = null;
                    BaseUrl = null;
                    LoginSessionEndpoint = null;
                    LogoutSessionEndpoint = null;
                    RefreshSessionEndpoint = null;
                    ValidateSessionEndpoint = null;
                    ConnectionTestEndpoint = null;
                    RegistrationActivateEndpoint = null;
                    RefreshLogin = false;
                    MaxRequestRetries = 3;
                    User = null;
                    Password = null;
                    RegisterEndpoint = null;
                    PasswordResetRequestDTOEndpoint = null;
                    PasswordResetDataTransferModelEndpoint = null;
                }

                // TODO: Nicht verwaltete Ressourcen (nicht verwaltete Objekte) freigeben und Finalizer überschreiben
                // TODO: Große Felder auf NULL setzen
                disposedValue = true;
            }
        }

        // // TODO: Finalizer nur überschreiben, wenn "Dispose(bool disposing)" Code für die Freigabe nicht verwalteter Ressourcen enthält
        // ~CustomAuthentificationStateProvider()
        // {
        //     // Ändern Sie diesen Code nicht. Fügen Sie Bereinigungscode in der Methode "Dispose(bool disposing)" ein.
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Ändern Sie diesen Code nicht. Fügen Sie Bereinigungscode in der Methode "Dispose(bool disposing)" ein.
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
