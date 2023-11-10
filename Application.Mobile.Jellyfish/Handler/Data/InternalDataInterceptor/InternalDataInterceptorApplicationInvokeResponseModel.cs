using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileApp.Handler.Data.InternalDataInterceptor.Abstraction;

namespace MobileApp.Handler.Data.InternalDataInterceptor
{
    public class InternalDataInterceptorApplicationInvokeResponseModel
    {
        public ConcurrentDictionary<IInternalDataInterceptorApplicationInvoker, DataInterceptorApplicationInvokerResponseModel> ExecResponseDictionary { get; private set; }
        public InternalDataInterceptorApplicationInvokeResponseModel(List<IInternalDataInterceptorApplicationInvoker> internalDataInterceptorApplicationInvokers)
        {
            ExecResponseDictionary = new ConcurrentDictionary<IInternalDataInterceptorApplicationInvoker, DataInterceptorApplicationInvokerResponseModel>();
            internalDataInterceptorApplicationInvokers.ForEach(x => { ExecResponseDictionary.TryAdd(x,new DataInterceptorApplicationInvokerResponseModel()); });
        }

    }
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
