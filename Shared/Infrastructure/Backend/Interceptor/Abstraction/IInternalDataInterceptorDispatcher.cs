namespace Shared.Infrastructure.Backend.Interceptor.Abstraction
{
    public interface IInternalDataInterceptorDispatcher

    {
        public List<IInternalDataInterceptorInvoker> Invoker { get; }
        Task Dispatch(params object[] data);
        public void Add(IInternalDataInterceptorInvoker invoker);
        public void Remove(IInternalDataInterceptorInvoker invoker);
    }
}
