using MobileApp.Handler.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.Handler.Device.ClipBoard
{
    public class ClipBoardHandler : AbstractDeviceActionHandler
    {
        public override Task CheckAndRequestPermission()
        {
            throw new NotImplementedException();
        }

        protected override Task<bool> AreRequiredPermissionsGranted()
        {
            throw new NotImplementedException();
        }
    }
}
