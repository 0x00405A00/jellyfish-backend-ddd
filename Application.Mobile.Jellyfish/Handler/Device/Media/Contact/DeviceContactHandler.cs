using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Communication = Microsoft.Maui.ApplicationModel.Communication;
using MobileApp.Model;
using Perms = Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.ApplicationModel.Communication;
using MobileApp.Handler.Abstraction;
using System.Runtime.CompilerServices;
using MobileApp.Service;
using MobileApp.ViewModel;
using MobileApp.View;
using MobileApp.Controls;
using System.Reflection;

namespace MobileApp.Handler.Device.Media.Contact
{
    public class DeviceContactHandler : AbstractDeviceActionHandler<Permissions.ContactsRead>
    {

        public async Task<List<Microsoft.Maui.ApplicationModel.Communication.Contact>> GetDeviceContacts()
        {
            bool permissions = await AreRequiredPermissionsGranted();
            if (!permissions)
            {
                return null;
            }
            List<Microsoft.Maui.ApplicationModel.Communication.Contact> list = new List<Microsoft.Maui.ApplicationModel.Communication.Contact>();
            var contactsFromPhone = await Microsoft.Maui.ApplicationModel.Communication.Contacts.GetAllAsync();
            if (contactsFromPhone != null)
            {
                foreach (var contact in contactsFromPhone)
                {
                    list.Add(contact);
                }
            }
            return list;
        }
        private readonly NavigationService _navigationService;
        private readonly UserSelectionPageViewModel _userSelectionPageViewModel;
        public DeviceContactHandler(NavigationService navigationService,UserSelectionPageViewModel userSelectionPageViewModel)
        {
            _navigationService = navigationService;
            _userSelectionPageViewModel = userSelectionPageViewModel;
        }
        public async Task OpenUserSelectionHandler(bool multiselection, string messageBusQueue)
        {
            try
            {
                bool permissions = await AreRequiredPermissionsGranted();
                if (!permissions)
                {
                    NotificationHandler.ToastNotify("Abort: No permissions");
                    return;
                }
                _userSelectionPageViewModel.SetResponseQueue(messageBusQueue);
                _userSelectionPageViewModel.IsFriendMultiSelectionEnabled = multiselection;
                var page = new UserSelectionPage(_userSelectionPageViewModel);
                page.BindingContext = _userSelectionPageViewModel;
                await _navigationService.PushAsync(page);
            }
            catch (Exception ex)
            {
                NotificationHandler.ToastNotify("Error: " + ex.Message + "");
            }


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
