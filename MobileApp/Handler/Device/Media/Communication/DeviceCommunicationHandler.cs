using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Communication = Microsoft.Maui.ApplicationModel.Communication;
using MobileApp.Model;
using System.Text.RegularExpressions;
using MobileApp.Handler.Abstraction;
using System.Runtime.CompilerServices;

namespace MobileApp.Handler.Device.Media.Communication
{
    public class DeviceCommunicationHandler : AbstractDeviceActionHandler<Permissions.LaunchApp>
    {

        public DeviceCommunicationHandler(Action userRationalAction, Action userDeniedAction, [CallerMemberName] string caller = "") : base(userRationalAction, userDeniedAction, caller)
        {
        }

        public void DialingPhoneNumber(string number)
        {
            if (string.IsNullOrEmpty(number))
                return;
            if (!PhoneNumber.IsPhoneNbr(number))
                return;
            Microsoft.Maui.ApplicationModel.Communication.PhoneDialer.Default.Open(number);
        }

        public override void SetUserDeniedAction()
        {
            throw new NotImplementedException();
        }

        public override void SetUserRationalAction()
        {
            throw new NotImplementedException();
        }

        public static class PhoneNumber
        {
            public const string motif = @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$";

            public static bool IsPhoneNbr(string number)
            {
                if (number != null) return Regex.IsMatch(number, motif);
                else return false;
            }
        }
    }
}
