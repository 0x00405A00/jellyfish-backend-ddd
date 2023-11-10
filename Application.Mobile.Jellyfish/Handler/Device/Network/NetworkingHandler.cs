using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MobileApp.Handler.Abstraction;
using Perms = Microsoft.Maui.ApplicationModel;

namespace MobileApp.Handler.Device.Network
{
    public class NetworkingHandler : AbstractDeviceActionHandler<Permissions.NetworkState>
    {
        public NetworkingHandler(Action userRationalAction, Action userDeniedAction, [CallerMemberName] string caller = "") : base(userRationalAction, userDeniedAction, caller)
        {
        }

        public bool IsInternetConnectionGiven()
        {
            var networkAccess = Connectivity.Current.NetworkAccess;
            return networkAccess == NetworkAccess.Internet;
        }
        public NetworkAccess GetCurrentNetworkAccess()
        {
            return Connectivity.Current.NetworkAccess;
        }
        public override void SetUserDeniedAction()
        {
            UserDeniedAction = () => { };
        }

        public override void SetUserRationalAction()
        {
            UserRationalAction = () => { };
        }
    }
}
