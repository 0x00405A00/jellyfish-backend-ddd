using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.Handler.Data.InternalDataInterceptor.Abstraction
{
    public interface IInternalDataInterceptor
    {
        public List<IInternalDataInterceptorInvoker> Invoker { get; }
        Task Invoke(params object[] data);
        public void Add(IInternalDataInterceptorInvoker invoker);
        public void Remove(IInternalDataInterceptorInvoker invoker);
    }
}
