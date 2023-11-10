using MobileApp.Data.WebApi;
using MobileApp.Handler.Data.InternalDataInterceptor.Abstraction;
using MobileApp.Handler.Data.InternalDataInterceptor.Invoker;
#if ANDROID
using MobileApp.Handler.Data.InternalDataInterceptor.Invoker.Notification.Android;
using Application.Shared.Kernel.Application.Model.DataTransferObject.ConcreteImplementation.ApiGateway;
#else
using MobileApp.Handler.Data.InternalDataInterceptor.Invoker.Notification.iOS;
using Application.Shared.Kernel.Application.Model.DataTransferObject.ConcreteImplementation.ApiGateway;
#endif

namespace MobileApp.Handler.Data.InternalDataInterceptor
{
    public class InternalDataInterceptorApplication : IInternalDataInterceptorApplication
    {
        private List<IInternalDataInterceptorApplicationInvoker> _interceptors;
        public List<IInternalDataInterceptorApplicationInvoker> Invoker { get => _interceptors; private set => _interceptors = value; }

        List<IInternalDataInterceptorApplicationInvoker> IInternalDataInterceptorApplication.Invoker => throw new NotImplementedException();

        public InternalDataInterceptorApplication()
        {
            Invoker = new List<IInternalDataInterceptorApplicationInvoker>();  

        }

        public void Add(IInternalDataInterceptorApplicationInvoker invoker)
        {
            if ((Invoker.Find(x => x.Equals(invoker)) != null))
                return;
            Invoker.Add(invoker);
        }
        public void Remove(IInternalDataInterceptorApplicationInvoker invoker)
        {
            if (Invoker.Find(x=> x.Equals(invoker)) == null)
                return;
            Invoker.Remove(invoker);
        }
        public IInternalDataInterceptorApplicationInvoker Get<T>() where T : IInternalDataInterceptorApplicationInvoker
        {
            var foundItem = Invoker.Find(x => x.GetType() == typeof(T));
            return foundItem;
        }
        private async Task<DataInterceptorApplicationInvokerResponseModel> ExecAction<T>(Func<T[], Task> func, T[] param) where T : class, new()
        {
            DataInterceptorApplicationInvokerResponseModel dataInterceptorApplicationInvokerResponseModel = new DataInterceptorApplicationInvokerResponseModel();
            dataInterceptorApplicationInvokerResponseModel.Start();
            try
            {
                await func(param);

                dataInterceptorApplicationInvokerResponseModel.IsSuccess = true;
            }
            catch (Exception ex)
            {

                dataInterceptorApplicationInvokerResponseModel.Exception = ex;
            }
            dataInterceptorApplicationInvokerResponseModel.Stop();
            return dataInterceptorApplicationInvokerResponseModel;
        }
        #region BusinessContext

        public async Task<InternalDataInterceptorApplicationInvokeResponseModel> ReceiveMessage(params MessageDTO[] data)
        {
            var response = new InternalDataInterceptorApplicationInvokeResponseModel(Invoker);
            foreach (var item in Invoker)
            {
                response.ExecResponseDictionary[item] = await ExecAction<MessageDTO>(item.ReceiveMessage, data);
            }
            return response;
        }

        public async Task<InternalDataInterceptorApplicationInvokeResponseModel> SendMessage(params MessageDTO[] data)
        {
            var response = new InternalDataInterceptorApplicationInvokeResponseModel(Invoker);
            foreach (var item in Invoker)
            {
                response.ExecResponseDictionary[item] = await ExecAction<MessageDTO>(item.SendMessage, data);
            }
            return response;
        }

        public async Task<InternalDataInterceptorApplicationInvokeResponseModel> CreateFriendRequest(params UserFriendshipRequestDTO[] data)
        {
            var response = new InternalDataInterceptorApplicationInvokeResponseModel(Invoker);
            foreach (var item in Invoker)
            {
                response.ExecResponseDictionary[item] = await ExecAction<UserFriendshipRequestDTO>(item.CreateFriendRequest,data);
            }
            return response;
        }
        public async Task<InternalDataInterceptorApplicationInvokeResponseModel> ReceiveAcceptFriendRequest(params Shared.Kernel.Application.Model.DataTransferObject.ConcreteImplementation.Jellyfish.UserJellyfishDTO[] data)
        {
            var response = new InternalDataInterceptorApplicationInvokeResponseModel(Invoker);
            foreach (var item in Invoker)
            {
                response.ExecResponseDictionary[item] = await ExecAction<Shared.Kernel.Application.Model.DataTransferObject.ConcreteImplementation.Jellyfish.UserJellyfishDTO>(item.ReceiveAcceptFriendRequest, data);
            }
            return response;
        }

        public async Task<InternalDataInterceptorApplicationInvokeResponseModel> ReceiveFriendRequest(params UserFriendshipRequestDTO[] data)
        {
            var response = new InternalDataInterceptorApplicationInvokeResponseModel(Invoker);
            foreach (var item in Invoker)
            {
                response.ExecResponseDictionary[item] = await ExecAction<UserFriendshipRequestDTO>(item.ReceiveFriendRequest, data);
            }
            return response;
        }

        public async Task<Shared.Kernel.Application.Model.DataTransferObject.ConcreteImplementation.Jellyfish.UserJellyfishDTO> GetOwnProfile(CancellationToken cancellationToken)
        {
            Shared.Kernel.Application.Model.DataTransferObject.ConcreteImplementation.Jellyfish.UserJellyfishDTO response = null;
            JellyfishWebApiRestClientInvoker webApiInvoker = (JellyfishWebApiRestClientInvoker)this.Get<JellyfishWebApiRestClientInvoker>();
            var requestResponse = await webApiInvoker.GetOwnProfile(cancellationToken);
            if (requestResponse.IsSuccess)
            {
                response = requestResponse.ApiResponseDeserialized.data.First().attributes;
            }
            return response;
        }

        public async Task<List<UserFriendshipUserModelDTO>> GetFriendshipRequests(CancellationToken cancellationToken)
        {
            var response = new List<UserFriendshipUserModelDTO>();
            JellyfishWebApiRestClientInvoker webApiInvoker = (JellyfishWebApiRestClientInvoker)this.Get<JellyfishWebApiRestClientInvoker>();

            var requestResponse = await webApiInvoker.GetFriendshipRequests(cancellationToken);
            if (requestResponse.IsSuccess)
            {
                foreach (var rq in requestResponse.ApiResponseDeserialized.data)
                {
                    response.Add(rq.attributes);
                }
            }
            return response;
        }
        public async Task<WebApiHttpRequestResponseModel<Shared.Kernel.Application.Model.DataTransferObject.ConcreteImplementation.Jellyfish.UserJellyfishDTO>> GetFriends(CancellationToken cancellationToken)
        {
            var response = new List<UserFriendshipUserModelDTO>();
            JellyfishWebApiRestClientInvoker webApiInvoker = (JellyfishWebApiRestClientInvoker)this.Get<JellyfishWebApiRestClientInvoker>();

            var requestResponse = await webApiInvoker.GetFriends(cancellationToken);
            return requestResponse;
        }

        public async Task<WebApiHttpRequestResponseModel<Shared.Kernel.Application.Model.DataTransferObject.ConcreteImplementation.Jellyfish.UserJellyfishDTO>> AcceptFriendRequest(Guid requestUuid, CancellationToken cancellationToken)
        {
            JellyfishWebApiRestClientInvoker webApiInvoker = (JellyfishWebApiRestClientInvoker)this.Get<JellyfishWebApiRestClientInvoker>();
            return await webApiInvoker.AcceptFriendshipRequests(requestUuid, cancellationToken);
        }

        public async Task<WebApiHttpRequestResponseModel<Shared.Kernel.Application.Model.DataTransferObject.ConcreteImplementation.Jellyfish.UserJellyfishDTO>> SearchUser(string searchUser, CancellationToken cancellationToken)
        {
            JellyfishWebApiRestClientInvoker webApiInvoker = (JellyfishWebApiRestClientInvoker)this.Get<JellyfishWebApiRestClientInvoker>();
            return await webApiInvoker.SearchUser(searchUser, cancellationToken);
        }
        public async Task<WebApiHttpRequestResponseModel<MessageDTO>> GetNackMessages(CancellationToken cancellationToken)
        {
            var response = new List<UserFriendshipUserModelDTO>();
            JellyfishWebApiRestClientInvoker webApiInvoker = (JellyfishWebApiRestClientInvoker)this.Get<JellyfishWebApiRestClientInvoker>();

            var requestResponse = await webApiInvoker.GetNackMessages(cancellationToken);
            return requestResponse;
        }

        #endregion

    }
}
