using MobileApp.Data.WebApi;
using Shared.DataTransferObject.Messenger;

namespace MobileApp.Handler.Data.InternalDataInterceptor.Abstraction
{
    public interface IInternalDataInterceptorApplication
    {
        public List<IInternalDataInterceptorApplicationInvoker> Invoker { get; }
        public Task<InternalDataInterceptorApplicationInvokeResponseModel> ReceiveMessage(params MessageDTO[] data);
        public Task<InternalDataInterceptorApplicationInvokeResponseModel> SendMessage(params MessageDTO[] data);
        public Task<InternalDataInterceptorApplicationInvokeResponseModel> CreateFriendRequest(params UserFriendshipRequestDTO[] data);
        public Task<InternalDataInterceptorApplicationInvokeResponseModel> ReceiveFriendRequest(params UserFriendshipRequestDTO[] data);
        public Task<InternalDataInterceptorApplicationInvokeResponseModel> ReceiveAcceptFriendRequest(params MessengerUserDTO[] data);

        public Task<MessengerUserDTO> GetOwnProfile(CancellationToken cancellationToken);
        public Task<List<UserFriendshipRequestDTO>> GetFriendshipRequests(CancellationToken cancellationToken);
        public Task<WebApiHttpRequestResponseModel<MessengerUserDTO>> SearchUser(string searchUser,CancellationToken cancellationToken);
        public Task<WebApiHttpRequestResponseModel<MessengerUserDTO>> AcceptFriendRequest(Guid requestUuid,CancellationToken cancellationToken);
        public void Add(IInternalDataInterceptorApplicationInvoker invoker);
        public void Remove(IInternalDataInterceptorApplicationInvoker invoker);
        public IInternalDataInterceptorApplicationInvoker Get<T>() where T: IInternalDataInterceptorApplicationInvoker;
    }
}
