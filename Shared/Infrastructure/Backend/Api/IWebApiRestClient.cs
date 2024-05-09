using Microsoft.AspNetCore.Components.Authorization;
using RestSharp;
using Shared.ApiDataTransferObject;
using Shared.ApiDataTransferObject.Object;
using Shared.DataTransferObject;
using System.Net;
using static Shared.Infrastructure.Backend.Api.JellyfishBackendApi;

namespace Shared.Infrastructure.Backend.Api
{
    public interface IWebApiRestClient
    {
        string BaseUrl { get; set; }
        string ConnectionTestEndpoint { get; }
        bool IsInit { get; }
        string LoginSessionEndpoint { get; }
        string LogoutSessionEndpoint { get; }
        string Password { get; }
        string PasswordResetDataTransferModelEndpoint { get; }
        string PasswordResetRequestDTOEndpoint { get; }
        bool RefreshLogin { get; }
        string RefreshSessionEndpoint { get; }
        string RegisterEndpoint { get; }
        string RegistrationActivateEndpoint { get; }
        string User { get; }
        string ValidateSessionEndpoint { get; }

        void AddErrorHandler(ErrorOutputEventHandler eventHandler);

        Task<WebApiHttpRequestResponseModel<ApiDataTransferObject<UserDTO>>> Register(ApiDataTransferObject<RegisterUserDTO> registerDataTransferModel, CancellationToken cancellationToken);
        Task<JellyfishBackendApi.JellyfishBackendApiResponse<UserDTO>> Register(RegisterUserDTO registerDataTransferModel, CancellationToken cancellationToken);
        Task<JellyfishBackendApi.JellyfishBackendApiResponse<UserDTO>> Activate(string base64Token, UserActivationDataTransferModel userActivationDataTransferModel, CancellationToken cancellationToken);

        Task<AuthDTO> Authentificate(string userName, string password, CancellationToken cancellationToken);
        void SetSession(AuthDTO authDTO);
        void SetSession(Func<Task<AuthDTO>> funcGetAuthDTO);
        Task<AuthDTO> GetSession();
        Task<AuthDTO> RefreshAuthentification(string token, string refreshToken, CancellationToken cancellationToken);
        Task<HttpStatusCode> Logout(CancellationToken cancellationToken);

        Task<JellyfishBackendApi.JellyfishBackendApiResponse<bool>> ResetPassword(PasswordResetDataTransferModel passwordResetDataTransferModel, CancellationToken cancellationToken);
        Task<JellyfishBackendApi.JellyfishBackendApiResponse<bool>> ResetPasswordRequest(PasswordResetRequestDTO passwordResetRequestDTO, CancellationToken cancellationToken);

        string BuildUrl(string endPoint);
        Task<bool> ConnectionTest(CancellationToken cancellationToken);
        Task<UserDTO?> GetCurrentUser(AuthenticationState authenticationState, CancellationToken cancellationToken);
        Task<JellyfishBackendApiResponse<UserDTO>> GetUser(Guid userId, CancellationToken cancellationToken);
        void Init(string baseUrl, string loginSessionEndpoint, string logoutSessionEndpoint, string registerEndpoint, string validateSessionEndpoint, string refreshSessionEndpoint, string connectionTestEndpoint, string passwordResetEndpoint, string passwordResetRequestEndpoint);
        Task<WebApiHttpRequestResponseModel<T1>> Request<T1, T2>(string url, Method method, CancellationToken cancellationToken, T2 bodyObject, List<KeyValuePair<string, string>> query = null, List<KeyValuePair<string, string>> headers = null, bool donttryagain = true);
        Task<RestResponse> Request<T2>(string url, Method method, CancellationToken cancellationToken, object bodyObject = null, List<KeyValuePair<string, string>> query = null, List<KeyValuePair<string, string>> headers = null, bool donttryagain = true);
        Task<JellyfishBackendApiResponse<T2>> TypedRequest<T, T2>(string url, Method method, T? data, CancellationToken cancellationToken, PaginationBase paginationBase = null);

    }
}
