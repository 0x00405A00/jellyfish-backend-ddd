using RestSharp;
using Shared.ApiDataTransferObject;
using Shared.ApiDataTransferObject.Object;
using Shared.DataFilter.Presentation;
using Shared.DataTransferObject;
using WebFrontEnd.Authentification;

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
        public async Task<JellyfishBackendApiResponse<List<UserDTO>>> GetUsers(SearchParamsBody searchParamsBody, CancellationToken cancellationToken)
        {

            //var response = await this.tx<ApiDataTransferObject<List<UserDTO>>, SearchParamsBody>("/user", RestSharp.Method.Get, cancellationToken, searchParamsBody);
            //return JellyfishBackendApiResponse<List<UserDTO>>.CreateFromWebApiResponseModel(response);
            var url = "/user";
            var response = await this.TypedRequest<SearchParamsBody, List<UserDTO>>(url, RestSharp.Method.Get, searchParamsBody, cancellationToken);
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
        public async Task<JellyfishBackendApiResponse<T2>> TypedRequest<T,T2>(string url, RestSharp.Method method, T data, CancellationToken cancellationToken, PaginationBase paginationBase=null)
            
        {
            if(data is IDataTransferObject dataTransferObject)
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
