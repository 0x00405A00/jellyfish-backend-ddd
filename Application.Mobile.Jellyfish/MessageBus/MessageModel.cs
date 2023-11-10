using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.MessageBus
{
    public class MessageModel
    {
        public string Message { get; private set; }
        public object[] Args { get; private set; }
        public string Caller { get; private set; }  
        
        public MessageModel(string message, object[] args, [CallerMemberName] string caller = "")
        {
            Message = message;
            Args = args;    
            Caller = caller;    
        }
    }
}
