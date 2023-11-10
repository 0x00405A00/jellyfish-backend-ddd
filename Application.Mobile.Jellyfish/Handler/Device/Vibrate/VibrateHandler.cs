using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MobileApp.Handler.Abstraction;
using Vibrate = Microsoft.Maui.ApplicationModel;

namespace MobileApp.Handler.Device.Vibrate
{
    public class VibrateHandler : AbstractDeviceActionHandler<Permissions.Vibrate>
    {
        public const int DefaultVibrationTimeInMilliseconds = 500;

        public VibrateHandler(Action userRationalAction, Action userDeniedAction, [CallerMemberName] string caller = "") : base(userRationalAction, userDeniedAction, caller)
        {
        }

        public async void VibrateDevice(int ms = DefaultVibrationTimeInMilliseconds)
        {
            bool permissions = await AreRequiredPermissionsGranted();
            if (!permissions)
            {
                return;
            }
            Vibration.Default.Vibrate(ms);
        }
        public async void VibrateDevice(TimeSpan timeSpan)
        {
            bool permissions = await AreRequiredPermissionsGranted();
            if (!permissions)
            {
                return;
            }
            VibrateDevice(int.TryParse(timeSpan.TotalMilliseconds.ToString(), out int ms) ? ms : DefaultVibrationTimeInMilliseconds);
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
