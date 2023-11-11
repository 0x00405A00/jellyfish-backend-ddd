using MobileApp.Data.SqlLite.Schema;
using MobileApp.ViewModel;
using Shared.DataTransferObject.Messenger;

namespace MobileApp.Model
{
    public class User :BaseViewModel
    {
        private string _nickName;
        private byte[] _profilePicture = null;
        private int _userId;
        private Guid _userUuid;
        public int UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }
        public Guid UserUuid
        {
            get { return _userUuid; }
            set { _userUuid = value; }
        }

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
        public string Firstletter
        {
            get
            {
                return this.NickName.Substring(0,2).ToUpper();
            }
        }
        private string _status = "Hello iam using MobileApp!";
        public string Status
        {
            get
            {
                return _status;
            }
            set
            {
                _status = value;
                OnPropertyChanged(nameof(Status));
                OnPropertyChanged(nameof(StatusDisplay));
            }
        }
        public string StatusDisplay
        {
            get
            {
                return string.Format("\"{0}\"", _status);
            }
        }
        public bool HasProfilePicture
        {
            get
            {
                return ProfilePicture != null && ProfilePicture.Length != null;
            }
        }
        public byte[] ProfilePicture
        {
            get
            {
                return _profilePicture;
            }
            set
            {
                _profilePicture = value;
                OnPropertyChanged(nameof(HasProfilePicture));
            }
        }
        private bool _isVisible = true;

        public bool IsVisible
        {
            get
            {
                return _isVisible;

            }
            set
            {
                _isVisible = value;
                OnPropertyChanged(nameof(IsVisible));
            }
        }
        private bool _isSelected = false;

        public bool IsSelected
        {
            get
            {
                return _isSelected;

            }
            set
            {
                _isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
            }
        }
        private bool _openFriendshipRequest = false;

        public bool HasOpenFriendshipRequest
        {
            get
            {
                return _openFriendshipRequest;

            }
            set
            {
                _openFriendshipRequest = value;
                OnPropertyChanged(nameof(HasOpenFriendshipRequest));
            }
        }

        public User()
        {

        }
        public User(MessengerUserDTO userDTO)
        {
            this.NickName = userDTO.User;
            this.ProfilePicture = userDTO.Picture != null ? Convert.FromBase64String(userDTO.Picture) : null;
            this.UserUuid = userDTO.Uuid;
        }
        public User(UserEntity userEntity)
        {
            this.NickName = userEntity.NickName;
            this.ProfilePicture = userEntity.ProfilePicture;
            this.UserId = userEntity.UserId;
            this.UserUuid = userEntity.UserUuidValue;
        }
    }
}
