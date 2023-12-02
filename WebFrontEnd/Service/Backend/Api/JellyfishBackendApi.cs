using Microsoft.AspNetCore.Components.Authorization;
using RestSharp;
using Shared.ApiDataTransferObject;
using Shared.ApiDataTransferObject.Object;
using Shared.Authentification.Claims;
using Shared.Const;
using Shared.DataFilter.Presentation;
using Shared.DataTransferObject;
using Shared.DataTransferObject.Abstraction;
using Shared.Reflection;
using System;
using WebFrontEnd.Authentification;
using WebFrontEnd.Service.Authentification;

namespace WebFrontEnd.Service.Backend.Api
{
    public partial class JellyfishBackendApi : WebApiRestClient
    {

        private readonly CustomAuthentificationStateProvider _customAuthentificationStateProvider;

        public JellyfishBackendApi(
            IConfiguration configuration,
            CustomAuthentificationStateProvider customAuthentificationStateProvider) : base(configuration)
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
            var url = "/user/" + user.Uuid + "";
            var response = await this.TypedRequest<UserDTO, UserDTO>(url, RestSharp.Method.Put, user, cancellationToken);
            return response;
        }
        public async Task<JellyfishBackendApiResponse<UserDTO>> UpdateUserProfilePicture(UserDTO user, CancellationToken cancellationToken)
        {
            var url = "/user/" + user.Uuid + "/profile-picture";
            var response = await this.TypedRequest<UserDTO, UserDTO>(url, RestSharp.Method.Put, user, cancellationToken);
            return response;
        }
        public async Task<JellyfishBackendApiResponse<UserDTO>> DeleteUserProfilePicture(UserDTO userDTO, CancellationToken cancellationToken)
        {
            var url = "/user/" + userDTO.Uuid + "/profile-picture";
            var response = await this.TypedRequest<UserDTO, UserDTO>(url, RestSharp.Method.Delete, null, cancellationToken);
            return response;
        }
        public async Task<JellyfishBackendApiResponse<List<UserDTO>>> GetUsers(SearchParamsBody searchParamsBody, CancellationToken cancellationToken)
        {

            //var response = await this.tx<ApiDataTransferObject<List<UserDTO>>, SearchParamsBody>("/user", RestSharp.Method.Get, cancellationToken, searchParamsBody);
            //return JellyfishBackendApiResponse<List<UserDTO>>.CreateFromWebApiResponseModel(response);
            var url = "/user";
            var response = await this.TypedRequest<SearchParamsBody, List<UserDTO>>(url, RestSharp.Method.Get, searchParamsBody, cancellationToken);
            return response;
        }
        public async Task<JellyfishBackendApiResponse<UserDTO>> GetUser(Guid userId, CancellationToken cancellationToken)
        {

            var url = "/user/" + userId + "";
            var response = await this.TypedRequest<SearchParamsBody, UserDTO>(url, RestSharp.Method.Get, null, cancellationToken);
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
            var url = "/user/" + user.Uuid + "/role";
            var response = await this.TypedRequest<List<RoleDTO>, List<Guid>>(url, RestSharp.Method.Patch, roleDTOs, cancellationToken);
            return response;
        }
        public async Task<JellyfishBackendApiResponse<List<Guid>>> RevokeRoles(UserDTO user, List<RoleDTO> roleDTOs, CancellationToken cancellationToken)
        {
            var url = "/user/" + user.Uuid + "/role";
            var response = await this.TypedRequest<List<RoleDTO>, List<Guid>>(url, RestSharp.Method.Delete, roleDTOs, cancellationToken);
            return response;
        }
        public async Task<UserDTO?> GetCurrentUser(AuthenticationState authenticationState, CancellationToken cancellationToken)
        {
            if (authenticationState is null)
                return null;
            if (authenticationState.User is null)
                return null;
            if (!authenticationState.User .Claims.Any())
                return null;
            var userUuid = authenticationState.User.Claims.GetClaims(x => x.Type == AuthorizationConst.Claims.ClaimTypeUserUuid)?.First();
            var userProfile = await this.GetUser(Guid.Parse(userUuid.Value), cancellationToken);
            return userProfile.IsSuccess ? userProfile.Data : null;
        }
        public override async Task<WebApiHttpRequestResponseModel<T1>> Request<T1, T2>(string url, Method method, CancellationToken cancellationToken, T2 bodyObject = default, List<KeyValuePair<string, string>> query = null, List<KeyValuePair<string, string>> headers = null, bool donttryagain = true)
        {
            if(headers==null)
            {
                headers = await CreateHeaders(cancellationToken);
            }
            if (headers!=null&& headers.Any())
            {
                var newHeaders = await CreateHeaders(cancellationToken);
                headers.AddRange(newHeaders);
            }
            return await base.Request<T1, T2>(url, method, cancellationToken, bodyObject, query, headers, donttryagain);
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
        public async Task<JellyfishBackendApiResponse<T2>> TypedRequest<T,T2>(string url, RestSharp.Method method, T data, CancellationToken cancellationToken, PaginationBase paginationBase=null)
            
        {
            if(data is IDataTransferObject dataTransferObject || (ReflectionExtension.IsListAndGenericTypeImplementsT<IDataTransferObject>(typeof(T))))
            {
                var body = ApiDataTransferObject<T>.Create(data,paginationBase);
                var response = await Request<ApiDataTransferObject<T2>, ApiDataTransferObject<T>>(url, method, cancellationToken, body);
                return JellyfishBackendApiResponse<T2>.CreateFromWebApiResponseModel(response);
            }
            else if(data is SearchParamsBody)
            {
                return await RequestI<T, T2>(url, method, data, cancellationToken);
            }
            return await RequestI<T,T2>(url, method,data,cancellationToken);
        }
        private async Task<JellyfishBackendApiResponse<T2>> RequestI<T,T2>(string url, RestSharp.Method method, T data, CancellationToken cancellationToken)
        {

            T body = data;
            var response = await this.Request<ApiDataTransferObject<T2>, T>(url, method, cancellationToken, body);

            return JellyfishBackendApiResponse<T2>.CreateFromWebApiResponseModel(response);
        }

        public async Task<List<KeyValuePair<string,string>>> CreateHeaders(CancellationToken cancellationToken)
        {
            var auth = await _customAuthentificationStateProvider.GetCurrentAuthentification(cancellationToken);
            return auth.CreateAuthorizationHeader();
        }
    }
}
