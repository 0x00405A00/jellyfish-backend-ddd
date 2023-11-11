using SQLite;
using SQLiteNetExtensions.Attributes;

namespace MobileApp.Data.SqlLite.Schema
{
    [SQLite.Table("message")]
    public class MessageEntity : AbstractEntity
    {
        private int _msgId;
        private int _userId;
        private UserEntity _user;
        private int _chatId;
        private DateTime _messageDateTime;
        private bool _readed;
        private string _text;
        private string _messageUuid;
        private MessageLocationEntity _location;
        private ChatEntity _chat;

        [PrimaryKey, AutoIncrement]
        [SQLite.Column("MessageId")]
        public int MessageId
        {
            get { return _msgId; }
            set { _msgId = value; }
        }
        [SQLite.Column("MessageUuid")]
        public string MessageUuid
        {
            get
            {
                return _messageUuid;
            }
            set
            {
                _messageUuid = value;
            }
        }
        [SQLite.Ignore]
        public Guid MessageUuidValue
        {
            get
            {
                return _messageUuid != null ? Guid.Parse(_messageUuid) : Guid.Empty;
            }
            set
            {
                _messageUuid = value.ToString();
            }
        }

        [ForeignKey(typeof(ChatEntity))]
        [SQLite.Column("ChatId")]
        public int ChatId
        {
            get { return _chatId; }
            set { _chatId = value; }
        }
        //, welcher die Nachricht abgesetzt hat
        [ForeignKey(typeof(UserEntity))]
        [SQLite.Column("UserId")]
        public int UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }

        /*[OneToOne()]
        public UserEntity User
        {
            get { return _user; }
            set { _user = value; }
        }
        [OneToOne()]
        public MessageLocationEntity Location
        {
            get { return _location; }
            set
            {
                _location = value;
            }
        }*/

        [SQLite.Column("MessageDateTime")]
        public DateTime MessageDateTime
        {
            get { return _messageDateTime; }
            set
            {
                _messageDateTime = value;
            }
        }
        [SQLite.Column("Readed")]
        public bool Readed
        {
            get
            { return _readed; }
            set
            {
                _readed = value;
            }
        }

        [SQLite.Column("Text")]
        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
            }
        }

        public MessageEntity()
        {
            
        }
    }
}
