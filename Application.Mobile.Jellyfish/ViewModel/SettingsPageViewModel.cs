using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MobileApp.Data.AppConfig.Abstraction;
using MobileApp.Data.AppConfig.ConcreteImplements;
using MobileApp.Handler.AppConfig;
using MobileApp.Handler.Backend.Communication.WebApi;
using MobileApp.Handler.Device.Media;
using MobileApp.Model;
using MobileApp.Service;
using MobileApp.View.SettingsSubPages;
using MobileApp.ViewModel.SettingsSubPage;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Shapes;

namespace MobileApp.ViewModel
{
    public class SettingsPageViewModel : BaseViewModel
    {
        public ObservableCollection<UserFriendshipRequest> UserFriendInvitesList { get; private set; } = new ObservableCollection<UserFriendshipRequest>();

        private UserFriendshipRequest _selectedUserInvite;
        /// <summary>
        /// Currents user friend invite
        /// </summary>
        public UserFriendshipRequest SelectedUserInvite
        {
            get
            {
                return _selectedUserInvite;
            }
            set
            {
                _selectedUserInvite = value;
                OnPropertyChanged(nameof(SelectedUserInvite));
            }
        }
        public User OwnProfile
        {
            get
            {
                return _applicationConfigHandler.ApplicationConfig.AccountConfig.User;
            }
        }
        public bool HasUserFriendInvites { get => UserFriendInvitesList != null && UserFriendInvitesList.Count != 0; }

        private readonly ApplicationConfigHandler _applicationConfigHandler;
        private readonly MobileApp.Handler.Data.InternalDataInterceptor.InternalDataInterceptorApplication _internalDataInterceptorApplication;
        private readonly NavigationService _navigationService;
        public ICommand OpenAccountPage { get; private set; }
        public ICommand LoadedCommand { get; private set; }
        public ICommand OpenChatsPage { get; private set; }
        public ICommand OpenNotificationsPage { get; private set; }
        public ICommand OpenNetworkConfigPage { get; private set; }
        public ICommand AcceptFriendshipInviteCommand { get; private set; }
        private Dictionary<ICommand, AbstractSettingsPageGenericViewModel> _commandsToViewsRelation = new Dictionary<ICommand, AbstractSettingsPageGenericViewModel>();
        public ObservableCollection<SettingsPageSettingItem> SettingsPageSettingItems { get; set; }
        public SettingsPageViewModel(
            MobileApp.Handler.Data.InternalDataInterceptor.InternalDataInterceptorApplication internalDataInterceptorApplication,
            NavigationService navigationService,
            ApplicationConfigHandler applicationConfigHandler,
            ApplicationResourcesHandler applicationResourcesHandler)
        {
            _internalDataInterceptorApplication = internalDataInterceptorApplication;
            _applicationConfigHandler = applicationConfigHandler;
            _navigationService = navigationService;
            LoadedCommand = new RelayCommand(LoadAction);
            OpenAccountPage = new RelayCommand<ICommand>(OpenSubSettingPage);
            OpenChatsPage = new RelayCommand<ICommand>(OpenSubSettingPage);
            OpenNotificationsPage = new RelayCommand<ICommand>(OpenSubSettingPage);
            OpenNetworkConfigPage = new RelayCommand<ICommand>(OpenSubSettingPage);
            AcceptFriendshipInviteCommand = new RelayCommand<UserFriendshipRequest>(AcceptFriendshipInviteAction);
            _commandsToViewsRelation.Add(OpenAccountPage, new SettingsPageGenericViewModel<AccountConfig,AccountConfigViewModel>("Account Settings",navigationService, applicationConfigHandler, new AccountConfigViewModel(applicationConfigHandler.ApplicationConfig.AccountConfig)));
            _commandsToViewsRelation.Add(OpenChatsPage, new SettingsPageGenericViewModel<ChatConfig,ChatConfigViewModel>("Chat Settings", navigationService, applicationConfigHandler, new ChatConfigViewModel(applicationConfigHandler.ApplicationConfig.ChatConfig)));
            _commandsToViewsRelation.Add(OpenNotificationsPage, new SettingsPageGenericViewModel<NotificationConfig,NotificationConfigViewModel>("Notification Settings", navigationService, applicationConfigHandler, new NotificationConfigViewModel(applicationConfigHandler.ApplicationConfig.NotificationConfig)));
            _commandsToViewsRelation.Add(OpenNetworkConfigPage, new SettingsPageGenericViewModel<NetworkConfig,NetworkConfigViewModel>("Network Settings", navigationService, applicationConfigHandler, new NetworkConfigViewModel(applicationConfigHandler.ApplicationConfig.NetworkConfig)));

            PathGeometry failoverValue = (PathGeometry)null;
            SettingsPageSettingItems = new ObservableCollection<SettingsPageSettingItem>()
            {
                new SettingsPageSettingItem{ Title = "Konto", SubTitle = "Sicherheitsbenachrichtigung, Nummer ändern", ExecCommand = OpenAccountPage, SvgPath = failoverValue},
                new SettingsPageSettingItem{ Title = "Chats", SubTitle = "Design, Hintergrund, Chatverlauf", ExecCommand = OpenChatsPage, SvgPath = failoverValue },
                new SettingsPageSettingItem{ Title = "Benachrichtigungen", SubTitle = "Nachrichten-, Gruppen- und Anruftöne", ExecCommand = OpenNotificationsPage, SvgPath = failoverValue },
                new SettingsPageSettingItem{ Title = "Network", SubTitle = "Network Settings", ExecCommand = OpenNetworkConfigPage, SvgPath = failoverValue },
            };
        }

        public override async void InitViewModel()
        {
            base.InitViewModel();
            
        }

        private async void LoadAction()
        {
            if (this.UserFriendInvitesList != null)
            {
                this.UserFriendInvitesList.Clear();
            }
            var responseOwnProfile = await _internalDataInterceptorApplication.GetOwnProfile(CancellationToken.None);

            var getUserFriendshipRequest = await _internalDataInterceptorApplication.GetFriendshipRequests(CancellationToken.None);
            foreach ( var userFriendship in getUserFriendshipRequest)
            {

                this.UserFriendInvitesList.Add(new UserFriendshipRequest(userFriendship));
            }

            OnPropertyChanged(nameof(UserFriendInvitesList));
            OnPropertyChanged(nameof(HasUserFriendInvites));
            OnPropertyChanged(nameof(OwnProfile));
        }

        private async void AcceptFriendshipInviteAction(UserFriendshipRequest user)
        {

            var acceptUserFriendshipRequest = await _internalDataInterceptorApplication.AcceptFriendRequest(user.RequestUuid,CancellationToken.None);
            if(acceptUserFriendshipRequest.IsSuccess)
            {
                if(acceptUserFriendshipRequest.ApiResponseDeserialized.data!=null)
                {
                    var d = acceptUserFriendshipRequest.ApiResponseDeserialized.data.ToList();
                    if(d.Find(x => x.attributes.Uuid == user.UserUuid) != null)
                    {

                        UserFriendInvitesList.Remove(user);
                        OnPropertyChanged(nameof(UserFriendInvitesList));
                        OnPropertyChanged(nameof(HasUserFriendInvites));
                    }
                }
            }


        }
        private void OpenSubSettingPage(ICommand command)
        {
            if(_commandsToViewsRelation.ContainsKey(command))
            {
                var pageVm = (AbstractSettingsPageGenericViewModel)_commandsToViewsRelation[command];
                var page = new SettingsPageGeneric();
                page.BindingContext = pageVm;
                
                _navigationService.PushAsync(page);
                pageVm.RefreshUi(); 
            }
        }

    }
}
