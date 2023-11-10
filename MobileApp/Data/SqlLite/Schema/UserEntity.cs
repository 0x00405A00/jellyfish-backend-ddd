using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MobileApp.Data.SqlLite.Schema
{
    public class UserEntity : AbstractEntity
    {
        private string _nickName;
        private byte[] _profilePicture = null;
        private int _userId;
        private string _useruUuid;
        private List<ChatEntity> _chats;

        [PrimaryKey, AutoIncrement]
        [SQLite.Column("UserId")]
        public int UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }
        [SQLite.Column("UserUuid")]
        public string UserUuid
        {
            get
            {
                return _useruUuid;
            }
            set
            {
                _useruUuid = value;
            }
        }
        [SQLite.Ignore]
        public Guid UserUuidValue
        {
            get
            {
                return _useruUuid!=null?Guid.Parse(_useruUuid):Guid.Empty;
            }
            set
            {
                _useruUuid = value.ToString();
            }
        }

        [SQLite.Column("NickName")]
        public string NickName
        {
            get
            {
                return _nickName;
            }
            set
            {
                _nickName = value;
            }
        }
        [SQLite.Column("ProfilePicture")]
        public byte[]? ProfilePicture
        {
            get
            {
                return _profilePicture;
            }
            set
            {
                _profilePicture = value;
            }
        }
        /*[ManyToMany(typeof(UserLinkChatEntity), CascadeOperations = CascadeOperation.CascadeRead)]
        public List<ChatEntity> Chats
        {
            get { return _chats; }
            set { _chats = value; }
        }*/
    }
}
