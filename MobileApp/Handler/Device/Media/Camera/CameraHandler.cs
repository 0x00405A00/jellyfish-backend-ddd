using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MobileApp.Controls;
using MobileApp.Handler.Abstraction;
using MobileApp.Handler.Device.Extension;
using MobileApp.Service;
using MobileApp.View;
using MobileApp.ViewModel;
using AppModel = Microsoft.Maui.ApplicationModel;

namespace MobileApp.Handler.Device.Media.Camera
{
    public class CameraHandler : AbstractDeviceActionHandler<Permissions.Camera, Permissions.StorageRead, Permissions.StorageWrite>
    {
        public CameraHandler(Action userRationalAction, Action userDeniedAction, [CallerMemberName] string caller = "") : base(userRationalAction, userDeniedAction, caller)
        {
        }

        public virtual async Task<byte[]> CapturePhoto()
        {
            bool permissions = await AreRequiredPermissionsGranted();
            if (!permissions)
            {
                return null;
            }
            byte[] data = null;
            FileResult media = await MediaPicker.Default.CapturePhotoAsync();
            data = await media.ReadAllBytes();
            return data;
        }
        public virtual async Task<byte[]> CaptureVideo()
        {
            bool permissions = await AreRequiredPermissionsGranted();
            if (!permissions)
            {
                return null;
            }
            byte[] data = null;
            FileResult media = await MediaPicker.Default.CaptureVideoAsync();
            data = await media.ReadAllBytes();
            return data;
        }
        public async Task OpenCustomCameraHandler(NavigationService navigationService,ChatPageViewModel chatPageViewModel)
        {
            try
            {
                bool permissions = await AreRequiredPermissionsGranted();
                if(!permissions)
                {
                    return;
                }
                var vm = new CameraHandlerPageViewModel(navigationService, chatPageViewModel);
                var page = new CameraHandlerPage();
                page.BindingContext = vm;
                await navigationService.PushAsync(page);
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

        public bool HasCamera
        {
            get
            {
                return MediaPicker.Default.IsCaptureSupported;
            }
        }
    }
}
