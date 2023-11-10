using MobileApp.Handler.Data.InternalDataInterceptor.Abstraction;
using Application.Shared.Kernel.Application.Model.DataTransferObject.ConcreteImplementation.ApiGateway;
using Application.Shared.Kernel.Application.Model.DataTransferObject.ConcreteImplementation.Jellyfish;

namespace MobileApp.Handler.Data.InternalDataInterceptor.Invoker.Notification
{
    public class PlatformIndependentNotificationInvoker : IInternalDataInterceptorApplicationInvoker
    {
        public Task CreateFriendRequest(params UserFriendshipRequestDTO[] data)
        {
            throw new NotImplementedException();
        }

        public Task ReceiveAcceptFriendRequest(params Shared.Kernel.Application.Model.DataTransferObject.ConcreteImplementation.Jellyfish.UserJellyfishDTO[] data)
        {
            throw new NotImplementedException();
        }

        public Task ReceiveFriendRequest(params UserFriendshipRequestDTO[] data)
        {
            throw new NotImplementedException();
        }

        public Task ReceiveMessage(params MessageDTO[] data)
        {
            throw new NotImplementedException();
        }

        public Task SendMessage(params MessageDTO[] data)
        {
            throw new NotImplementedException();
        }
    }
}
