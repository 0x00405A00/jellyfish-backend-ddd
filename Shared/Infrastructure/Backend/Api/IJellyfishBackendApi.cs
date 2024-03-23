using Microsoft.AspNetCore.Components.Authorization;
using RestSharp;
using Shared.ApiDataTransferObject;
using Shared.ApiDataTransferObject.Object;
using Shared.DataFilter.Presentation;
using Shared.DataTransferObject;
using Shared.DataTransferObject.Messenger;
using System.Net;
using static Shared.Infrastructure.Backend.Api.JellyfishBackendApi;

namespace Shared.Infrastructure.Backend.Api
{
    public interface IJellyfishBackendApi
    {
        Task<UserDTO?> GetCurrentUser(AuthenticationState authenticationState, CancellationToken cancellationToken);
        void AddErrorHandler(ErrorOutputEventHandler eventHandler);
        Task<JellyfishBackendApi.JellyfishBackendApiResponse<UserDTO>> AddUser(UserDTO user, CancellationToken cancellationToken);
        Task<JellyfishBackendApi.JellyfishBackendApiResponse<Guid>> RemoveUser(UserDTO user, CancellationToken cancellationToken);
        Task<JellyfishBackendApi.JellyfishBackendApiResponse<List<Guid>>> AssignRoles(UserDTO user, List<RoleDTO> roleDTOs, CancellationToken cancellationToken);
        Task<JellyfishBackendApi.JellyfishBackendApiResponse<Guid>> ChangePassword(Guid userId, PasswordChangeDTO passwordChangeDTO, CancellationToken cancellationToken);
        Task<JellyfishBackendApi.JellyfishBackendApiResponse<UserDTO>> DeleteUserProfilePicture(UserDTO userDTO, CancellationToken cancellationToken);
        Task<JellyfishBackendApi.JellyfishBackendApiResponse<UserDTO>> EditUser(UserDTO user, CancellationToken cancellationToken);
        Task<JellyfishBackendApi.JellyfishBackendApiResponse<List<RoleDTO>>> GetRoles(CancellationToken cancellationToken);
        Task<JellyfishBackendApi.JellyfishBackendApiResponse<List<UserDTO>>> GetUsers(SearchParamsBody? searchParamsBody, CancellationToken cancellationToken);
        Task<JellyfishBackendApi.JellyfishBackendApiResponse<List<UserDTO>>> GetUsers(string searchText, int selectedPage, int maxItemsPerPage, bool filterOnlyDeletedUsers, CancellationToken cancellationToken);
        Task<JellyfishBackendApi.JellyfishBackendApiResponse<List<UserTypeDTO>>> GetUserTypes(CancellationToken cancellationToken);
        Task<JellyfishBackendApi.JellyfishBackendApiResponse<List<Guid>>> RevokeRoles(UserDTO user, List<RoleDTO> roleDTOs, CancellationToken cancellationToken);
        Task<JellyfishBackendApi.JellyfishBackendApiResponse<T2>> TypedRequest<T, T2>(string url, Method method, T? data, CancellationToken cancellationToken, PaginationBase paginationBase = null);
        Task<JellyfishBackendApi.JellyfishBackendApiResponse<UserDTO>> UpdateUserProfilePicture(UserDTO user, CancellationToken cancellationToken);
        Task<JellyfishBackendApi.JellyfishBackendApiResponse<UserDTO>> Activate(string base64Token, UserActivationDataTransferModel userActivationDataTransferModel, CancellationToken cancellationToken);
        Task<JellyfishBackendApi.JellyfishBackendApiResponse<UserDTO>> Register(RegisterUserDTO registerDataTransferModel, CancellationToken cancellationToken);
        Task<JellyfishBackendApi.JellyfishBackendApiResponse<bool>> ResetPassword(PasswordResetDataTransferModel passwordResetDataTransferModel, CancellationToken cancellationToken);
        Task<JellyfishBackendApi.JellyfishBackendApiResponse<bool>> ResetPasswordRequest(PasswordResetRequestDTO passwordResetRequestDTO, CancellationToken cancellationToken);
        Task<JellyfishBackendApi.JellyfishBackendApiResponse<List<MessageDTO>>> GetNotDeliveredMessages(CancellationToken cancellationToken);
        Task<JellyfishBackendApi.JellyfishBackendApiResponse<MessageDTO>> AcknowledgeNotDeliveredMessages(Guid messageId,CancellationToken cancellationToken);

        Task<AuthDTO> Authentificate(string userName, string password, CancellationToken cancellationToken);
        Task<AuthDTO> RefreshAuthentification(string token, string refreshToken, CancellationToken cancellationToken);
        Task<HttpStatusCode> Logout(CancellationToken cancellationToken);
    }
}