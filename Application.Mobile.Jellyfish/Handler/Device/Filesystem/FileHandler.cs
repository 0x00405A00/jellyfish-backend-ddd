using MobileApp.ApplicationSpecific;
using MobileApp.Handler.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.Handler.Device.Filesystem
{
    public class FileHandler : AbstractDeviceActionHandler<Permissions.StorageRead, Permissions.StorageWrite>
    {
        public FileHandler(Action userRationalAction, Action userDeniedAction, [CallerMemberName] string caller = "") : base(userRationalAction, userDeniedAction, caller)
        {
            Init();
        }

        private async void Init()
        {
            CreateDirIfNotExists(Global.CacheDirectory);
            CreateDirIfNotExists(Global.MediaDirectory);
            CreateDirIfNotExists(Global.MediaPhotosDirectory);
            CreateDirIfNotExists(Global.MediaVideosDirectory);
            CreateDirIfNotExists(Global.LoggingDirectory);
        }

        public void CreateDirIfNotExists(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public async Task<FileResult> SelectFile(PickOptions option)
        {
            try
            {
                bool permissions = await AreRequiredPermissionsGranted();
                if (!permissions)
                {
                    return null;
                }
                var result = await FilePicker.Default.PickAsync(option); 

                return result;
            }
            catch (Exception ex)
            {
            }
            return null;
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
