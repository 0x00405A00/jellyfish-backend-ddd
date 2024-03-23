using Shared.Infrastructure.Backend.Interceptor.Abstraction;
using System.Collections.Concurrent;

namespace Shared.Infrastructure.Backend.Interceptor
{
    public class InternalDataInterceptorApplicationInvokeResponseModel
    {
        public ConcurrentDictionary<IInternalDataInterceptorApplicationInvoker, DataInterceptorApplicationInvokerResponseModel> ExecResponseDictionary { get; private set; }
        public InternalDataInterceptorApplicationInvokeResponseModel(List<IInternalDataInterceptorApplicationInvoker> internalDataInterceptorApplicationInvokers)
        {
            ExecResponseDictionary = new ConcurrentDictionary<IInternalDataInterceptorApplicationInvoker, DataInterceptorApplicationInvokerResponseModel>();
            internalDataInterceptorApplicationInvokers.ForEach(x => { ExecResponseDictionary.TryAdd(x, new DataInterceptorApplicationInvokerResponseModel()); });
        }

    }
}
