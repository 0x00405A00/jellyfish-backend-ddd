//#define SAMPLE_DATA
using MobileApp.Model;
using MobileApp.Service;
using System.Collections.ObjectModel;
using System.Windows.Input;
using MobileApp.Controls;
using MobileApp.Handler.Data;
using SQLiteNetExtensionsAsync.Extensions;
using MobileApp.Data.SqlLite.Schema;
using MobileApp.Handler.Data.InternalDataInterceptor.Invoker;

namespace MobileApp.ViewModel
{

    public class ChatsPageViewModel : BaseViewModel
    {
        private readonly NavigationService _navigationService;
        private readonly JellyfishSqlliteDatabaseHandler _jellyfishSqlliteDatabaseHandler;
        private readonly MobileApp.Handler.Data.InternalDataInterceptor.InternalDataInterceptorApplication _internalDataInterceptorApplication;
        private readonly IServiceProvider _serviceProvider;
        private ObservableCollection<Chat> _chats = new ObservableCollection<Chat>();
        public ObservableCollection<Chat> Chats
        { get { return _chats; } }
        public string Test { get; set; } = "Mein Test Str ChatsPageViewModel";
        private Chat _SelectedChat;
        public Chat SelectedChat
        {
            get
            {
                return _SelectedChat;
            }
            set
            {
                _SelectedChat = value;
            }
        }

        private bool _selectedView = false;
        public bool SelectedView
        {
            get { return _selectedView; }
            set
            {
                _selectedView = value;
                OnPropertyChanged(nameof(SelectedView));
            }
        }

        public bool AreChatsAvailable
        {
            get
            {
                return (Chats != null && Chats.Count != 0) ? true : false ;
            }
        }
        private bool _isChatsListRefreshing;
        public bool IsChatsListRefreshing
        {
            get
            {
                return _isChatsListRefreshing
                    ;
            }
            set
            {
                _isChatsListRefreshing = value;
                OnPropertyChanged();
            }
        }
        public ICommand SelectedChatChangedCommand { get; private set; }
        public ICommand TabChatCommand { get; private set; }
        public ICommand DeleteChatCommand { get; private set; }
        public ICommand RefreshChatsViewCommand { get; private set; }
        public ICommand SwipeLeftCommand { get; private set; }
        public ICommand SwipeRightCommand { get; private set; }

        public ChatsPageViewModel(
            IServiceProvider serviceProvider,
            JellyfishSqlliteDatabaseHandler jellyfishSqlliteDatabaseHandler,
            MobileApp.Handler.Data.InternalDataInterceptor.InternalDataInterceptorApplication internalDataInterceptorApplication,
            NavigationService navigationService)
        {
            _jellyfishSqlliteDatabaseHandler = jellyfishSqlliteDatabaseHandler;
            _internalDataInterceptorApplication = internalDataInterceptorApplication;   
            _serviceProvider = serviceProvider;
            _navigationService = navigationService;
            DeleteChatCommand = new RelayCommand<Chat>(DeleteChatAction);
            RefreshChatsViewCommand = new RelayCommand(RefreshChatsViewAction);
            SelectedChatChangedCommand = new RelayCommand(SelectedChatChangedAction);
            TabChatCommand = new RelayCommand<Chat>(TabChatAction);

            Init();
        }
        public override async void InitViewModel()
        {
            base.InitViewModel();


            #region TEST_DATA
            /*var insertUserResponse = await _jellyfishSqlliteDatabaseHandler.Insert<UserEntity>(new UserEntity { UserId = 1, NickName="Pablo" });
            var insertChatResponse = await  _jellyfishSqlliteDatabaseHandler.Insert<ChatEntity>(new ChatEntity { ChatId = 1 });

            var insertChatRelationToUserResponse = await _jellyfishSqlliteDatabaseHandler.Insert<UserLinkChatEntity>(new UserLinkChatEntity { ChatId = 1, UserId = 1 });

            var insertMsgResponse = await  _jellyfishSqlliteDatabaseHandler.Insert<MessageEntity>(new MessageEntity { ChatId = 1, Text = "meine erste nachricht", UserId = 1, Readed=false, MessageId=1, MessageDateTime = DateTime.Now });*/
            #endregion
            try
            {

                var chats = await _jellyfishSqlliteDatabaseHandler.DatabaseHandle.GetAllWithChildrenAsync<ChatEntity>();
                var users = await _jellyfishSqlliteDatabaseHandler.Select<UserEntity>();

                foreach (var chat in chats)
                {
                    var chatItemForVm = new Chat(chat);
                    chatItemForVm.Messages = new ObservableCollection<MessageGroup>();
                    if (chat.Messages != null)
                    {
                        foreach (var msg in chat.Messages)
                        {
                            var compareModel = new MessageGroup(DateOnly.FromDateTime(msg.MessageDateTime));
                            var msgGrp = chatItemForVm.Messages.ToList().Find(x => x.SortKey == compareModel.SortKey);
                            if (msgGrp == null)
                            {
                                chatItemForVm.Messages.Add(compareModel);
                            }
                            Message message = new Message(msg);
                            compareModel.Add(message);
                        }
                    }
                    this.Chats.Add(chatItemForVm);
                }

                OnPropertyChanged(nameof(AreChatsAvailable));
                OnPropertyChanged(nameof(Chats));
            }
            catch(Exception ex) 
            {

            }
        }
        public async void Init()
        {
            InitViewModel();
        }

        public void SelectedChatChangedAction()
        {

        }
        public void TabChatAction(Chat chat)
        {
            OnChatSelected(chat);
        }
        public void DeleteChatAction(Chat chat)
        {

            _chats.Remove(chat);
            OnPropertyChanged(nameof(AreChatsAvailable));
            OnPropertyChanged(nameof(Chats));
        }

        public void RefreshChatsViewAction()
        {
            IsChatsListRefreshing = true;
            IsChatsListRefreshing = false;
        }

        private async void OnChatSelected(Chat chat)
        {
            try
            {

                var vmFromDi = _serviceProvider.GetService<ChatPageViewModel>();
                ChatPage chatPage = new ChatPage(vmFromDi);
                var vm = chatPage.BindingContext as ChatPageViewModel;
                vm.SetChat(chat);
                await _navigationService.PushAsync(chatPage);
                vm.FocusLastMessage = false;
                vm.FocusLastMessage = true;
                this._SelectedChat = null;//Reset that Chat is clickable/tapable after return from chat to chats view
                OnPropertyChanged(nameof(SelectedChat));
            }
            catch(Exception ex) {
                NotificationHandler.DisplayAlert("exc",ex.Message+ ":"+ex.Source,null,cancel:"cancel");
            }

        }

        public async Task<ObservableCollection<Chat>> LoadChats()
        {


#if SAMPLE_DATA
            Task<ObservableCollection<Chat>> task = Task.Factory.StartNew(() =>
            {
                ObservableCollection<Chat> chats = new ObservableCollection<Chat>();
                Random random = new Random();

                for (int i = 0; i < 50; i++)
                {
                    int randomChatCount = random.Next(1, 44);
                    var chat = new Chat() { Name = "User" + i + "", Messages = new ObservableCollection<MessageGroup>() };
                    bool sendMessage = false;
                    List<Message> messages = new List<Message>();
                    for (int j = 0; j < randomChatCount; j++)
                    {
                        sendMessage = !sendMessage;
                        var msg = new Message() { Text = "random+" + i + ",demo-msg-id:" + j, Received = sendMessage, MessageDateTime = DateTime.Now, SendToBackend = true };
                        messages.Add(msg);
                    }
                    var msg2 = new Message() { Text = "1111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111", Received = sendMessage, MessageDateTime = DateTime.Now.AddDays(-random.Next(0, 50)), SendToBackend = true };
                    var msg3 = new Message() { Text = "https://www.google.com", Received = sendMessage, MessageDateTime = DateTime.Now, SendToBackend = true };
                    var msg4 = new Message() { Location = new Location(36.9628066, -122.0194722), Text = "komm hier hin", Received = true, MessageDateTime = DateTime.Now, SendToBackend = true };
                    messages.Add(msg2);
                    messages.Add(msg3);
                    messages.Add(msg4);
                    List<MessageGroup> messageGrp = new List<MessageGroup>();
                    foreach (Message m in messages)
                    {
                        DateOnly key = DateOnly.FromDateTime(m.MessageDateTime);
                        int index = messageGrp.IndexOf(x => x.Date == key);
                        if (index == -1)
                        {
                            messageGrp.Add(new MessageGroup(key));
                            index = messageGrp.Count - 1;
                        }
                        messageGrp[index].Add(m);

                    }
                    chat.Messages = messageGrp.OrderBy(x => x.SortKey).ToList().ToObservableCollection();
                    chats.Add(chat);

                }
                return chats;
            });
            return await task;
#else

            return new ObservableCollection<Chat>();
#endif

        }

    }
}
