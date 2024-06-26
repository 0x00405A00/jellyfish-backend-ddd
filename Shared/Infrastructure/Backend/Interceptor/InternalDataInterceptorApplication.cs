﻿using Domain.ValueObjects;
using Shared.DataTransferObject.Messenger;
using Shared.Infrastructure.Backend.Interceptor.Abstraction;
using System.Collections.Immutable;

namespace Shared.Infrastructure.Backend.Interceptor
{
    /// <summary>
    /// Dispatcher logic for executing <see cref="IInternalDataInterceptorApplicationInvoker"/>
    /// </summary>
    public class InternalDataInterceptorApplicationDispatcher : IInternalDataInterceptorApplicationDispatcher
    {
        private List<IInternalDataInterceptorApplicationInvoker> _interceptors;
        public IReadOnlyCollection<IInternalDataInterceptorApplicationInvoker> Invoker 
        { 
            get => _interceptors.OrderByDescending(x=>x.Priority).ToImmutableList(); 
        }

        public InternalDataInterceptorApplicationDispatcher()
        {
            _interceptors = new List<IInternalDataInterceptorApplicationInvoker>();

        }

        public void Add(IInternalDataInterceptorApplicationInvoker invoker, int priority = -1)
        {
            if (_interceptors.Find(x => x.Equals(invoker)) != null)
                return;
            if(priority != -1)
            {
                invoker.SetPriority(priority);
            }
            _interceptors.Add(invoker);
        }
        public void Remove(IInternalDataInterceptorApplicationInvoker invoker)
        {
            if (_interceptors.Find(x => x.Equals(invoker)) == null)
                return;
            _interceptors.Remove(invoker);
        }
        public IInternalDataInterceptorApplicationInvoker Get<T>() where T : IInternalDataInterceptorApplicationInvoker
        {
            var foundItem = _interceptors.Find(x => x.GetType() == typeof(T));
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
            var response = new InternalDataInterceptorApplicationInvokeResponseModel(_interceptors);
            foreach (var item in Invoker)
            {
                response.ExecResponseDictionary[item] = await ExecAction(item.ReceiveMessage, data);
            }
            return response;    
        }

        public async Task<InternalDataInterceptorApplicationInvokeResponseModel> SendMessage(params MessageDTO[] data)
        {
            var response = new InternalDataInterceptorApplicationInvokeResponseModel(_interceptors);
            foreach (var item in Invoker)
            {
                response.ExecResponseDictionary[item] = await ExecAction(item.SendMessage, data);
            }
            return response;
        }

        public async Task<InternalDataInterceptorApplicationInvokeResponseModel> CreateFriendRequest(params UserFriendshipRequestDTO[] data)
        {
            var response = new InternalDataInterceptorApplicationInvokeResponseModel(_interceptors);
            foreach (var item in Invoker)
            {
                response.ExecResponseDictionary[item] = await ExecAction(item.CreateFriendRequest, data);
            }
            return response;
        }
        public async Task<InternalDataInterceptorApplicationInvokeResponseModel> ReceiveAcceptFriendRequest(params MessengerUserDTO[] data)
        {
            var response = new InternalDataInterceptorApplicationInvokeResponseModel(_interceptors);
            foreach (var item in Invoker)
            {
                response.ExecResponseDictionary[item] = await ExecAction(item.ReceiveAcceptFriendRequest, data);
            }
            return response;
        }

        public async Task<InternalDataInterceptorApplicationInvokeResponseModel> ReceiveFriendRequest(params UserFriendshipRequestDTO[] data)
        {
            var response = new InternalDataInterceptorApplicationInvokeResponseModel(_interceptors);
            foreach (var item in Invoker)
            {
                response.ExecResponseDictionary[item] = await ExecAction(item.ReceiveFriendRequest, data);
            }
            return response;
        }


        #endregion

    }
}
