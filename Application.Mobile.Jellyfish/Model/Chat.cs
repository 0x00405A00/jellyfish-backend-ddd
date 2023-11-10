using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileApp.Data.SqlLite.Schema;
using MobileApp.ViewModel;

namespace MobileApp.Model
{
    public class Chat : BaseViewModel
    {
        private List<User> _chatMembers = new List<User>();
        public List<User> ChatMembers
        {
            get { return _chatMembers; }
            set { _chatMembers = value; }
        }
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(); }
        }

        private Guid _chatUuid;
        public Guid ChatUuid
        {
            get
            {
                return _chatUuid;   
            }
            set
            {
                _chatUuid = value;  
            }
        }
        public bool HasChatPicture
        {
            get
            {
                return ChatPicture != null && ChatPicture.Length != null;
            }
        }
        private byte[] _chatPicture;
        public byte[] ChatPicture
        {
            get
            {
                return _chatPicture;
            }
            set
            {
                _chatPicture = value;
            }
        }
        private ObservableCollection<MessageGroup> _messages;
        public ObservableCollection<MessageGroup> Messages
        {
            get { return _messages; }
            set { _messages = value; OnPropertyChanged(); OnPropertyChanged("IsNewConversation"); }
        }
        public MessageGroup LastMessageGroup
        {
            get
            {
                return _messages.LastOrDefault();
            }
        }
        public Message LastMessageInMessageGroup
        {
            get
            {
                return LastMessageGroup != null && LastMessageGroup.Count > 0 ? LastMessageGroup.LastOrDefault() : null;
            }
        }




        public bool IsNewConversation
        {
            get
            {
                return _messages == null || _messages.Count() == 0;
            }
        }

        public Chat()
        {
            
        }
        public Chat(ChatEntity chatEntity)
        {
            this.Name = chatEntity.ChatName;
            if(chatEntity.Users != null)
            {
                foreach(var user in chatEntity.Users)
                {

                    this.ChatMembers.Add(new User(user));
                }
            }
            this.ChatUuid = chatEntity.ChatUuidValue;
        }

    }
}
