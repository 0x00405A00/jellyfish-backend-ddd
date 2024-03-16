using Microsoft.Extensions.Configuration;
using RestSharp;
using Shared.ApiDataTransferObject.Object;
using Shared.DataFilter.Presentation;
using Shared.DataTransferObject;
using System.Net;
using System.Reflection;
using System.Text.Json.Serialization;

namespace Shared.Infrastructure.Backend.Api
{
    public partial class JellyfishBackendApi : WebApiRestClient
    {
        public delegate void ErrorOutputEventHandler(object sender, JellyfishBackendApi.JellyfishApiErrorEventArgs e);
        public sealed record JellyfishApiErrorEventArgs(HttpStatusCode StatusCode,Uri ResponseUri, List<ApiError> Errors);

        private readonly ICustomAuthentificationStateProvider _customAuthentificationStateProvider;

        public JellyfishBackendApi(
            IConfiguration configuration,
            ICustomAuthentificationStateProvider customAuthentificationStateProvider) : base(configuration)
        {
            this._customAuthentificationStateProvider = customAuthentificationStateProvider;
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
        public async Task<JellyfishBackendApiResponse<UserDTO>> EditUser(UserDTO user, CancellationToken cancellationToken)
        {
            var url = "/user/" + user.Id + "";
            var response = await this.TypedRequest<UserDTO, UserDTO>(url, RestSharp.Method.Put, user, cancellationToken);
            return response;
        }
        public async Task<JellyfishBackendApiResponse<UserDTO>> UpdateUserProfilePicture(UserDTO user, CancellationToken cancellationToken)
        {
            var url = "/user/" + user.Id + "/profile-picture";
            var response = await this.TypedRequest<UserDTO, UserDTO>(url, RestSharp.Method.Put, user, cancellationToken);
            return response;
        }
        public async Task<JellyfishBackendApiResponse<UserDTO>> DeleteUserProfilePicture(UserDTO userDTO, CancellationToken cancellationToken)
        {
            var url = "/user/" + userDTO.Id + "/profile-picture";
            var response = await this.TypedRequest<UserDTO, UserDTO>(url, RestSharp.Method.Delete, null, cancellationToken);
            return response;
        }
        public override Task<JellyfishBackendApiResponse<T2>> TypedRequest<T, T2>(string url, Method method, T? data, CancellationToken cancellationToken, PaginationBase paginationBase = null) where T : default
        {
            return base.TypedRequest<T, T2>(url, method, data, cancellationToken, paginationBase);
        }
        public async Task<JellyfishBackendApiResponse<List<UserDTO>>> GetUsers(string searchText,int selectedPage,int maxItemsPerPage,bool filterOnlyDeletedUsers, CancellationToken cancellationToken)
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
            var response = await this.GetUsers(searchParamsBody, cancellationToken);

            return response;
        }
        public async Task<JellyfishBackendApiResponse<List<UserDTO>>> GetUsers(SearchParamsBody? searchParamsBody, CancellationToken cancellationToken)
        {

            //var response = await this.tx<ApiDataTransferObject<List<UserDTO>>, SearchParamsBody>("/user", RestSharp.Method.Get, cancellationToken, searchParamsBody);
            //return JellyfishBackendApiResponse<List<UserDTO>>.CreateFromWebApiResponseModel(response);
            var url = "/user";
            var response = await this.TypedRequest<SearchParamsBody?, List<UserDTO>>(url, RestSharp.Method.Get, searchParamsBody, cancellationToken);
            return response;
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
        public async Task<JellyfishBackendApiResponse<List<UserTypeDTO>>> GetUserTypes(CancellationToken cancellationToken)
        {

            var response = await this.TypedRequest<object, List<UserTypeDTO>>("/user/user-types", RestSharp.Method.Get, null, cancellationToken);
            return response;
        }
        public async Task<JellyfishBackendApiResponse<List<RoleDTO>>> GetRoles(CancellationToken cancellationToken)
        {

            var response = await this.TypedRequest<object, List<RoleDTO>>("/role", RestSharp.Method.Get, null, cancellationToken);
            return response;
        }
        public async Task<JellyfishBackendApiResponse<List<Guid>>> AssignRoles(UserDTO user, List<RoleDTO> roleDTOs, CancellationToken cancellationToken)
        {
            var url = "/user/" + user.Id + "/role";
            var response = await this.TypedRequest<List<RoleDTO>, List<Guid>>(url, RestSharp.Method.Patch, roleDTOs, cancellationToken);
            return response;
        }
        public async Task<JellyfishBackendApiResponse<List<Guid>>> RevokeRoles(UserDTO user, List<RoleDTO> roleDTOs, CancellationToken cancellationToken)
        {
            var url = "/user/" + user.Id + "/role";
            var response = await this.TypedRequest<List<RoleDTO>, List<Guid>>(url, RestSharp.Method.Delete, roleDTOs, cancellationToken);
            return response;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
