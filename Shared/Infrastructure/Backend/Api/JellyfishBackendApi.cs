using Microsoft.AspNetCore.Components.Authorization;
using RestSharp;
using Shared.ApiDataTransferObject.Object;
using Shared.DataFilter.Presentation;
using Shared.DataTransferObject;
using Shared.DataTransferObject.Messenger;
using System.Net;
using System.Reflection;
using System.Text.Json.Serialization;

namespace Shared.Infrastructure.Backend.Api
{
    public partial class JellyfishBackendApi : IJellyfishBackendApi
    {
        public delegate void ErrorOutputEventHandler(object sender, JellyfishBackendApi.JellyfishApiErrorEventArgs e);
        public sealed record JellyfishApiErrorEventArgs(HttpStatusCode StatusCode, Uri ResponseUri, List<ApiError> Errors);

        private readonly IWebApiRestClient apiRestClient;

        public JellyfishBackendApi(
            IWebApiRestClient apiRestClient)
        {
            this.apiRestClient = apiRestClient;
        }

        public async Task<JellyfishBackendApiResponse<Guid>> ChangePassword(Guid userId, PasswordChangeDTO passwordChangeDTO, CancellationToken cancellationToken)
        {
            var response = await this.TypedRequest<PasswordChangeDTO, Guid>($"/user/password/change/{userId}", RestSharp.Method.Post, passwordChangeDTO, cancellationToken);
            return response;
        }
        public async Task<JellyfishBackendApiResponse<UserDTO>> AddUser(UserDTO user, CancellationToken cancellationToken)
        {
            var response = await this.TypedRequest<UserDTO, UserDTO>($"/user", RestSharp.Method.Post, user, cancellationToken);
            return response;
        }
        public Task<JellyfishBackendApiResponse<UserDTO>> EditUser(UserDTO user, CancellationToken cancellationToken)
        {
            var url = "/user/" + user.Id + "";
            var response = this.TypedRequest<UserDTO, UserDTO>(url, RestSharp.Method.Put, user, cancellationToken);
            return response;
        }
        public Task<JellyfishBackendApiResponse<UserDTO>> UpdateUserProfilePicture(UserDTO user, CancellationToken cancellationToken)
        {
            var url = "/user/" + user.Id + "/profile-picture";
            var response = this.TypedRequest<UserDTO, UserDTO>(url, RestSharp.Method.Put, user, cancellationToken);
            return response;
        }
        public Task<JellyfishBackendApiResponse<UserDTO>> DeleteUserProfilePicture(UserDTO userDTO, CancellationToken cancellationToken)
        {
            var url = "/user/" + userDTO.Id + "/profile-picture";
            var response = this.TypedRequest<UserDTO, UserDTO>(url, RestSharp.Method.Delete, null, cancellationToken);
            return response;
        }
        public Task<JellyfishBackendApiResponse<T2>> TypedRequest<T, T2>(string url, Method method, T? data, CancellationToken cancellationToken, PaginationBase paginationBase = null)
        {
            return apiRestClient.TypedRequest<T, T2>(url, method, data, cancellationToken, paginationBase);
        }
        public Task<JellyfishBackendApiResponse<List<UserDTO>>> GetUsers(string searchText, int selectedPage, int maxItemsPerPage, bool filterOnlyDeletedUsers, CancellationToken cancellationToken)
        {
            SearchParamsBody searchParamsBody = new SearchParamsBody();
            searchParamsBody.page_offset = selectedPage;
            searchParamsBody.page_size = maxItemsPerPage;

            var columnFilterDeleted = new Shared.DataFilter.Infrastructure.ColumnFilter
            {
                field = "deleted_time",
                op = "eq",
                value = null
            };
            if (filterOnlyDeletedUsers)
            {
                columnFilterDeleted = new Shared.DataFilter.Infrastructure.ColumnFilter
                {
                    field = "deleted_time",
                    op = "neq",
                    value = null
                };
            }

            var columnFilterGroupAnd = new Shared.DataFilter.Infrastructure.ColumnFilterGroup
            {
                Group = "and",
                Filters = new List<Shared.DataFilter.Infrastructure.ColumnFilter>
                            {
                                columnFilterDeleted
                            }
            };
            searchParamsBody.filters = new List<Shared.DataFilter.Infrastructure.ColumnFilterGroup>
            {
                columnFilterGroupAnd
            };
            if (!String.IsNullOrWhiteSpace(searchText))
            {
                var columnFilterGroupOr =
                        new Shared.DataFilter.Infrastructure.ColumnFilterGroup
                        {
                            Group = "or",
                            Filters = new List<Shared.DataFilter.Infrastructure.ColumnFilter>()
                        };

                var properties = typeof(UserDTO).GetProperties()
                        .Where(x => x.CanWrite && (x.PropertyType == typeof(string) || (x.PropertyType == typeof(Guid) || x.PropertyType == typeof(Guid?)) || (x.PropertyType == typeof(DateTime) || x.PropertyType == typeof(DateTime?))))
                        .Select(x => new { attr = x.GetCustomAttribute<JsonPropertyNameAttribute>(), propertyType = x.PropertyType })
                        .ToList();
                foreach (var prop in properties)
                {
                    string field = prop.attr.Name;
                    string op = null;
                    if (prop.propertyType == typeof(string) || prop.propertyType == typeof(Guid) || prop.propertyType == typeof(Guid?))
                    {
                        op = "contains";
                    }
                    else if (prop.propertyType == typeof(DateTime) || prop.propertyType == typeof(DateTime?))
                    {
                        op = "contains";
                    }
                    else
                    {
                        op = "eq";
                    }

                    var condition = new Shared.DataFilter.Infrastructure.ColumnFilter
                    {
                        field = field,
                        op = op,
                        value = searchText
                    };
                    columnFilterGroupOr.Filters.Add(condition);
                }
                searchParamsBody.filters.Add(columnFilterGroupOr);
            }

            return this.GetUsers(searchParamsBody, cancellationToken);
        }
        public Task<JellyfishBackendApiResponse<List<UserDTO>>> GetUsers(SearchParamsBody? searchParamsBody, CancellationToken cancellationToken)
        {

            //var response = await this.tx<ApiDataTransferObject<List<UserDTO>>, SearchParamsBody>("/user", RestSharp.Method.Get, cancellationToken, searchParamsBody);
            //return JellyfishBackendApiResponse<List<UserDTO>>.CreateFromWebApiResponseModel(response);
            var url = "/user";
            return this.TypedRequest<SearchParamsBody?, List<UserDTO>>(url, RestSharp.Method.Get, searchParamsBody, cancellationToken);
        }
        public Task<JellyfishBackendApiResponse<UserDTO>> Activate(string base64Token, UserActivationDataTransferModel userActivationDataTransferModel, CancellationToken cancellationToken)
        {
            return apiRestClient.Activate(base64Token,userActivationDataTransferModel,cancellationToken);
        }
        public Task<JellyfishBackendApiResponse<UserDTO>> Register(RegisterUserDTO registerDataTransferModel, CancellationToken cancellationToken)
        {
            return apiRestClient.Register(registerDataTransferModel,cancellationToken);
        }
        /// <summary>
        /// Sends the confirmation secure code from users mail to backend to confirm the password request action
        /// </summary>
        /// <param name="passwordResetRequestDataTransferModel"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public Task<JellyfishBackendApiResponse<bool>> ResetPasswordRequest(PasswordResetRequestDTO passwordResetRequestDTO, CancellationToken cancellationToken)
        {
            return apiRestClient.ResetPasswordRequest(passwordResetRequestDTO,cancellationToken);
        }
        /// <summary>
        /// Reset the password, previously the password reset must be requested and confirmed by secure code 
        /// </summary>
        /// <param name="passwordResetDataTransferModel"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public Task<JellyfishBackendApiResponse<bool>> ResetPassword(PasswordResetDataTransferModel passwordResetDataTransferModel, CancellationToken cancellationToken)
        {
            return apiRestClient.ResetPassword(passwordResetDataTransferModel,cancellationToken);
        }
        public Task<JellyfishBackendApiResponse<List<UserTypeDTO>>> GetUserTypes(CancellationToken cancellationToken)
        {
            return this.TypedRequest<object, List<UserTypeDTO>>("/user/user-types", RestSharp.Method.Get, null, cancellationToken);
        }
        public Task<JellyfishBackendApiResponse<List<RoleDTO>>> GetRoles(CancellationToken cancellationToken)
        {

            return this.TypedRequest<object, List<RoleDTO>>("/role", RestSharp.Method.Get, null, cancellationToken);
        }
        public Task<JellyfishBackendApiResponse<ChatDTO>> GetChat(Guid chatId, CancellationToken cancellationToken)
        {
            var url = "/chat/" + chatId + "";
            return this.TypedRequest<object, ChatDTO>(url, RestSharp.Method.Get, null, cancellationToken);
        }
        public Task<JellyfishBackendApiResponse<List<Guid>>> AssignRoles(UserDTO user, List<RoleDTO> roleDTOs, CancellationToken cancellationToken)
        {
            var url = "/user/" + user.Id + "/role";
            return this.TypedRequest<List<RoleDTO>, List<Guid>>(url, RestSharp.Method.Patch, roleDTOs, cancellationToken);
        }
        public Task<JellyfishBackendApiResponse<List<Guid>>> RevokeRoles(UserDTO user, List<RoleDTO> roleDTOs, CancellationToken cancellationToken)
        {
            var url = "/user/" + user.Id + "/role";
            return this.TypedRequest<List<RoleDTO>, List<Guid>>(url, RestSharp.Method.Delete, roleDTOs, cancellationToken);
        }
        /// <summary>
        /// Gets user data of a specific user from api (<see cref="MessengerUserDTO"/> less complex than <see cref="UserDTO"/>
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns><see cref="JellyfishBackendApiResponse"/> with trailing data of type <see cref="MessengerUserDTO"/></returns>
        public Task<JellyfishBackendApiResponse<MessengerUserDTO>> GetMessengerUser(Guid userId, CancellationToken cancellationToken)
        {
            var url = "/user/messenger/" + userId + "";
            return this.TypedRequest<object, MessengerUserDTO>(url, RestSharp.Method.Get, null, cancellationToken);
        }
        public Task<UserDTO?> GetCurrentUser(AuthenticationState authenticationState, CancellationToken cancellationToken)
        {
            return apiRestClient.GetCurrentUser(authenticationState, cancellationToken);
        }

        public void AddErrorHandler(ErrorOutputEventHandler eventHandler)
        {
            apiRestClient.AddErrorHandler(eventHandler);
        }

        public Task<JellyfishBackendApiResponse<Guid>> RemoveUser(UserDTO user, CancellationToken cancellationToken)
        {
            var url = "/user/" + user.Id + "";
            return this.TypedRequest<object, Guid>(url, RestSharp.Method.Delete, null, cancellationToken);
        }

        public Task<AuthDTO> Authentificate(string userName, string password, CancellationToken cancellationToken)
        {
            return apiRestClient.Authentificate(userName, password, cancellationToken) ;
        }

        public Task<AuthDTO> RefreshAuthentification(string token, string refreshToken, CancellationToken cancellationToken)
        {
            return apiRestClient.RefreshAuthentification(token, refreshToken, cancellationToken) ;
        }

        public Task<HttpStatusCode> Logout(CancellationToken cancellationToken)
        {
            return apiRestClient.Logout(cancellationToken) ;  
        }

        public Task<JellyfishBackendApiResponse<List<MessageDTO>>> GetNotDeliveredMessages(CancellationToken cancellationToken)
        {
            var url = "/chat/message/nack";
            return this.TypedRequest<object, List<MessageDTO>>(url, RestSharp.Method.Get, null, cancellationToken);
        }

        public Task<JellyfishBackendApiResponse<MessageDTO>> AcknowledgeNotDeliveredMessages(Guid messageId, CancellationToken cancellationToken)
        {
            var url = $"/chat/message/ack/{messageId}";
            return this.TypedRequest<object, MessageDTO>(url, RestSharp.Method.Post, null, cancellationToken);
        }

        public Task<bool> ConnectionTest(CancellationToken cancellationToken)
        {
            return this.apiRestClient.ConnectionTest(cancellationToken) ;
        }

        public void SetSession(AuthDTO authDTO)
        {
            this.apiRestClient.SetSession(authDTO) ;
        }

        public Task<AuthDTO> GetSession() => this.apiRestClient.GetSession();

        public void SetSession(Func<Task<AuthDTO>> funcGetAuthDTO)
        {
            apiRestClient.SetSession(funcGetAuthDTO) ;
        }

    }
}
