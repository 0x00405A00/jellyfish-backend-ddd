using Domain.Const;
using Domain.Entities.Auths;
using Domain.Entities.Chats;
using Domain.Entities.Messages;
using Domain.Entities.Roles;
using Domain.Entities.Users.Events;
using Domain.Entities.Users.Exceptions;
using Domain.Primitives;
using Domain.ValueObjects;
using Shared.Entities.Chats;
using Shared.Entities.Users;
using Shared.ValueObjects.Ids;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;

namespace Domain.Entities.Users
{
    public sealed partial class User : AuditableEntity<UserId>
    {
        /// <summary>
        ///  ^                        Start anchor
        /// (?=.*[A-Z].*[A-Z])        Ensure string has two uppercase letters.
        /// (?=.*[!@#$&*])            Ensure string has one special case letter.
        /// (?=.*[0-9].*[0-9])        Ensure string has two digits.
        /// (?=.*[a-z].*[a-z].*[a-z]) Ensure string has three lowercase letters.
        /// .{8}                      Ensure string is of length 8.
        /// $                         End anchor.
        /// </summary>
        public static Dictionary<string, Regex> PasswordPolicy = new Dictionary<string, Regex> {
            {"need two uppercase letters",new Regex("^(.*[A-Z]){2,}") },
            {"need one special case letter",new Regex("^(?=.*[!@#$%^&*()_+{}\\[\\]:;<>,.?~\\\\|]).+")},
            {"need two digits",new Regex("^(?:.*\\d.*){2,}") },
            {"need three lowercase letters",new Regex("^(?:.*[a-z]){3,}") },
            {"need length of minimal 8",new Regex("^.{8,}$") },
        };
        public static int ActivationCodeLen = 4;
        public static TimeSpan PasswordResetExpiresInTime = new TimeSpan(12, 0, 0);
        public static DateTime MinimumBirthDayDate = new DateTime(1900, 1, 1);
        public static DateTime MaximumBirthDayDate = DateTime.Now.AddYears(-16);//man muss mindestens 16 sein

        private ICollection<UserHasRelationToFriend> _friends = new List<UserHasRelationToFriend>();
        private ICollection<UserHasRelationToRole> _roles = new List<UserHasRelationToRole>();


        public UserTypeId UserTypeForeignKey { get; private set; }
        public string UserName { get; protected set; }
        public string Password { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string ActivationToken { get; private set; }
        public string ActivationCode { get; private set; }

        public string PasswordResetCode { get; private set; }
        public string PasswordResetToken { get; private set; }
        public DateTime? PasswordResetExpiresIn { get; private set; }

        public Email Email { get; protected set; }
        public PhoneNumber Phone { get; private set; }
        public Picture Picture { get; private set; }
        public DateOnly DateOfBirth { get; private set; }
        public DateTime? ActivationDateTime { get; private set; }

        public bool HasUserProfilePicture => Picture != null && Picture.FilePath != null;

        private User():base()
        {

        }

        private User(
            UserId userId,
            UserType userType,
            string userName,
            string password,
            string firstName,
            string lastName,
            string activationToken,
            string activationCode,
            string passwordResetCode,
            string passwordResetToken,
            DateTime? passwordResetExpiresIn,
            Email email,
            PhoneNumber phone,
            Picture? picture,
            ICollection<UserHasRelationToRole> roles,
            ICollection<UserHasRelationToFriend>? friends,
            ICollection<FriendshipRequest>? friendshipRequests,
            DateOnly dateOfBirth,
            DateTime? activationDateTime,
            CustomDateTime createdDateTime,
            UserId createdBy,
            CustomDateTime? modifiedDateTime,
            UserId? modifiedBy,
            CustomDateTime? deletedDateTime,
            UserId? deletedBy) : base(userId)
        {
            UserType = userType;
            UserName = userName;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;
            Picture = picture;
            DateOfBirth = dateOfBirth;
            Phone = phone;
            ActivationDateTime = activationDateTime;
            ActivationCode = activationCode;
            ActivationToken = activationToken;
            _friendshipRequests = friendshipRequests ?? new List<FriendshipRequest>();
            _friends = friends ?? new List<UserHasRelationToFriend>();
            _roles = roles ?? new List<UserHasRelationToRole>();
            PasswordResetCode = passwordResetCode;
            PasswordResetExpiresIn = passwordResetExpiresIn;
            PasswordResetToken = passwordResetToken;

            CreatedTime = createdDateTime;
            CreatedByUserForeignKey = createdBy;
            LastModifiedTime = modifiedDateTime;
            LastModifiedByUserForeignKey = modifiedBy;
            DeletedTime = deletedDateTime;
            DeletedByUserForeignKey = deletedBy;
        }
        /// <summary>
        /// Factory Method for creating a User instance with specified attributes.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userType"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="activationToken"></param>
        /// <param name="activationCode"></param>
        /// <param name="passwordResetCode"></param>
        /// <param name="passwordResetToken"></param>
        /// <param name="passwordResetExpiresIn"></param>
        /// <param name="email"></param>
        /// <param name="phone"></param>
        /// <param name="picture"></param>
        /// <param name="roles"></param>
        /// <param name="friends"></param>
        /// <param name="friendshipRequests"></param>
        /// <param name="dateOfBirth"></param>
        /// <param name="activationDateTime"></param>
        /// <param name="createdDateTime"></param>
        /// <param name="createdBy"></param>
        /// <param name="modifiedDateTime"></param>
        /// <param name="modifiedBy"></param>
        /// <param name="deletedDateTime"></param>
        /// <param name="deletedBy"></param>
        /// <returns>A new instance of User</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidDataException"></exception>
        /// <exception cref="InvalidUserNameException"></exception>
        public static User Create(
            UserId id,
            UserType userType,
            string userName,
            string password,
            string firstName,
            string lastName,
            string activationToken,
            string activationCode,
            string passwordResetCode,
            string passwordResetToken,
            DateTime? passwordResetExpiresIn,
            Email email,
            PhoneNumber phone,
            Picture? picture,
            ICollection<UserHasRelationToRole>? roles,
            ICollection<UserHasRelationToFriend>? friends,
            ICollection<FriendshipRequest>? friendshipRequests,
            DateOnly dateOfBirth,
            DateTime? activationDateTime,
            CustomDateTime createdDateTime,
            UserId createdBy,
            CustomDateTime? modifiedDateTime,
            UserId? modifiedBy,
            CustomDateTime? deletedDateTime,
            UserId? deletedBy)
        {
            if (userType == null)
            {
                throw new ArgumentNullException(nameof(userType));
            }
            if (roles != null && roles.Any(x => x == null))
            {
                throw new InvalidDataException($"item of list {nameof(roles)} contains one or more items with null value");
            }
            if (friendshipRequests != null && friendshipRequests.Any(x => x == null))
            {
                throw new InvalidDataException($"item of list {nameof(friendshipRequests)} contains one or more items with null value");
            }
            if (friends != null && friends.Any(x => x == null))
            {
                throw new InvalidDataException($"item of list {nameof(friendshipRequests)} contains one or more items with null value");
            }
            if (userName.ToLower() == email.ToString())
            {
                throw new InvalidUserNameException();
            }
            User user = new User(
                id,
                userType,
                userName,
                password,
                firstName,
                lastName,
                activationToken,
                activationCode,
                passwordResetCode,
                passwordResetToken,
                passwordResetExpiresIn,
                email,
                phone,
                picture,
                roles,
                friends,
                friendshipRequests,
                dateOfBirth,
                activationDateTime,
                createdDateTime,
                createdBy,
                modifiedDateTime,
                modifiedBy,
                deletedDateTime,
                deletedBy);

            return user;
        }

        public static User GetSystemUser()
        {
            UserId userId = new UserId(UserConst.SystemUserGuid);
            UserTypeId userTypeId = new UserTypeId(UserConst.SystemUserTypeGuid);
            UserType userType = UserType.Create(userTypeId, UserConst.SystemUserTypeName, DateTime.Now, null, null, null, null, null);
            return new User(
                userId,
                userType,
                UserConst.SystemUserName,
                string.Empty,
                string.Empty,
                string.Empty,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                DateOnly.MaxValue,
                null,
                DateTime.MaxValue,
                null,
                null,
                null,
                null,
                null);
        }

        private string GenerateCode(int len)
        {
            string actCode = null;
            Random random = new Random();
            for (int i = 0; i < len; i++)
            {
                actCode += random.Next(0, 9);
            }
            return actCode;
        }
        public void GenerateActivationToken()
        {
            ActivationCode = GenerateCode(ActivationCodeLen);
            var bytes = Encoding.UTF8.GetBytes(Guid.NewGuid().ToString());
            var base64 = Convert.ToBase64String(bytes);
            ActivationToken = base64;
        }
        public void Activate(string code, string activationToken)
        {
            if (code != ActivationCode && activationToken != ActivationToken)
            {
                throw new InvalidActivationTry();
            }
            if (ActivationDateTime != null)
            {
                throw new InvalidActivationTry();
            }

            //ActivationDateTime = DateTime.Now;
            Raise(new UserActivatedDomainEvent(this));
        }
        public void NewRegistered()
        {
            Raise(new UserCreatedDomainEvent(this));
        }
        public void UpdateUserType(User modifiedUser, UserType userType)
        {
            if (userType == null)
            {
                throw new ArgumentNullException(nameof(userType));
            }
            UserType = userType;
            SetLastModified(modifiedUser);
            Raise(new UserUpdatedDomainEvent<User, UserId>(this, x => x.Email, userType));
        }

        public void UpdateUserName(User modifiedUser, string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new InvalidUserNameException();
            }
            if (userName.ToLower() == Email.ToString())
            {
                throw new InvalidUserNameException();
            }
            if (UserName == userName)
            {
                return;
            }
            UserName = userName;
            SetLastModified(modifiedUser);
            Raise(new UserUpdatedDomainEvent<User, UserId>(this, x => x.UserName, userName));
        }

        public static void CheckPasswordWithPolicy(string password, string passwordConfirm)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new InvalidPasswordException("password is empty");
            }
            if (string.IsNullOrWhiteSpace(passwordConfirm))
            {
                throw new InvalidPasswordException("passwordConfirm is empty");
            }
            if (password != passwordConfirm)
            {
                throw new InvalidPasswordException("password != passwordConfirm");
            }

            bool[] matches = new bool[PasswordPolicy.Keys.Count];
            int idx1 = 0, idx2 = 0;
            PasswordPolicy.ToList().ForEach(x =>
            {
                Match match = x.Value.Match(password);
                if (match.Success)
                {
                    matches[idx1] = true;
                }
                idx1++;
            });
            if (matches.Where(x => x).Count() != PasswordPolicy.Keys.Count)
            {
                string[] passwordPolicyViolences = new string[matches.Length];

                matches.ToList().ForEach(x =>
                {
                    if (!x)
                    {
                        passwordPolicyViolences[idx2] = PasswordPolicy.Keys.ToList()[idx2];
                    }
                    idx2++;
                });
                throw new InvalidPasswordException(passwordPolicyViolences);
            }
        }

        public void UpdatePassword(User modifiedUser, string password, string passwordConfirm)
        {
            try
            {
                CheckPasswordWithPolicy(password, passwordConfirm);
            }
            catch (InvalidPasswordException ex)
            {
                throw;
            }
            Password = password;
            SetLastModified(modifiedUser);
            Raise(new UserUpdatedDomainEvent<User, UserId>(this, x => x.Password, password));
        }

        public void ResetPasswordRequest()
        {
            PasswordResetCode = GenerateCode(ActivationCodeLen);
            PasswordResetExpiresIn = DateTime.Now.Add(PasswordResetExpiresInTime);
            PasswordResetToken = Convert.ToBase64String(Encoding.UTF8.GetBytes(Random.Shared.Next(1000) + Guid.NewGuid().ToString()));
            Raise(new UserPasswordResetRequestDomainEvent(this));
        }
        public void ResetPassword(string newPassword, string newPasswordConfirm, string base64, string code)
        {
            if (PasswordResetExpiresIn <= DateTime.Now)
            {
                throw new InvalidPasswordException("password-reset action time frame is expired");
            }
            if (PasswordResetToken != base64)
            {
                throw new InvalidPasswordException("this.PasswordResetToken != base64");
            }
            if (PasswordResetCode != code)
            {
                throw new InvalidPasswordException("this.PasswordResetCode != code");
            }
            try
            {

                UpdatePassword(this, newPassword, newPasswordConfirm);
            }
            catch (InvalidPasswordException ex)
            {
                throw;
            }

            Raise(new UserPasswordResetCompletedDomainEvent(this));
        }

        public void UpdateFirstName(User modifiedUser, string firstName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new InvalidFirstNameException();
            }
            if (FirstName == firstName)
            {
                return;
            }
            FirstName = firstName;
            SetLastModified(modifiedUser);
            Raise(new UserUpdatedDomainEvent<User, UserId>(this, x => x.Email, firstName));
        }

        public void UpdateLastName(User modifiedUser, string lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new InvalidLastNameException();
            }
            if (LastName == lastName)
            {
                return;
            }
            LastName = lastName;
            SetLastModified(modifiedUser);
            Raise(new UserUpdatedDomainEvent<User, UserId>(this, x => x.Email, lastName));
        }

        public void UpdateEmail(User modifiedUser, Email email)
        {
            if (email is null)
            {
                throw new ArgumentNullException(nameof(email));
            }
            if (Email == email)
            {
                return;
            }
            Email = email;
            SetLastModified(modifiedUser);
            Raise(new UserUpdatedDomainEvent<User, UserId>(this, x => x.Email, email));
        }

        public void UpdatePhone(User modifiedUser, PhoneNumber phone)
        {
            if (phone is null)
            {
                throw new ArgumentNullException(nameof(phone));
            }
            Phone = phone;
            SetLastModified(modifiedUser);
            Raise(new UserUpdatedDomainEvent<User, UserId>(this, x => x.Phone, phone));
        }

        public void UpdateDateOfBirth(User modifiedUser, DateOnly dateOfBirth)
        {
            DateOfBirth = dateOfBirth;
            SetLastModified(modifiedUser);
            Raise(new UserUpdatedDomainEvent<User, UserId>(this, x => x.DateOfBirth, dateOfBirth));
        }

        public void UpdatePicture(User modifiedUser, Picture picture)
        {
            Picture = picture;
            SetLastModified(modifiedUser);
            Raise(new UserUpdatedDomainEvent<User, UserId>(this, x => x.Picture, picture));
        }

        public void AddFriendshipRequest(FriendshipRequest friendRequest)
        {
            if (_friendshipRequests.Where(x => x.TargetUser == friendRequest.TargetUser && x.RequestUser == friendRequest.RequestUser).Any())
            {
                throw new AddFriendshipRequestException("friendship request already exists");
            }
            _friendshipRequests.Add(friendRequest);
            Raise(new UserCreateFriendshipRequestDomainEvent(this, friendRequest));
        }

        public void RemoveFriendshipRequest(FriendshipRequest friendRequest)
        {
            if (!_friendshipRequests.Where(x => x.TargetUser == friendRequest.TargetUser && x.RequestUser == friendRequest.RequestUser).Any())
            {
                throw new RemoveFriendshipRequestException("friendship request doesnt exists");
            }
            _friendshipRequests.Remove(friendRequest);
            Raise(new UserRemoveFriendshipRequestDomainEvent(this, friendRequest));
        }

        public void AcceptFriendshipRequest(FriendshipRequest friendRequest)
        {
            if (!_friendshipRequests.Where(x => x.TargetUser == friendRequest.TargetUser && x.RequestUser == friendRequest.RequestUser).Any())
            {
                throw new AcceptFriendshipException("friendship request doesnt exists");
            }
            if (_friends.Where(x => x.Friend == friendRequest.RequestUser).Any())
            {
                throw new AcceptFriendshipException("you already friends");
            }
            _friendshipRequests.Remove(friendRequest);
            AddFriend(this, friendRequest.RequestUser);
            Raise(new UserAcceptFriendshipRequestDomainEvent(this, friendRequest));
        }

        public void AddFriend(User execUser, User friend)
        {

            var foundFriend = _friends.Any(x => x.Friend == friend);
            if (foundFriend)
            {
                throw new AddFriendException("you already friends");
            }
            if (friend == this)
            {
                throw new AddFriendException("you cant add yourself to friends");
            }
            var userFriend = UserHasRelationToFriend.Create(UserHasRelationToFriend.NewId(), this, friend, DateTime.Now, null, null, execUser);
            _friends.Add(userFriend);
            Raise(new UserAddFriendDomainEvent(this, friend));
        }

        public void RemoveFriend(User execUser, User friend)
        {
            if (!_friends.Any())
            {
                throw new RemoveFriendException("you have no friends");
            }
            var foundFriend = _friends.Where(x => x.Friend == friend).FirstOrDefault();
            if (foundFriend is null)
            {
                throw new RemoveFriendException("you arent be friends");
            }
            _friends.Remove(foundFriend);
            Raise(new UserRemoveFriendDomainEvent(this, friend));
        }

        public void AddRole(User assignerUser, Role role)
        {
            if (_roles.Where(x => x.Role == role).Any())
            {
                throw new AddRoleException("role already assigned");
            }
            var userRole = UserHasRelationToRole.Create(
                UserHasRelationToRole.NewId(),
                role.Id,
                DateTime.Now,
                null,
                null);
            _roles.Add(userRole);
            SetLastModified(assignerUser);
            Raise(new UserAssignedRoleToUserDomainEvent(assignerUser, this, role));
        }

        public void RemoveRole(User revokerUser, Role role)
        {
            if (!_roles.Any())
            {
                throw new RemoveRoleException("you have no roles assigned");
            }
            var foundUserRole = _roles.Where(x => x.Role == role)
                .FirstOrDefault();
            if (foundUserRole is null)
            {
                throw new RemoveRoleException("role already revoked or is not assigned");
            }
            _roles.Remove(foundUserRole);
            SetLastModified(revokerUser);
            Raise(new UserRevokedRoleToUserDomainEvent(revokerUser, this, role));
        }

        public void Remove(User deletedByUser)
        {
            if (IsDeleted())
            {
                throw new UserAlreadyDeletedException("user is already deleted");
            }
            SetDeleted(deletedByUser);
            Raise(new UserDeletedDomainEvent(this));
        }

        public bool IsFullyRegistered()
        {
            return ActivationDateTime != null && ActivationDateTime != DateTime.MinValue;
        }

        public bool HasBirthday()
        {
            return DateOfBirth == DateOnly.FromDateTime(DateTime.Now);
        }
        public bool HasFriends()
        {
            return Friends.Any();
        }
        public bool IsFriend(User user)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return Friends.Any(x => x.Friend == user);
        }
        /// <summary>
        /// Gets all own requested friendship invites (direction: u to other user)
        /// </summary>
        /// <returns></returns>
        public List<FriendshipRequest> GetRequestedFriendshipRequests()
        {
            return RequestedFriendshipRequests.Where(x => x.AmIRequester(this))
                                          .ToList();
        }
        /// <summary>
        /// Gets all received friendship requests (direction: other user to u)
        /// </summary>
        /// <returns></returns>
        public List<FriendshipRequest> GetReceivedFriendshipRequests()
        {
            return RequestedFriendshipRequests.Where(x => x.AmIReceiver(this))
                                          .ToList();
        }
        public bool HasFriendshipRequests()
        {
            return RequestedFriendshipRequests.Any();
        }
        public bool IsDeleted() => DeletedTime != null;
    }
    public sealed partial class User
    {
        public UserType UserType { get; set; }//<---- Navigation Property must have the accessors get; set;

        public ICollection<UserHasRelationToRole> UserHasRelationToRoles { get => _roles.ToList(); }

        public ICollection<ChatRelationToUser>? ChatRelationToUsers { get; }

        public ICollection<Message>? Messages { get; }
        public ICollection<Message>? CreatedMessages { get; }
        public ICollection<Message>? ModifiedMessages { get; }
        public ICollection<Message>? DeletedMessages { get; }

        /// <summary>
        /// Messages that are not received from user, when received (acknowledged) than the message should be removed from entity 'MessagesInOutbox'
        /// </summary>
        public ICollection<MessageOutbox>? MessagesInOutbox { get; }

        public ICollection<Chat>? CreatedChatRelationToUsers { get; }
        public ICollection<ChatRelationToUser>? ModifiedChatRelationToUsers { get; }
        public ICollection<ChatRelationToUser>? DeletedChatRelationToUsers { get; }

        public ICollection<Role> Roles { get; }

        public ICollection<UserHasRelationToFriend>? UserHasRelationToFriendsLeft { get; }
        public ICollection<UserHasRelationToFriend>? UserHasRelationToFriendsRight { get; }

        public ICollection<UserHasRelationToRole>? CreatedUserHasRelationToRoles { get; }
        public ICollection<UserHasRelationToRole>? ModifiedUserHasRelationToRoles { get; }
        public ICollection<UserHasRelationToRole>? DeletedUserHasRelationToRoles { get; }

        public ICollection<FriendshipRequest>? FriendshipRequestsWhereIamRequester { get; }
        public ICollection<FriendshipRequest>? FriendshipRequestsWhereIamTarget { get; }

        public ICollection<ChatInviteRequest>? ChatInvitesWhereIamRequester { get; }
        public ICollection<ChatInviteRequest>? ChatInvitesWhereIamTarget { get; }

        public ICollection<UserType>? CreatedUserTypes { get; }
        public ICollection<UserType>? ModifiedUserTypes { get; }
        public ICollection<UserType>? DeletedUserTypes { get; }

        public ICollection<Role>? CreatedRoles { get; }
        public ICollection<Role>? ModifiedRoles { get; }
        public ICollection<Role>? DeletedRoles { get; }

        public ICollection<Chat>? CreatedChats { get; }
        public ICollection<Chat>? ModifiedChats { get; }
        public ICollection<Chat>? DeletedChats { get; }

        public ICollection<User>? CreatedUsers { get; }
        public ICollection<User>? ModifiedUsers { get; }
        public ICollection<User>? DeletedUsers { get; }

        public ICollection<Auth>? Auths { get; }
    }
}
