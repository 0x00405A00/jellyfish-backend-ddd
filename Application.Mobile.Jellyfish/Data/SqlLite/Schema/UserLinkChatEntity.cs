using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.Data.SqlLite.Schema
{
    [SQLite.Table("users_related_to_chats")]
    public class UserLinkChatEntity : AbstractEntity
    {
        private int _userId;
        private int _chatId;
        [ForeignKey(typeof(UserEntity))]
        public int UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }
        [ForeignKey(typeof(ChatEntity))]
        public int ChatId
        {
            get { return _chatId; }
            set { _chatId = value; }
        }
    }
}
