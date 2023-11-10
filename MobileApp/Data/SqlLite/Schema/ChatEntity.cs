using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace MobileApp.Data.SqlLite.Schema
{
    [SQLite.Table("chat")]
    public class ChatEntity : AbstractEntity
    {
        private int _chatId;
        private string _chatUuid;
        private string _chatName;
        private List<UserEntity> _chatUsers;
        private List<MessageEntity> _messages;

        [PrimaryKey, AutoIncrement]
        public int ChatId
        {
            get { return _chatId; }
            set { _chatId = value; }
        }
        [SQLite.Column("ChatName")]
        public string ChatName
        {
            get { return _chatName; }
            set { _chatName = value; }
        }
        [SQLite.Column("ChatUuid")]
        public string ChatUuid
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
        [SQLite.Ignore]
        public Guid ChatUuidValue
        {
            get
            {
                return _chatUuid!=null?Guid.Parse(_chatUuid):Guid.Empty; 
            }
            set
            {
                _chatUuid = value.ToString();
            }
        }

        [OneToMany()]
        public List<MessageEntity> Messages
        {
            get { return _messages; }
            set { _messages = value; }
        }
        //Ein Chat kann ja auch ein Gruppenchat sein d.h. eine Collection an UserEntites
        [ManyToMany(typeof(UserLinkChatEntity))]
        public List<UserEntity> Users
        {
            get { return _chatUsers; }
            set { _chatUsers = value; }
        }

    }
}
