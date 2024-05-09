using Shared.Infrastructure.Backend.Interceptor.Abstraction;

namespace Shared.Infrastructure.Backend.Interceptor
{
    public class InternalDataInterceptorDispatcher : IInternalDataInterceptorDispatcher
    {
        private List<IInternalDataInterceptorInvoker> _interceptors;
        public List<IInternalDataInterceptorInvoker> Invoker { get => _interceptors; private set => _interceptors = value; }

        public InternalDataInterceptorDispatcher()
        {
            _interceptors = new List<IInternalDataInterceptorInvoker>();
        }

        public Task Dispatch(params object[]? data)
        {
            foreach (var item in Invoker)
            {
                item.Invoke(data);
            }
            return Task.CompletedTask;
        }

        public void Add(IInternalDataInterceptorInvoker invoker)
        {
            if (Invoker.Find(x => x.Equals(invoker)) != null)
                return;
            Invoker.Add(invoker);
        }
        public void Remove(IInternalDataInterceptorInvoker invoker)
        {
            if (Invoker.Find(x => x.Equals(invoker)) == null)
                return;
            Invoker.Remove(invoker);
        }
    }
}
