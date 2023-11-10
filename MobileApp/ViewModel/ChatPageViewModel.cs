using Microsoft.Maui.ApplicationModel;
//using Plugin.LocalNotification;
//using Plugin.LocalNotification.AndroidOption;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using Microsoft.Maui.ApplicationModel.Communication;
using Microsoft.Maui.Controls.Shapes;
using MobileApp.Model;
using MobileApp.ControlExtension;
using CommunityToolkit.Mvvm.Messaging;
using MobileApp.Service;
using MobileApp.Handler.Device.Media.Camera;
using MobileApp.View;
using MobileApp.Handler.Device.Extension;
using MobileApp.Controls;
using CommunityToolkit.Mvvm.Input;
using MobileApp.Handler.Device.Sensor;
using MobileApp.Handler.Device.Media.Contact;

namespace MobileApp.ViewModel
{
    public class ChatPageViewModel : BaseViewModel
    {
        //private readonly INotificationService _notificationService;
        private Chat _chat;
        public Chat Chat
        {
            get { return _chat; }
        }
        private ObservableCollection<MessageGroup> _messages;
        public ObservableCollection<MessageGroup> Messages
        {
            get
            {
                return _messages;
            }
            set
            {
                _messages = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<CameraMediaModel> TakenPhotos { get; set; }
        private string _text;
        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
                OnPropertyChanged();
            }
        }
        private bool _sendReceiveMessageState;
        public bool SendReceiveMessageState
        {
            get
            {
                return _sendReceiveMessageState
                    ;
            }
            set
            {
                _sendReceiveMessageState = value;
                OnPropertyChanged();
            }
        }
        private bool _isChatRefreshing;
        public bool IsChatRefreshing
        {
            get
            {
                return _isChatRefreshing
                    ;
            }
            set
            {
                _isChatRefreshing = value;
                OnPropertyChanged();
            }
        }
        private Message _selectedMessage;
        public Message SelectedMessage
        {
            get
            {
                return _selectedMessage;
            }
            set
            {
                _selectedMessage = value;
                OnMessageSelected(value);
            }
        }

        private bool _focusLastMessage;
        public bool FocusLastMessage
        {
            get { return _focusLastMessage; }
            set
            {
                _focusLastMessage = value;
                OnPropertyChanged(nameof(FocusLastMessage));
            }
        }
        private bool _expandAttachmentsIsExpanded;
        public bool ExpandAttachmentsIsExpanded
        {
            get { return _expandAttachmentsIsExpanded; }
            set
            {
                _expandAttachmentsIsExpanded = value;
                OnPropertyChanged();
            }
        }
        private bool _expandChatMenusIsExpanded;
        public bool ExpandChatMenuIsExpanded
        {
            get { return _expandChatMenusIsExpanded; }
            set
            {
                _expandChatMenusIsExpanded = value;
                OnPropertyChanged();
            }
        }

        private bool _blockBackSwitch;
        public bool BlockBackSwitch
        {
            get { return _blockBackSwitch; }
            set
            {
                _blockBackSwitch = value;
                OnPropertyChanged();
            }
        }
        private User _selectedContact;
        public User SelectedContact
        {
            get
            {
                return _selectedContact;
            }
            set
            {
                _selectedContact = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsMessagePresetEntitySelected));
                OnPropertyChanged(nameof(IsContactSelected));
            }
        }
        private Location _selectedGpsLocation;
        public Location SelectedGpsLocation
        {
            get
            {
                return _selectedGpsLocation;
            }
            set
            {
                _selectedGpsLocation = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsMessagePresetEntitySelected));
                OnPropertyChanged(nameof(IsGpsLocationSelected));
                OnPropertyChanged(nameof(SelectedGpsLocationString));
            }
        }
        public string SelectedGpsLocationString
        {
            get
            {
                if (_selectedGpsLocation == null)
                    return null;
                string str = string.Format("Lat: {0}, Lon: {1}", _selectedGpsLocation.Latitude, _selectedGpsLocation.Longitude);
                return str;
            }
        }

        public bool IsMessagePresetEntitySelected
        {
            get
            {
                return _selectedGpsLocation != null ||_selectedContact != null ;
            }
        }
        private bool _isChatUserOnline = false;
        public bool IsChatUserOnline
        {
            get
            {
                return _isChatUserOnline;
            }
            private set
            {
                _isChatUserOnline=value;
            }
        }

        public bool IsContactSelected { get => _selectedContact != null; }

        public bool IsGpsLocationSelected { get => _selectedGpsLocation != null; }
        private bool _isLoadingInBackground = false;
        public bool IsLoadingInBackground
        {
            get
            {
                return _isLoadingInBackground;
            }
            private set
            {
                _isLoadingInBackground =value;
                OnPropertyChanged(nameof(IsLoadingInBackground));
            }
        }

        public const string MessageQueueSendContact = "MessageQueueSendContact";
        private readonly CameraHandler _cameraHandler;
        private readonly GpsHandler _gpsHandler;
        private readonly NavigationService _navigationService;
        private readonly ProfilePageViewModel _profilePageViewModel;
        private readonly DeviceContactHandler _deviceContactHandler;
        public ICommand OpenProfilePageCommand { get; private set; }
        public ICommand BackButtonCommand { get; private set; }
        public ICommand MessageSwipeLeftCommand { get; private set; }
        public ICommand MessageSwipeRightCommand { get; private set; }
        public ICommand SendMessageCommand { get; private set; }
        public ICommand RefreshChatViewCommand { get; private set; }
        public ICommand TapLinkCommand { get; private set; }
        public ICommand TapGpsLocationCommand { get; private set; }
        public ICommand ExpandAttachmentsCommand { get; private set; }
        public ICommand ExpandChatsMenuCommand { get; private set; }
        public ICommand TakePhotoCommand { get; private set; }
        public ICommand PlayVideoCommand { get; private set; }
        public ICommand ShowMediaCommand { get; private set; }
        public ICommand VoiceRecordMessageCommand { get; private set; }
        public ICommand SendContactCommand { get; private set; }
        public ICommand SendMediaPhotoCommand { get; private set; }
        public ICommand SendGpsLocationCommand { get; private set; }
        public ChatPageViewModel(NavigationService navigationService,
            CameraHandler cameraHandler,
            GpsHandler gpsHandler,
            DeviceContactHandler deviceContactHandler,
            ProfilePageViewModel profilePageViewModel)//INotificationService notificationService)
        {
            _profilePageViewModel = profilePageViewModel;
            _deviceContactHandler = deviceContactHandler;
            _navigationService = navigationService;
            _cameraHandler = cameraHandler;
            _gpsHandler = gpsHandler;   
            //_notificationService = notificationService;
            MessageSwipeLeftCommand = new RelayCommand(MessageSwipeLeftAction);
            MessageSwipeRightCommand = new RelayCommand<Message>(MessageSwipeRightAction);
            SendMessageCommand = new RelayCommand(SendMessageAction);
            RefreshChatViewCommand = new RelayCommand(RefreshChatViewAction);
            TapLinkCommand = new RelayCommand<Message>(OpenLinkFromMessageAction);
            TapGpsLocationCommand = new RelayCommand<Message>(OpenGpsLocationFromMessageAction);
            ExpandAttachmentsCommand = new RelayCommand(ExpandAttachmentsAction);
            ExpandChatsMenuCommand = new RelayCommand(ExpandChatsMenuAction);
            TakePhotoCommand = new RelayCommand(TakePhoto);
            BackButtonCommand = new RelayCommand(BackButtonAction);
            PlayVideoCommand = new RelayCommand<CameraMediaModel>(PlayVideoAction);
            ShowMediaCommand = new RelayCommand<CameraMediaModel>(ShowMediaAction);
            VoiceRecordMessageCommand = new RelayCommand(VoiceRecordMessageAction);
            SendContactCommand = new RelayCommand(SendContactAction); 
            SendMediaPhotoCommand = new RelayCommand(SendMediaPhotoAction); 
            SendGpsLocationCommand = new RelayCommand(SendGpsLocationAction);
            OpenProfilePageCommand = new RelayCommand(OpenProfilePageAction);
            WeakReferenceMessenger.Default.Register<MessageBus.MessageModel>(this, (r, m) =>
            {
                if(m.Message!= null)
                {
                    switch(m.Message)
                    {
                        case "OnBackButtonPressed":
                            break;
                        case MessageQueueSendContact:
                            if (m.Args != null && m.Args.Length == 1)
                            {
                                UserSelectionPageViewModel response = (UserSelectionPageViewModel)m.Args[0];
                                if (response != null)
                                {
                                    if (response.SelectedUserFriend != null)
                                    {
                                        this.SelectedGpsLocation = null;
                                        this.SelectedContact = null;
                                        this.SelectedContact = response.SelectedUserFriend;
                                    }
                                }
                            }
                            break;
                    }
                }
            });
        }
        private async void SendContactAction()
        {
            this.SelectedGpsLocation = null;
            ExpandAttachmentsIsExpanded = false;
            ExpandChatMenuIsExpanded = false;
            try
            {
                await _deviceContactHandler.OpenUserSelectionHandler(false,MessageQueueSendContact);
                ExpandAttachmentsIsExpanded = false;

            }
            catch (Exception ex)
            {
                NotificationHandler.ToastNotify(ex.ToString());
            }
        }
        private async void OpenProfilePageAction()
        {
            var page = new ProfilePage() ;
            page.BindingContext = _profilePageViewModel;
            await _navigationService.PushAsync(page);
        }
        private async void SendMediaPhotoAction()
        {

        }
        private async void SendGpsLocationAction()
        {
            this.SelectedGpsLocation = null;
            this.SelectedContact = null;
            ExpandAttachmentsIsExpanded = false;
            ExpandChatMenuIsExpanded = false;
            IsLoadingInBackground = true;
            var location = await _gpsHandler.GetCurrentLocation();
            if (location != null)
            {
                this.SelectedGpsLocation = location;
            }
            else
            {
                NotificationHandler.ToastNotify("Abort: Gps need permissions");
            }
            IsLoadingInBackground = false;
        }
        private async void VoiceRecordMessageAction()
        {

        }
        private async void PlayVideoAction(CameraMediaModel data)
        {
            PermissionStatus status = await Permissions.CheckStatusAsync<Permissions.StorageRead>();
            if (status == PermissionStatus.Denied)
            {
                NotificationHandler.ToastNotify("Abort: Show media need permissions");
                return;
            }
            if (data == null)
                return;

            var page = new MediaPlayerPage();
            var vm = new MediaPlayerPageViewModel();
            page.BindingContext = vm;
            vm.SetVideoModel(new List<CameraMediaModel>() { data });
            await _navigationService.PushAsync(page);
        }
        private async void ShowMediaAction(CameraMediaModel data)
        {
            PermissionStatus status = await Permissions.CheckStatusAsync<Permissions.StorageRead>();
            if (status == PermissionStatus.Denied)
            {
                NotificationHandler.ToastNotify("Abort: Show media need permissions");
                return;
            }
            if (data == null)
                return;
            CameraMediaModel mdia = (CameraMediaModel)data;
            var page = new MediaPlayerPage();
            var vm = new MediaPlayerPageViewModel();
            page.BindingContext = vm;

            vm.SetVideoModel(new List<CameraMediaModel> { mdia});
            await _navigationService.PushAsync(page);
        }

        public async void TakePhoto()
        {
            await _cameraHandler.OpenCustomCameraHandler(_navigationService,this);
            ExpandAttachmentsAction();
        }
        public void BackButtonAction()
        {
            if(BlockBackSwitch && !(ExpandChatMenuIsExpanded || ExpandAttachmentsIsExpanded))
            {
                BlockBackSwitch = false;
            }
            else if(BlockBackSwitch && (ExpandChatMenuIsExpanded || ExpandAttachmentsIsExpanded))
            {
                ExpandChatMenuIsExpanded = false;
                ExpandAttachmentsIsExpanded = false;
            }
        }
        public void ExpandChatsMenuAction()
        {
            ExpandChatMenuIsExpanded = !ExpandChatMenuIsExpanded;
            BlockBackSwitch = ExpandChatMenuIsExpanded;
            ExpandAttachmentsIsExpanded = false;
        }
        public void ExpandAttachmentsAction()
        {
            ExpandAttachmentsIsExpanded = !ExpandAttachmentsIsExpanded;
            BlockBackSwitch = ExpandAttachmentsIsExpanded;
            ExpandChatMenuIsExpanded = false;
        }
        public void OnMessageSelected(Message message)
        {
            if (message.IsLink)
            {
                TapLinkCommand.Execute(message);

            }
            else if (message.IsGpsMessage)
            {
                TapGpsLocationCommand.Execute(message);

            }
            else if(message.IsImg)
            {

            }
        }
        public async void OpenLinkFromMessageAction(Message message)
        {
            PermissionStatus status = await Permissions.CheckStatusAsync<Permissions.LaunchApp>();
            if (status == PermissionStatus.Denied)
            {
                NotificationHandler.ToastNotify("Abort: Open url need permissions");
                return;
            }
            try
            {

                await Browser.Default.OpenAsync(message.ExtractedUrlFromText, BrowserLaunchMode.SystemPreferred);
            }
            catch
            {

            }
        }
        public async void OpenGpsLocationFromMessageAction(Message message)
        {
            PermissionStatus status = await Permissions.CheckStatusAsync<Permissions.Maps>();
            if (status == PermissionStatus.Denied)
            {
                NotificationHandler.ToastNotify("Abort: Open location need permissions");
                return;
            }
            try
            {

                await message.Location.OpenMapsAsync();
            }
            catch
            {

            }
        }
        public void MessageSwipeLeftAction()
        {

        }
        public void MessageSwipeRightAction(Message message)
        {

        }

        public void RefreshChatViewAction()
        {
            IsChatRefreshing = true;
            IsChatRefreshing = false;
        }
        public async void SendMessageAction()
        {
            if (Messages == null)
            {
                Messages = new ObservableCollection<MessageGroup>();
            }
            SendReceiveMessageState = !SendReceiveMessageState;//fuer tests


            var curDateTime = DateTime.Now;
            DateOnly msgGrpKey = DateOnly.FromDateTime(curDateTime);
            int index = Messages.IndexOf(x => msgGrpKey == x.Date);
            if (index == -1)
            {


                Messages.Add(new MessageGroup(msgGrpKey));
                index = Messages.Count - 1;
            }
            if (this.TakenPhotos != null && this.TakenPhotos.Count>0)
            {
                foreach (var mediaModel in this.TakenPhotos)
                {

                    var msgMedia = new Message()
                    {
                        Media = mediaModel, 
                        Text = null,
                        Received = SendReceiveMessageState,
                        MessageDateTime = curDateTime,
                        SendToBackend = true,
                    };
                    Messages[index].Add(msgMedia);
                }
            }
            if(!String.IsNullOrWhiteSpace(Text))
            {
                var msg = new Message()
                {
                    Text = Text,
                    Received = SendReceiveMessageState,
                    MessageDateTime = curDateTime,
                    SendToBackend = true,
                    Location = this.SelectedGpsLocation,
                    Contact = this.SelectedContact
                };
                Messages[index].Add(msg);
            }
            else if(this.SelectedGpsLocation != null || this.SelectedContact != null)
            {
                var msg = new Message()
                {
                    Text = null,
                    Received = SendReceiveMessageState,
                    MessageDateTime = curDateTime,
                    SendToBackend = true,
                    Location = this.SelectedGpsLocation,
                    Contact = this.SelectedContact
                };
                Messages[index].Add(msg);
            }
            Text = null;
            RefreshChatViewAction();
            OnPropertyChanged("Messages");
            FocusLastMessage = true;
            this.TakenPhotos = null;
            this.SelectedContact = null;
            this.SelectedGpsLocation = null;
        }

        public void SetChat(Chat chat)
        {
            _chat = chat;
            OnPropertyChanged("Chat");
            if (Messages == null)
            {
                Messages = new ObservableCollection<MessageGroup>();
            }
            foreach (var item in chat.Messages)
            {
                Messages.Add(item);
            }
            OnPropertyChanged("Messages");
        }

    }
}
