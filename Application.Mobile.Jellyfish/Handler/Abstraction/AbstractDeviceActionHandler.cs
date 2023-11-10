using MobileApp.Controls;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls.Compatibility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.Maui.ApplicationModel.Permissions;
using Perms = Microsoft.Maui.ApplicationModel;

namespace MobileApp.Handler.Abstraction
{
    public abstract class AbstractDeviceActionHandler
    {
        //Prüft anhand aller Handler und deren benutzte Permissions, welche Permissions die App im Gesamten braucht um reibungslose zu funktionieren
        public List<T> GetAllAppRequiredPermissions<T>()
            where T : BasePermission, new()
        {
            List<T> response = new List<T>();

            var allHandlersWithDerivedDeviceActionHandler = Assembly.GetExecutingAssembly().GetTypes()?.
                ToList().FindAll(x => x.BaseType == typeof(AbstractDeviceActionHandler) || x.DeclaringType == typeof(AbstractDeviceActionHandler)).FindAll(x => x.IsGenericType);
            if (allHandlersWithDerivedDeviceActionHandler != null && allHandlersWithDerivedDeviceActionHandler.Any())
            {
                foreach (var item in allHandlersWithDerivedDeviceActionHandler)
                {
                    var genericParams = item.GetGenericArguments();
                    if (genericParams.Length > 0)
                    {
                        for (int i = 0; i < genericParams.Length; i++)
                        {
                            if (genericParams[i] == typeof(BasePermission))
                            {
                                var t = genericParams[i] as T;
                                response.Add(t);
                            }
                        }
                    }
                }
            }
            return response;
        }

        public async Task<PermissionStatus> GetPermissionState<PERM>() where PERM : BasePermission, new()
        {
            return await CheckStatusAsync<PERM>();
        }
        public async Task<bool> IsPermissionGranted<PERM>() where PERM : BasePermission, new()
        {
            var perm = await GetPermissionState<PERM>();
            //Limited by iOS
            if (perm.HasFlag(PermissionStatus.Granted | PermissionStatus.Limited) || perm == PermissionStatus.Granted)
            {
                return true;
            }
            return false;
        }
        public async Task<PermissionStatus> GetPermissionFromDeviceUser<PERM>() where PERM : BasePermission, new()
        {
            return await RequestAsync<PERM>();
        }
        public async Task<Hashtable> GetAllPermissionStates()
        {
            Hashtable hashtable = new Hashtable();
            var listOfPermissionREADypes = Assembly.GetExecutingAssembly().GetTypes()?.ToList().
                FindAll(x => x.BaseType == typeof(BasePermission) || x.DeclaringType == typeof(BasePermission));

            if (listOfPermissionREADypes != null && listOfPermissionREADypes.Any())
            {
                foreach (var type in listOfPermissionREADypes)
                {

                    var checkPermission = await (Task<PermissionStatus>)typeof(AbstractDeviceActionHandler).
                        GetMethod("GetPermissionState").MakeGenericMethod(type, type).Invoke(this, new object[] { });
                    hashtable.Add(type, checkPermission);
                }
            }
            return hashtable;
        }
        public abstract Task CheckAndRequestPermission();
        protected abstract Task<bool> AreRequiredPermissionsGranted();
    }
    public abstract class AbstractDeviceActionHandler<PERM1, PERM2, PERM3, PERM4> : AbstractDeviceActionHandler
        where PERM1 : BasePermission, new()
        where PERM2 : BasePermission, new()
        where PERM3 : BasePermission, new()
        where PERM4 : BasePermission, new()
    {
        public Action UserRationalAction { get; protected set; }
        public Action UserDeniedAction { get; protected set; }
        public AbstractDeviceActionHandler() : this(null, null)
        {

        }

        public AbstractDeviceActionHandler(Action userGrantAction, Action userDeniedAction, [CallerMemberName] string caller = "")
        {
            UserRationalAction = userGrantAction;
            UserDeniedAction = userDeniedAction;
            InitDeviceActor(userGrantAction, userDeniedAction, caller);
        }
        protected async Task<PermissionStatus> CheckAndRequestPermission<T>()
            where T : BasePermission, new()
        {
            PermissionStatus status = await Permissions.CheckStatusAsync<T>();

            if (status == PermissionStatus.Granted)
                return status;

            if (status == PermissionStatus.Denied && DeviceInfo.Platform == DevicePlatform.iOS)
            {
                // Prompt the user to turn on in settings
                // On iOS once a permission has been denied it may not be requested again from the application
                if (UserDeniedAction != null)
                    UserDeniedAction.Invoke();
                return status;
            }

            if (Permissions.ShouldShowRationale<T>())
            {
                // Prompt the user with additional information as to why the permission is needed
                NotificationHandler.ToastNotify("Permission: " + nameof(T) + " is needed");
            }

            status = await Permissions.RequestAsync<T>();

            return status;
        }
        public override async Task<PermissionStatus[]> CheckAndRequestPermission()
        {
            PermissionStatus[] permissions = new PermissionStatus[4]
                {
                    await CheckAndRequestPermission<PERM1>(),await CheckAndRequestPermission<PERM2>(),await CheckAndRequestPermission<PERM3>(),await CheckAndRequestPermission<PERM4>()
                };
            return permissions;
        }
        private void InitDeviceActor(Action successAction, Action userDeniedAction,string caller)
        {
            if(successAction == null || userDeniedAction == null) 
            {
                SetUserRationalAction();
                SetUserDeniedAction();
            }

        }

        protected override async Task<bool> AreRequiredPermissionsGranted()
        {
            bool permissions = false;
            var stateCheck = await CheckAndRequestPermission();
            permissions = stateCheck.ToList().FindAll(x => x == PermissionStatus.Granted).Count() == stateCheck.Count();
            return permissions;
        }
        public abstract void SetUserRationalAction();
        public abstract void SetUserDeniedAction();

    }

    public abstract class AbstractDeviceActionHandler<PERM1> : AbstractDeviceActionHandler<PERM1, PERM1, PERM1, PERM1> where PERM1 : BasePermission, new()
    {
        protected AbstractDeviceActionHandler() : base() { }

        protected AbstractDeviceActionHandler(Action userRationalAction, Action userDeniedAction, [CallerMemberName] string caller = "") : base(userRationalAction, userDeniedAction, caller)
        {
        }
        protected new async Task<PermissionStatus> CheckAndRequestPermission()
        {
            return await CheckAndRequestPermission<PERM1>();
        }
    }
    public abstract class AbstractDeviceActionHandler<PERM1, PERM2> : AbstractDeviceActionHandler<PERM1, PERM2, PERM1, PERM1> where PERM1 : BasePermission, new()
        where PERM2 : BasePermission, new()
    {

        protected AbstractDeviceActionHandler() : base() { }

        protected AbstractDeviceActionHandler(Action userRationalAction, Action userDeniedAction, [CallerMemberName] string caller = "") : base(userRationalAction, userDeniedAction, caller)
        {
        }
        protected new async Task<PermissionStatus[]> CheckAndRequestPermission()
        {
            PermissionStatus[] permissions = new PermissionStatus[2]
                {
                    await CheckAndRequestPermission<PERM1>(),await CheckAndRequestPermission<PERM2>() 
                };   
            return permissions;
        }
    }
    public abstract class AbstractDeviceActionHandler<PERM1, PERM2, PERM3> : AbstractDeviceActionHandler<PERM1, PERM2, PERM3, PERM1> where PERM1 : BasePermission, new()
        where PERM2 : BasePermission, new()
        where PERM3 : BasePermission, new()
    {

        protected AbstractDeviceActionHandler() : base() { }

        protected AbstractDeviceActionHandler(Action userRationalAction, Action userDeniedAction, [CallerMemberName] string caller = "") : base(userRationalAction, userDeniedAction, caller)
        {
        }
        protected new async Task<PermissionStatus[]> CheckAndRequestPermission()
        {
            PermissionStatus[] permissions = new PermissionStatus[3]
                {
                    await CheckAndRequestPermission<PERM1>(),await CheckAndRequestPermission<PERM2>(),await CheckAndRequestPermission<PERM3>()
                };
            return permissions;
        }
    }
}
