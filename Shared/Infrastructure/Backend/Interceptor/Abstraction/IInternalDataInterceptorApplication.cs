using Shared.DataTransferObject.Messenger;

namespace Shared.Infrastructure.Backend.Interceptor.Abstraction
{
    public interface IInternalDataInterceptorApplicationDispatcher
    {
        public IReadOnlyCollection<IInternalDataInterceptorApplicationInvoker> Invoker { get; }
        public Task<InternalDataInterceptorApplicationInvokeResponseModel> ReceiveMessage(params MessageDTO[] data);
        public Task<InternalDataInterceptorApplicationInvokeResponseModel> SendMessage(params MessageDTO[] data);
        public Task<InternalDataInterceptorApplicationInvokeResponseModel> CreateFriendRequest(params UserFriendshipRequestDTO[] data);
        public Task<InternalDataInterceptorApplicationInvokeResponseModel> ReceiveFriendRequest(params UserFriendshipRequestDTO[] data);
        public Task<InternalDataInterceptorApplicationInvokeResponseModel> ReceiveAcceptFriendRequest(params MessengerUserDTO[] data);

        public void Add(IInternalDataInterceptorApplicationInvoker invoker, int priority);
        public void Remove(IInternalDataInterceptorApplicationInvoker invoker);
        public IInternalDataInterceptorApplicationInvoker Get<T>() where T : IInternalDataInterceptorApplicationInvoker;
    }
}
