using MobileApp.Data.WebApi;
using Application.Shared.Kernel.Application.Model.DataTransferObject.ConcreteImplementation.ApiGateway;
using Application.Shared.Kernel.Application.Model.DataTransferObject.ConcreteImplementation.Jellyfish;

namespace MobileApp.Handler.Data.InternalDataInterceptor.Abstraction
{
    public interface IInternalDataInterceptorApplication
    {
        public List<IInternalDataInterceptorApplicationInvoker> Invoker { get; }
        public Task<InternalDataInterceptorApplicationInvokeResponseModel> ReceiveMessage(params MessageDTO[] data);
        public Task<InternalDataInterceptorApplicationInvokeResponseModel> SendMessage(params MessageDTO[] data);
        public Task<InternalDataInterceptorApplicationInvokeResponseModel> CreateFriendRequest(params UserFriendshipRequestDTO[] data);
        public Task<InternalDataInterceptorApplicationInvokeResponseModel> ReceiveFriendRequest(params UserFriendshipRequestDTO[] data);
        public Task<InternalDataInterceptorApplicationInvokeResponseModel> ReceiveAcceptFriendRequest(params Shared.Kernel.Application.Model.DataTransferObject.ConcreteImplementation.Jellyfish.UserJellyfishDTO[] data);

        public Task<Shared.Kernel.Application.Model.DataTransferObject.ConcreteImplementation.Jellyfish.UserJellyfishDTO> GetOwnProfile(CancellationToken cancellationToken);
        public Task<List<UserFriendshipUserModelDTO>> GetFriendshipRequests(CancellationToken cancellationToken);
        public Task<WebApiHttpRequestResponseModel<Shared.Kernel.Application.Model.DataTransferObject.ConcreteImplementation.Jellyfish.UserJellyfishDTO>> SearchUser(string searchUser,CancellationToken cancellationToken);
        public Task<WebApiHttpRequestResponseModel<Shared.Kernel.Application.Model.DataTransferObject.ConcreteImplementation.Jellyfish.UserJellyfishDTO>> AcceptFriendRequest(Guid requestUuid,CancellationToken cancellationToken);
        public void Add(IInternalDataInterceptorApplicationInvoker invoker);
        public void Remove(IInternalDataInterceptorApplicationInvoker invoker);
        public IInternalDataInterceptorApplicationInvoker Get<T>() where T: IInternalDataInterceptorApplicationInvoker;
    }
}
