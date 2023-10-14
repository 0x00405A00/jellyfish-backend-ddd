using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Error
{
    public class Error
    {
        public string Message { get; private set; }

        public Error(string message)
        {
            Message = message;
        }
    }
}
