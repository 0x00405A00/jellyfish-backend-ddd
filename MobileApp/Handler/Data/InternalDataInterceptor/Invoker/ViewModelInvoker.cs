using MobileApp.ControlExtension;
using MobileApp.Controls;
using MobileApp.Handler.Data.InternalDataInterceptor.Abstraction;
using MobileApp.Model;
using MobileApp.ViewModel;
using Shared.DataTransferObject.Messenger;

namespace MobileApp.Handler.Data.InternalDataInterceptor.Invoker
{
    public class ViewModelInvoker : IInternalDataInterceptorApplicationInvoker
    {
        private readonly ChatsPageViewModel _chatsPageViewModel;
        private readonly ChatPageViewModel _chatPageViewModel;
        public ViewModelInvoker(
            ChatsPageViewModel chatsPageViewModel,
            ChatPageViewModel chatPageViewModel)
        {
            _chatPageViewModel = chatPageViewModel;
            _chatsPageViewModel = chatsPageViewModel;
        }
        public Task CreateFriendRequest(params UserFriendshipRequestDTO[] data)
        {
            throw new NotImplementedException();
        }

        public Task ReceiveAcceptFriendRequest(params MessengerUserDTO[] data)
        {
            throw new NotImplementedException();
        }

        public Task ReceiveFriendRequest(params UserFriendshipRequestDTO[] data)
        {
            throw new NotImplementedException();
        }

        public Task ReceiveMessage(params MessageDTO[] data)
        {
            List<MessageDTO> messages =  data.ToList();
            messages.ForEach(message =>
            {
                MainThread.InvokeOnMainThreadAsync(() =>
                {
                    Chat chat = _chatsPageViewModel.Chats.ToList().Find(x => x.ChatUuid == message.ChatUuid);
                    if(chat == null)
                    {

                        chat = new Model.Chat
                        {
                            Name = message.ChatUuid.ToString(),
                            ChatPicture = null,
                            ChatUuid = message.ChatUuid,
                            Messages = new System.Collections.ObjectModel.ObservableCollection<MessageGroup>()
                        };
                        _chatsPageViewModel.Chats.Add(chat);
                    }

                    var messageGrp = new Model.MessageGroup(DateOnly.FromDateTime(message.CreationDateTime));
                    var msg = new Model.Message(message);
                    var messageGroupItemIdx = chat.Messages.ToList().Find(x=>x.SortKey == messageGrp.SortKey);
                    if (messageGroupItemIdx != null)
                    {
                        messageGroupItemIdx.Add(msg);
                    }
                    else
                    {
                        messageGrp.Add(msg);
                        chat.Messages.Add(messageGrp);
                    }
                    chat.OnPropertyChanged(nameof(chat.LastMessageInMessageGroup));
                    chat.Messages = chat.Messages.OrderBy(x => x.SortKey).ToList().ToObservableCollection();
                    _chatsPageViewModel.OnPropertyChanged(nameof(_chatsPageViewModel.AreChatsAvailable));
                    _chatsPageViewModel.OnPropertyChanged(nameof(_chatsPageViewModel.Chats));
                    _chatPageViewModel.RefreshChatViewCommand.Execute(null);    
                    NotificationHandler.ToastNotify(message.Text);
                });
            });
            return Task.CompletedTask;
        }

        public Task SendMessage(params MessageDTO[] data)
        {
            throw new NotImplementedException();
        }
    }
}
