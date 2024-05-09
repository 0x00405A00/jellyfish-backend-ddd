using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration;
using RestSharp;
using Shared.ApiDataTransferObject;
using Shared.ApiDataTransferObject.Object;
using Shared.Authentification.Claims;
using Shared.Const;
using Shared.DataFilter.Presentation;
using Shared.DataTransferObject;
using Shared.DataTransferObject.Abstraction;
using Shared.Reflection;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using static Shared.Infrastructure.Backend.Api.JellyfishBackendApi;
using System;
using Shared.Logger;

namespace Shared.Infrastructure.Backend.Api
{

    public class WebApiRestClient : AbstractRestClient, IDisposable, IWebApiRestClient
    {
        private bool _isInit;
        private AuthDTO _currentWebApiSession;
        private Func<Task<AuthDTO>> _currentWebApiSessionGetFunc;
        
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
        private ICollection<ErrorOutputEventHandler> _errorOutputEventHandlers = new List<ErrorOutputEventHandler>();

        public WebApiRestClient(
            ILogger<WebApiRestClient> logger,
            IConfiguration configuration) : base(logger)
        {
            this.logger = logger;
            var infrastructureApiSection = configuration.GetSection("Infrastructure:Api");
            string baseUrl = infrastructureApiSection.GetValue<string>("BaseUrl");
            string loginSessionEndpoint = infrastructureApiSection.GetValue<string>("LoginEndpoint");
            string logoutSessionEndpoint = infrastructureApiSection.GetValue<string>("LogoutEndpoint");
            string registerEndpoint = infrastructureApiSection.GetValue<string>("RegisterEndpoint");
            string validateSessionEndpoint = infrastructureApiSection.GetValue<string>("ValidateEndpoint");
            string refreshSessionEndpoint = infrastructureApiSection.GetValue<string>("RefreshSessionEndpoint");
            string connectionTestEndpoint = infrastructureApiSection.GetValue<string>("ConnectionTestEndpoint");
            string passwordResetEndpoint = infrastructureApiSection.GetValue<string>("PasswordResetEndpoint");
            string passwordResetRequestEndpoint = infrastructureApiSection.GetValue<string>("PasswordResetRequestEndpoint");
            Init(
                baseUrl,
                loginSessionEndpoint,
                logoutSessionEndpoint,
                registerEndpoint,
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
            string registerEndpoint,
            string validateSessionEndpoint,
            string refreshSessionEndpoint,
            string connectionTestEndpoint,
                string passwordResetEndpoint,
                string passwordResetRequestEndpoint)
        {
            BaseUrl = baseUrl;
            LoginSessionEndpoint = BuildUrl(loginSessionEndpoint);
            LogoutSessionEndpoint = BuildUrl(logoutSessionEndpoint);
            RegisterEndpoint = BuildUrl(registerEndpoint);
            RefreshSessionEndpoint = BuildUrl(refreshSessionEndpoint);
            ValidateSessionEndpoint = BuildUrl(validateSessionEndpoint);
            ConnectionTestEndpoint = BuildUrl(connectionTestEndpoint);
            PasswordResetRequestDTOEndpoint = BuildUrl(passwordResetRequestEndpoint);
            PasswordResetDataTransferModelEndpoint = BuildUrl(passwordResetEndpoint);
            _isInit = true;
            logger.LogMethod("init");
        }
        public void AddErrorHandler(ErrorOutputEventHandler eventHandler)
        {
            logger.LogMethod($"add {nameof(ErrorOutputEventHandler)}: {nameof(eventHandler)}");
            _errorOutputEventHandlers.Add(eventHandler);
        }
        private void DispatchEventHandlers(JellyfishApiErrorEventArgs jellyfishApiErrorEventArgs)
        {
            if (_errorOutputEventHandlers.Any())
            {
                foreach (var eventHandler in _errorOutputEventHandlers)
                {
                    logger.LogMethod($"invoke evenhandler {nameof(ErrorOutputEventHandler)}: {nameof(eventHandler)}");
                    eventHandler.Invoke(this, jellyfishApiErrorEventArgs);
                }
            }
        }
        private void SetCredentials(string user, string password, bool sessionAutoRefresh = true)
        {
            User = user;
            Password = password;
            RefreshLogin = sessionAutoRefresh;
            logger.LogMethod($"set credentials for 'Authorization' header");
        }
        public async Task<UserDTO?> GetCurrentUser(AuthenticationState authenticationState, CancellationToken cancellationToken)
        {
            if (authenticationState is null)
                return null;
            if (authenticationState.User is null)
                return null;
            if (!authenticationState.User.Claims.Any())
                return null;
            var userUuid = authenticationState.User.Claims.GetClaims(x => x.Type == AuthorizationConst.Claims.ClaimTypeUserUuid)?.First();
            var userProfile = await this.GetUser(Guid.Parse(userUuid.Value), cancellationToken);
            return userProfile.IsSuccess ? userProfile.Data : null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">Request Body Type</typeparam>
        /// <typeparam name="T2">Response Type</typeparam>
        /// <param name="url">Target Url, when <see cref="BaseUrl"</see> is already set you can request the target endpoint via path directly without write out the url complete e.g. full write out: https://mytargeturl/mytargetendpoint/1, short variant when <see cref="BaseUrl"></see>/> is set></param>
        /// <param name="method">HTTP Method GET, POST, PUT, DELETE etc.</param>
        /// <param name="data">Body data></param>
        /// <param name="cancellationToken">To cancel the task</param>
        /// <param name="paginationBase">Pagination behaviour of backend, influent the backend behaviour by evaluate the result</param>
        /// <returns>Response Type <<see cref="T2"/></returns>
        public virtual async Task<JellyfishBackendApiResponse<T2>> TypedRequest<T, T2>(string url, RestSharp.Method method, T? data, CancellationToken cancellationToken, PaginationBase paginationBase = null)

        {
            logger.LogMethod($"start exec {nameof(TypedRequest)}");
            if (data is IDataTransferObject dataTransferObject || (ReflectionExtension.IsListAndGenericTypeImplementsT<IDataTransferObject>(typeof(T))))
            {
                logger.LogMethod($"body is {nameof(IDataTransferObject)} generic list with type");
                var body = ApiDataTransferObject<T>.Create(data, paginationBase);
                var response = await Request<ApiDataTransferObject<T2>, ApiDataTransferObject<T>>(url, method, cancellationToken, body);
                return JellyfishBackendApiResponse<T2>.CreateFromWebApiResponseModel(response);
            }
            else if (data is SearchParamsBody)
            {
                logger.LogMethod($"body is {nameof(SearchParamsBody)}");
                return await RequestI<T, T2>(url, method, data, cancellationToken);
            }
            return await RequestI<T, T2>(url, method, data, cancellationToken);
        }
        private async Task<JellyfishBackendApiResponse<T2>> RequestI<T, T2>(string url, RestSharp.Method method, T? data, CancellationToken cancellationToken)
        {

            T body = data;
            logger.LogMethod($"body data.ToString(): {body?.ToString()}");
            var response = await this.Request<ApiDataTransferObject<T2>, T>(url, method, cancellationToken, body);
            var webApiResponseModel = JellyfishBackendApiResponse<T2>.CreateFromWebApiResponseModel(response);
            if (webApiResponseModel != null && webApiResponseModel.HasErrors)
            {

                logger.LogMethod($"response has error data");
                if (_errorOutputEventHandlers.Any())
                {
                    logger.LogMethod($"start to dispatch error eventhandlers");
                    var eventArgs = new JellyfishApiErrorEventArgs(webApiResponseModel.DefaultResponse.DefaultResponse.StatusCode, webApiResponseModel.DefaultResponse.DefaultResponse.ResponseUri, webApiResponseModel.Errors);
                    DispatchEventHandlers(eventArgs);
                    logger.LogMethod($"dispatch completed");
                }
            }
            return webApiResponseModel;
        }
        
        public async Task<JellyfishBackendApiResponse<UserDTO>> Activate(string base64Token, UserActivationDataTransferModel userActivationDataTransferModel, CancellationToken cancellationToken)
        {
            string url = RegistrationActivateEndpoint + "/" + base64Token;
            var response = await this.TypedRequest<UserActivationDataTransferModel, UserDTO>(url, RestSharp.Method.Post, userActivationDataTransferModel, cancellationToken);
            return response;
        }
        public async Task<JellyfishBackendApiResponse<UserDTO>> Register(RegisterUserDTO registerDataTransferModel, CancellationToken cancellationToken)
        {
            var response = await this.TypedRequest<RegisterUserDTO, UserDTO>(RegisterEndpoint, RestSharp.Method.Post, registerDataTransferModel, cancellationToken);
            return response;
        }
        /// <summary>
        /// Sends the confirmation secure code from users mail to backend to confirm the password request action
        /// </summary>
        /// <param name="passwordResetRequestDataTransferModel"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<JellyfishBackendApiResponse<bool>> ResetPasswordRequest(PasswordResetRequestDTO passwordResetRequestDTO, CancellationToken cancellationToken)
        {
            var response = await this.TypedRequest<PasswordResetRequestDTO, bool>(PasswordResetRequestDTOEndpoint, RestSharp.Method.Post, passwordResetRequestDTO, cancellationToken);
            return response;
        }
        /// <summary>
        /// Reset the password, previously the password reset must be requested and confirmed by secure code 
        /// </summary>
        /// <param name="passwordResetDataTransferModel"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<JellyfishBackendApiResponse<bool>> ResetPassword(PasswordResetDataTransferModel passwordResetDataTransferModel, CancellationToken cancellationToken)
        {
            string url = PasswordResetDataTransferModelEndpoint + passwordResetDataTransferModel.Base64Token;

            var response = await this.TypedRequest<PasswordResetDataTransferModel, bool>(url, RestSharp.Method.Post, passwordResetDataTransferModel, cancellationToken);
            return response;
        }
        public async Task<JellyfishBackendApiResponse<UserDTO>> GetUser(Guid userId, CancellationToken cancellationToken)
        {

            var url = "/user/" + userId + "";
            var response = await this.TypedRequest<SearchParamsBody, UserDTO>(url, RestSharp.Method.Get, null, cancellationToken);
            return response;
        }
        public string BuildUrl(string endPoint)
        {
            if (BaseUrl == null)
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
            if (resp.IsSuccessStatusCode)
            {
                AuthDTO response = JsonSerializer.Deserialize<AuthDTO>(resp.Content);

                SetSession(response);
                return response;
            }
            return null;
        }
        public virtual async Task<AuthDTO> RefreshAuthentification(string token, string refreshToken, CancellationToken cancellationToken)
        {
            if (!IsInit)
            {
                throw new InvalidOperationException("please initialize the handler correctly via method: " + nameof(Init) + "");
            }
            string url = RefreshSessionEndpoint + "/" + refreshToken;
            var headers = new List<KeyValuePair<string, string>>();
            headers.Add(new KeyValuePair<string, string>("Authorization", token));
            RestResponse resp = await Request<AuthDTO>(url, Method.Post, cancellationToken, null, null, headers: headers, true);
            if (resp.IsSuccessStatusCode)
            {
                AuthDTO response;
                response = JsonSerializer.Deserialize<AuthDTO>(resp.Content);
                return response;
            }
            return null;
        }
        public async Task<WebApiHttpRequestResponseModel<ApiDataTransferObject<UserDTO>>> Register(ApiDataTransferObject<RegisterUserDTO> registerDataTransferModel, CancellationToken cancellationToken)
        {
            if (!IsInit)
            {
                throw new InvalidOperationException("please initialize the handler correctly via method: " + nameof(Init) + "");
            }
            var resp = await Request<ApiDataTransferObject<UserDTO>, ApiDataTransferObject<RegisterUserDTO>>(RegisterEndpoint, Method.Post, cancellationToken, registerDataTransferModel, null, null, true);
            if (resp != null)
            {
                return resp;
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
        private bool IsValidWebApiSession(AuthDTO authDTO) => authDTO != null && authDTO.IsAuthentificated;
        public async Task<System.Net.HttpStatusCode> Logout(CancellationToken cancellationToken)
        {
            if (!IsInit)
            {
                throw new InvalidOperationException("please initialize the handler correctly via method: " + nameof(Init) + "");
            }
            if (IsValidWebApiSession((await GetSession())))
            {
                var headers = new List<KeyValuePair<string, string>>();

                var requ = CreateRequest(LogoutSessionEndpoint, Method.Post, ContentType.Json, null, null, headers);
                var resp = await Send(requ, cancellationToken);
                return resp.StatusCode;
            }
            return System.Net.HttpStatusCode.Forbidden;
        }
        public static JsonSerializerOptions JsonSerializerOptions = new JsonSerializerOptions { WriteIndented = true, DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull };
        private readonly ILogger<WebApiRestClient> logger;

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
                logger.LogMethod($"initialize headers, because it was previously null");
            }
            var session = (await GetSession());
            if (IsValidWebApiSession(session))
            {
                headers.Add(new KeyValuePair<string, string>("Authorization", (session).Token));
                logger.LogMethod($"add authorization header: Authorization {session.Token}");
            }
            if (bodyObject != null)
            {
                body = JsonSerializer.Serialize(bodyObject, JsonSerializerOptions);
                logger.LogMethod($"serialize body: {body}");
            }
            int retries = 0;
            bool reauth = false;
            RestResponse response = null;
            do
            {
                url = url.ToLower().StartsWith("http") ? url : BuildUrl(url);
                logger.LogMethod($"start create request object");
                var requestObject = CreateRequest(url, method, ContentType.Json, body, query, headers);
                logger.LogMethod($"create request object: completed");
                logger.LogMethod($"request sending");
                response = await Send(requestObject, cancellationToken);
                logger.LogMethod($"request sent");
                if (!response.IsSuccessStatusCode)
                {
                    logger.LogMethod($"response status-code: {response.StatusCode}");
                    if (RefreshLogin)
                    {
                        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                        {
                            logger.LogMethod($"reauthentificate for invalid token (expired or not set)");
                            if (!reauth)
                            {
                                var responseAuth = await Authentificate(User, Password, cancellationToken);
                                if (responseAuth != null)
                                {
                                    logger.LogMethod($"reauthentificated, set authorization header");
                                    headers.Add(new KeyValuePair<string, string>("Authorization", _currentWebApiSession.Token));
                                }
                                reauth = true;
                            }
                            else//da bereits die erste auth fehlgeschlagen ist werden die nächsten folgen d.h. do-while abort
                            {
                                logger.LogMethod($"reauthentificate failed (check login credentials), reauth abort");
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
                    logger.LogMethod($"try {retries} of {MaxRequestRetries}");
                }
                else
                {
                    donttryagain = true;
                }
            }
            while (retries < MaxRequestRetries && !donttryagain && !response.IsSuccessStatusCode);
            return response;
        }

        public virtual async Task<WebApiHttpRequestResponseModel<T1>> Request<T1, T2>(
            string url,
            Method method,
            CancellationToken cancellationToken,
            T2 bodyObject,
            List<KeyValuePair<string, string>> query = null,
            List<KeyValuePair<string, string>> headers = null,
            bool donttryagain = true)
        {
            logger.LogMethod($"start exec");
            if (headers == null)
            {
                logger.LogMethod($"init headers");
                headers = new List<KeyValuePair<string, string>>();
            }

            WebApiHttpRequestResponseModel<T1> responseModel = new WebApiHttpRequestResponseModel<T1>();
            var resp = await Request<T2>(url, method, cancellationToken, bodyObject, query, headers, donttryagain);
            responseModel.DefaultResponse = resp;
            if (resp != null && resp.Content != null && !string.IsNullOrEmpty(resp.Content))
            {

                logger.LogMethod($"response content is given, start to deserialize to {typeof(T1).Name}");
                string responseJson = resp.Content;
                logger.LogMethod($"response content: {responseJson}");
                var apiResponseModel = JsonSerializer.Deserialize<T1>(responseJson, JsonSerializerOptions);
                responseModel.ApiResponseDeserialized = apiResponseModel;
                logger.LogMethod($"deserialized to {typeof(T1).Name}");
            }
            return responseModel;
        }

        protected virtual void Dispose(bool disposing)
        {
            logger.LogMethod($"disposing started");
            if (!disposedValue)
            {
                logger.LogMethod($"disposing values");
                if (disposing)
                {

                }

                if (_errorOutputEventHandlers.Any())
                {
                    logger.LogMethod($"clear {nameof(_errorOutputEventHandlers)}");
                    _errorOutputEventHandlers.Clear();
                }
                logger.LogMethod($"disposing completed, set boolean {disposedValue}");
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
            logger.LogMethod($"disposing started");
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
            logger.LogMethod($"garbage collector invoked to suppress finalize");
        }

        public void SetSession(AuthDTO authDTO)
        {
            _currentWebApiSession = authDTO;
            logger.LogMethod($"set session for 'Authorization' header");
        }

        public async Task<AuthDTO> GetSession()
        {
            if(_currentWebApiSession == null)
            {
                if(_currentWebApiSessionGetFunc != null)
                {
                    var auth = await _currentWebApiSessionGetFunc();
                    return auth;
                }
            }
            return _currentWebApiSession;
        }

        public void SetSession(Func<Task<AuthDTO>> funcGetAuthDTO)
        {
            _currentWebApiSessionGetFunc = funcGetAuthDTO;
            logger.LogMethod($"set session for 'Authorization' header");
        }
    }
}
