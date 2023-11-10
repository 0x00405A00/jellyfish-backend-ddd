using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Communication = Microsoft.Maui.ApplicationModel.Communication;

namespace MobileApp.Handler.Device.Media.Contact
{
    public interface IDeviceContactHandler
    {
        public Task<List<Microsoft.Maui.ApplicationModel.Communication.Contact>> GetDeviceContacts();
    }
}
