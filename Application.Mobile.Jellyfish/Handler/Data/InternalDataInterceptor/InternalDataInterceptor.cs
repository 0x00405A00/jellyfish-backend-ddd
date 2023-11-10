using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileApp.Handler.Data.InternalDataInterceptor.Abstraction;

namespace MobileApp.Handler.Data.InternalDataInterceptor
{
    public class InternalDataInterceptor : IInternalDataInterceptor
    {
        private List<IInternalDataInterceptorInvoker> _interceptors;
        public List<IInternalDataInterceptorInvoker> Invoker { get => _interceptors; private set => _interceptors = value; }

        public InternalDataInterceptor()
        {
            Invoker = new List<IInternalDataInterceptorInvoker>();  
        }

        public Task Invoke(params object[]? data)
        {
            foreach(var item in Invoker)
            {
                item.Invoke(data);
            }
            return Task.CompletedTask;
        }

        public void Add(IInternalDataInterceptorInvoker invoker)
        {
            if ((Invoker.Find(x => x.Equals(invoker)) != null))
                return;
            Invoker.Add(invoker);
        }
        public void Remove(IInternalDataInterceptorInvoker invoker)
        {
            if (Invoker.Find(x=> x.Equals(invoker)) == null)
                return;
            Invoker.Remove(invoker);
        }
    }
}
