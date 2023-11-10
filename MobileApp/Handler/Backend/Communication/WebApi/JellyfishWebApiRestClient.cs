using MobileApp.Data.WebApi;
using MobileApp.Handler.AppConfig;
using RestSharp;

namespace MobileApp.Handler.Backend.Communication.WebApi
{
    public class JellyfishWebApiRestClient : WebApiRestClient
    {
        public struct JellyfishEndpoints
        {
            public const string GetFriendEndpoint = "user/friends";
            public const string GetFriendshipRequestsEndpoint = "user/friendship/request";
            public const string AcceptFriendshipRequestsEndpoint = "user/friendship/request/accept";
            public const string SearchUserEndpoint = "user/search";
            public const string OwnProfileUserEndpoint = "user/profile";
            public const string AddFriendUserEndpoint = "user/friendship/request";
            public const string GetNackMessages = "message/nack";
        }
        private readonly ApplicationConfigHandler _applicationConfigHandler;

        public JellyfishWebApiRestClient(ApplicationConfigHandler applicationConfigHandler):base()
        {
            _applicationConfigHandler = applicationConfigHandler;
        }
        public override RestRequest CreateRequest(string urlOrEndpoint, Method httpMethod, ContentType contentType, string body = null, List<KeyValuePair<string, string>> query = null, List<KeyValuePair<string, string>> headerValues = null)
        {

            var baseRequ = base.CreateRequest(urlOrEndpoint, httpMethod, contentType, body, query, headerValues);
            
            return baseRequ;
        }
        public override Task<AuthModel> Authentificate(string userName, string password, CancellationToken cancellationToken)
        {
            var result =base.Authentificate(userName, password, cancellationToken);
            return result;
        }

        public async Task<WebApiHttpRequestResponseModel<Shared.Kernel.Application.Model.DataTransferObject.ConcreteImplementation.Jellyfish.UserJellyfishDTO>> GetFriends(CancellationToken cancellationToken)
        {
            var url = BuildUrl(JellyfishEndpoints.GetFriendEndpoint);
            var appendHeaders = new List<KeyValuePair<string, string>>();
            appendHeaders.Add(new KeyValuePair<string, string>("Authorization", "Bearer " + _applicationConfigHandler.ApplicationConfig.AccountConfig.UserSession.Token));
            var result = await this.Request<Shared.Kernel.Application.Model.DataTransferObject.ConcreteImplementation.Jellyfish.UserJellyfishDTO, object>(url: url, method: RestSharp.Method.Get, cancellationToken: cancellationToken, null, null, appendHeaders, false);

            return result;
        }
        public async Task<WebApiHttpRequestResponseModel<UserFriendshipUserModelDTO>> GetFriendshipRequests(CancellationToken cancellationToken)
        {
            var url = BuildUrl(JellyfishEndpoints.GetFriendshipRequestsEndpoint);
            var appendHeaders = new List<KeyValuePair<string, string>>();
            appendHeaders.Add(new KeyValuePair<string, string>("Authorization", "Bearer " + _applicationConfigHandler.ApplicationConfig.AccountConfig.UserSession.Token));
            var result = await this.Request<UserFriendshipUserModelDTO, object>(url: url, method: RestSharp.Method.Get, cancellationToken: cancellationToken, null, null, appendHeaders, false);

            return result;
        }
        public async Task<WebApiHttpRequestResponseModel<Shared.Kernel.Application.Model.DataTransferObject.ConcreteImplementation.Jellyfish.UserJellyfishDTO>> AcceptFriendshipRequests(Guid requestUuid,CancellationToken cancellationToken)
        {
            var url = BuildUrl(JellyfishEndpoints.AcceptFriendshipRequestsEndpoint);
            var appendHeaders = new List<KeyValuePair<string, string>>();
            appendHeaders.Add(new KeyValuePair<string, string>("Authorization", "Bearer " + _applicationConfigHandler.ApplicationConfig.AccountConfig.UserSession.Token));
            var model = new WebApiModel.RootObject { data = new WebApiModel.Data<UserFriendshipRequestAcceptDTO> { type = nameof(UserFriendshipRequestAcceptDTO), attributes = new UserFriendshipRequestAcceptDTO { UserFriendshipRequestUuids = new List<Guid> { requestUuid } } }, meta = new WebApiModel.Meta { count = 1 } };
            var result = await this.Request<Shared.Kernel.Application.Model.DataTransferObject.ConcreteImplementation.Jellyfish.UserJellyfishDTO, object>(url: url, method: RestSharp.Method.Post, cancellationToken: cancellationToken, model, null, appendHeaders, false);

            return result;
        }

        public async Task<WebApiHttpRequestResponseModel<Shared.Kernel.Application.Model.DataTransferObject.ConcreteImplementation.Jellyfish.UserJellyfishDTO>> SearchUser(string searchName, CancellationToken cancellationToken)
        {
            var url = BuildUrl(JellyfishEndpoints.SearchUserEndpoint);

            var appendHeaders = new List<KeyValuePair<string, string>>();
            appendHeaders.Add(new KeyValuePair<string, string>("Authorization", "Bearer " + _applicationConfigHandler.ApplicationConfig.AccountConfig.UserSession.Token));
            var model = new WebApiModel.RootObject { data = new WebApiModel.Data<UserSearchDTO> { type = nameof(UserSearchDTO), attributes = new UserSearchDTO { SearchUser = searchName } }, meta = new WebApiModel.Meta { count = 1 } };
            var result = await this.Request<Shared.Kernel.Application.Model.DataTransferObject.ConcreteImplementation.Jellyfish.UserJellyfishDTO, object>(url: url, method: RestSharp.Method.Post, cancellationToken: cancellationToken, model, null, appendHeaders, false);

            return result;
        }
        public async Task<WebApiHttpRequestResponseModel<Shared.Kernel.Application.Model.DataTransferObject.ConcreteImplementation.Jellyfish.UserJellyfishDTO>> GetOwnProfile(CancellationToken cancellationToken)
        {
            var url = BuildUrl(JellyfishEndpoints.OwnProfileUserEndpoint);

            var appendHeaders = new List<KeyValuePair<string, string>>();
            appendHeaders.Add(new KeyValuePair<string, string>("Authorization", "Bearer " + _applicationConfigHandler.ApplicationConfig.AccountConfig.UserSession.Token));
            var result = await this.Request<Shared.Kernel.Application.Model.DataTransferObject.ConcreteImplementation.Jellyfish.UserJellyfishDTO, object>(url: url, method: RestSharp.Method.Get, cancellationToken: cancellationToken, null, null, appendHeaders, false);
            if (result.IsSuccess)
            {
                var d = result.ApiResponseDeserialized.data.First().attributes;
                _applicationConfigHandler.ApplicationConfig.AccountConfig.User = new Model.User(d);

            }
            return result;
        }
        public async Task<WebApiHttpRequestResponseModel<UserFriendshipRequestDTO>> SendFriendshipRequest(Guid userUuid, CancellationToken cancellationToken)
        {
            var url = BuildUrl(JellyfishEndpoints.AddFriendUserEndpoint);

            var appendHeaders = new List<KeyValuePair<string, string>>();
            appendHeaders.Add(new KeyValuePair<string, string>("Authorization", "Bearer " + _applicationConfigHandler.ApplicationConfig.AccountConfig.UserSession.Token));
            var model = new WebApiModel.RootObject { data = new WebApiModel.Data<UserFriendshipRequestDTO> { type = nameof(UserFriendshipRequestDTO), attributes = new UserFriendshipRequestDTO { TargetUserUuid = userUuid, TargetUserRequestMessage = "Hey, will you be my friend?" } }, meta = new WebApiModel.Meta { count = 1 } };
            var result = await this.Request<UserFriendshipRequestDTO, object>(url: url, method: RestSharp.Method.Post, cancellationToken: cancellationToken, model, null, appendHeaders, false);

            return result;
        }
        public async Task<WebApiHttpRequestResponseModel<MessageDTO>> GetNackMessages(CancellationToken cancellationToken)
        {
            var url = BuildUrl(JellyfishEndpoints.GetNackMessages);

            var appendHeaders = new List<KeyValuePair<string, string>>();
            appendHeaders.Add(new KeyValuePair<string, string>("Authorization", "Bearer " + _applicationConfigHandler.ApplicationConfig.AccountConfig.UserSession.Token));
            var result = await this.Request<MessageDTO, object>(url: url, method: RestSharp.Method.Post, cancellationToken: cancellationToken, null, null, appendHeaders, false);

            return result;
        }

    }
}
