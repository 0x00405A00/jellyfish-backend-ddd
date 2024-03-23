namespace Shared.Infrastructure.Backend.Interceptor.Abstraction
{
    public interface IInternalDataInterceptorInvoker
    {
        Task Invoke(params object[] data);
    }
}
