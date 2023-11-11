using MobileApp.Controls;
using MobileApp.Data.WebApi;
using MobileApp.Handler.Data.InternalDataInterceptor.Abstraction;
using MobileApp.Model;
using Shared.DataTransferObject.Messenger;

namespace MobileApp.Handler.Data.InternalDataInterceptor.Invoker
{
    public class JellyfishWebApiRestClientInvoker : IInternalDataInterceptorApplicationInvoker
    {
        private readonly JellyfishWebApiRestClient _jellyfishWebApiRestClient;
        public JellyfishWebApiRestClientInvoker(JellyfishWebApiRestClient jellyfishWebApiRestClient)
        {
            _jellyfishWebApiRestClient = jellyfishWebApiRestClient;
        }
        public Task CreateFriendRequest(params UserFriendshipRequestDTO[] data)
        {
            throw new NotImplementedException();
        }

        public Task ReceiveAcceptFriendRequest(params MessengerUserDTO[] data)
        {
            throw new NotImplementedException();
        }

        public Task ReceiveFriendRequest(params UserFriendshipRequestDTO[] data)
        {
            throw new NotImplementedException();
        }

        public Task ReceiveMessage(params MessageDTO[] data)
        {
            List<MessageDTO> messages =  data.ToList();
            messages.ForEach(message =>
            {
                MainThread.InvokeOnMainThreadAsync(() =>
                {

                    NotificationHandler.ToastNotify(message.Text);
                });
            });
            return Task.CompletedTask;
        }

        public Task SendMessage(params MessageDTO[] data)
        {
            throw new NotImplementedException();
        }


        public async Task<WebApiHttpRequestResponseModel<MessengerUserDTO>> GetOwnProfile(CancellationToken cancellationToken)
        {
            var result =await _jellyfishWebApiRestClient.GetOwnProfile(cancellationToken);
            return result;
        }

        public async Task<WebApiHttpRequestResponseModel<UserFriendshipRequest>> GetFriendshipRequests(CancellationToken cancellationToken)
        {
            var result =await _jellyfishWebApiRestClient.GetFriendshipRequests(cancellationToken);
            return result;
        }

        public async Task<WebApiHttpRequestResponseModel<MessengerUserDTO>> GetFriends(CancellationToken cancellationToken)
        {
            var result = await _jellyfishWebApiRestClient.GetFriends(cancellationToken);
            return result;
        }
        public async Task<WebApiHttpRequestResponseModel<MessengerUserDTO>> AcceptFriendshipRequests(Guid requestUuid, CancellationToken cancellationToken)
        {

            var result = await _jellyfishWebApiRestClient.AcceptFriendshipRequests(requestUuid,cancellationToken);
            return result;
        }
        public async Task<WebApiHttpRequestResponseModel<MessengerUserDTO>> SearchUser(string searchName, CancellationToken cancellationToken)
        {
            var result = await _jellyfishWebApiRestClient.SearchUser(searchName, cancellationToken);
            return result;
        }
        public async Task<WebApiHttpRequestResponseModel<MessageDTO>> GetNackMessages(CancellationToken cancellationToken)
        {
            var result = await _jellyfishWebApiRestClient.GetNackMessages(cancellationToken);
            return result;
        }
    }
}
