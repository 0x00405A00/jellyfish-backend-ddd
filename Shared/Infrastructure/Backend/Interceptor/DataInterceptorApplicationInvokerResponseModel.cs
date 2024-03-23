using System.Diagnostics;

namespace Shared.Infrastructure.Backend.Interceptor
{
    public class DataInterceptorApplicationInvokerResponseModel
    {
        public Stopwatch Stopwatch { get; private set; } = new Stopwatch();
        public bool IsRunning { get => Stopwatch.IsRunning; }
        public bool IsSuccess { get; set; } = false;
        public bool ErrorOccured { get => Exception != null; }
        public Exception Exception { get; set; }
        public DataInterceptorApplicationInvokerResponseModel()
        {

        }
        public void Start()
        {
            Stopwatch.Start();
        }
        public void Stop()
        {
            Stopwatch.Stop();
        }
    }
}
