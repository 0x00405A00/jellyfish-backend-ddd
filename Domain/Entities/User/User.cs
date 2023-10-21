using Domain.Const;
using Domain.Entities.User.Event;
using Domain.Entities.User.Exception;
using Domain.Primitives;
using Domain.ValueObjects;
using System.Data;
using System.Text.RegularExpressions;

namespace Domain.Entities.User
{
    public sealed class User : Entity<UserId>,IAuditibleCreateEntity,IAuditibleModifiedEntity,IAuditibleDeleteEntity
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
        public static Dictionary<string,Regex> PasswordPolicy = new Dictionary<string, Regex> {
            {"Two uppercase letters",new Regex("^(.*[A-Z]){2,}") },
            {"One special case letter",new Regex("^(?=.*[!@#$%^&*()_+{}\\[\\]:;<>,.?~\\\\|]).+")},
            {"Two digits",new Regex("^(?:.*\\d.*){2,}") },
            {"Three lowercase letters",new Regex("^(?:.*[a-z]){3,}") },
            {"Length of minimal 8",new Regex("^.{8,}$") },
        };

        private ICollection<UserFriend> _friends = new List<UserFriend>();
        public ICollection<UserFriend> Friends { get => _friends?.ToList(); }
        private ICollection<FriendshipRequest> _friendshipRequests = new List<FriendshipRequest>();
        public ICollection<FriendshipRequest> FriendshipRequests { get => _friendshipRequests?.ToList(); }
        private ICollection<UserRole> _roles = new List<UserRole>();
        public ICollection<UserRole> Roles { get => _roles.ToList(); }

        public UserType UserType { get;private set; }
        public string UserName { get; protected set; }
        public string Password { get; private set; }
        public string FirstName { get;private set; }
        public string LastName { get;private set; }
        public Email Email { get; protected set; }
        public PhoneNumber Phone { get; private set; }
        public Picture Picture { get; private set; }
        public DateOnly DateOfBirth { get; private set; }
        public DateTime? ActivationDateTime { get; private set; }

        public DateTime? CreatedTime { get;private set; }
        public DateTime? LastModifiedTime { get;private set; }
        public DateTime? DeletedTime { get;private set; }
        public User? CreatedByUser { get;private set; }
        public User? LastModifiedByUser { get;private set; }
        public User? DeletedByUser { get;private set; }

        private User()
        {
            
        }

        private User(UserId userId,
                    UserType userType,
                    string userName,
                    string password,
                    string firstName,
                    string lastName,
                    Email email,
                    PhoneNumber phone,
                    Picture? picture,
                    ICollection<UserRole> roles,
                    ICollection<UserFriend>? friends,
                    ICollection<FriendshipRequest>? friendshipRequests,
                    DateOnly dateOfBirth,
                    DateTime? activationDateTime,
                    DateTime createdTime,
                    DateTime? lastModifiedTime,
                    DateTime? deletedTime) : base(userId)
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
            CreatedTime = createdTime;
            ActivationDateTime = activationDateTime;
            LastModifiedTime = lastModifiedTime;
            DeletedTime = deletedTime;
            _friendshipRequests = friendshipRequests ?? new List<FriendshipRequest>();
            _friends = friends??new List<UserFriend>();
            _roles = roles??new List<UserRole>();
        }

        /// <summary>
        /// Factory Method for creating a User
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userType"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="email"></param>
        /// <param name="phone"></param>
        /// <param name="dateOfBirth"></param>
        /// <param name="activationDateTime"></param>
        /// <param name="createdTime"></param>
        /// <param name="lastModifiedTime"></param>
        /// <param name="deletedTime"></param>
        /// <returns></returns>
        public static User Create(
            UserId id,
            UserType userType,
            string userName,
            string password,
            string firstName,
            string lastName,
            Email email,
            PhoneNumber phone,
            Picture? picture,
            ICollection<UserRole>? roles,
            ICollection<UserFriend>? friends,
            ICollection<FriendshipRequest>? friendshipRequests,
            DateOnly dateOfBirth,
            DateTime? activationDateTime,
            DateTime createdTime,
            DateTime? lastModifiedTime,
            DateTime? deletedTime,
            User createdBy)
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
                email,
                phone,
                picture,
                roles,
                friends,
                friendshipRequests,
                dateOfBirth,
                activationDateTime,
                createdTime,
                lastModifiedTime,
                deletedTime);
            user.SetCreated(createdBy);

            user.Raise(new UserCreatedDomainEvent(user));
            return user;
        }

        public static User GetSystemUser()
        {
            UserId userId = new UserId(UserConst.SystemUserGuid);
            UserTypeId userTypeId = new UserTypeId(UserConst.SystemUserTypeGuid);
            UserType userType = UserType.Create(userTypeId,UserConst.SystemUserTypeName,DateTime.Now,null,null);
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
                DateOnly.MaxValue,
                null,
                DateTime.MaxValue,
                null,
                null);
        }
        /*protected override void Update<TEntity,Id>(Expression<Func<TEntity, object>> propertyExpression, object value)
        {
            base.Update(propertyExpression, value);
            Raise(new UpdatedDomainEvent<TEntity, Id>(this, propertyExpression, value));
        }

        protected override void Update<TEntity>(params KeyValuePair<Expression<Func<TEntity, object>>, object>[] propertyExpression)
        {
            base.Update(propertyExpression);

            propertyExpression.ToList().ForEach(e => {
                Raise(new UserUpdatedDomainEvent<User, UserId>(this, e.Key, e.Value));
            });
        }*/

        public void UpdateUserType(User modifiedUser, UserType userType)
        {
            if(userType == null)
            {
                throw new ArgumentNullException(nameof(userType));
            }
            UserType = userType;
            SetLastModified(modifiedUser);
            Raise(new UserUpdatedDomainEvent<User, UserId>(this, x => x.Email, userType));
        }

        public void UpdateUserName(User modifiedUser, string userName)
        {
            if (String.IsNullOrWhiteSpace(userName))
            {
                throw new InvalidUserNameException();
            }
            if(userName.ToLower()==this.Email.ToString())
            {
                throw new InvalidUserNameException();
            }
            if(UserName != userName)
            {
                return;
            }
            UserName = userName;
            SetLastModified(modifiedUser);
            Raise(new UserUpdatedDomainEvent<User, UserId>(this, x => x.UserName, userName));
        }

        public void UpdatePassword(User modifiedUser, string password)
        {
            if (String.IsNullOrWhiteSpace(password))
            {
                throw new InvalidPasswordException("password is empty");
            }

            bool[] matches = new bool[PasswordPolicy.Keys.Count];
            int idx1 = 0,idx2 = 0;
            PasswordPolicy.ToList().ForEach(x => {
                Match match = x.Value.Match(password);
                if (match.Success)
                {
                    matches[idx1] = true;   
                }
                idx1++;
            });
            if (matches.Where(x=>x).Count()!=PasswordPolicy.Keys.Count)
            {
                string[] passwordPolicyViolences = new string[matches.Length];

                matches.ToList().ForEach(x =>
                {
                    if(!x)
                    {
                        passwordPolicyViolences[idx2] = PasswordPolicy.Keys.ToList()[idx2];
                    }
                    idx2++;
                });
                throw new InvalidPasswordException(passwordPolicyViolences);
            }
            Password = password;
            SetLastModified(modifiedUser);
            Raise(new UserUpdatedDomainEvent<User, UserId>(this, x => x.Password, password));
        }

        public void UpdateFirstName(User modifiedUser, string firstName)
        {
            if (String.IsNullOrWhiteSpace(firstName))
            {
                throw new InvalidFirstNameException();
            }
            if (FirstName != firstName)
            {
                return;
            }
            FirstName = firstName;
            SetLastModified(modifiedUser);
            Raise(new UserUpdatedDomainEvent<User, UserId>(this, x => x.Email, firstName));
        }

        public void UpdateLastName(User modifiedUser, string lastName)
        {
            if(String.IsNullOrWhiteSpace(lastName))
            {
                throw new InvalidLastNameException();
            }
            if (LastName != LastName)
            {
                return;
            }
            LastName = LastName;
            SetLastModified(modifiedUser);
            Raise(new UserUpdatedDomainEvent<User, UserId>(this, x => x.Email, lastName));
        }

        public void UpdateEmail(User modifiedUser, Email email)
        {
            if(email == null)
            {
                throw new ArgumentNullException(nameof(email));
            }
            if (this.Email != email)
            {
                return;
            }
            Email = email;
            SetLastModified(modifiedUser);
            Raise(new UserUpdatedDomainEvent<User, UserId>(this, x => x.Email, email));
        }

        public void UpdatePhone(User modifiedUser, PhoneNumber phone)
        {
            if (phone == null)
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
                throw new AcceptFriendshipException("friendship doesnt exists");
            }
            if (_friends.Where(x => x.Friend == friendRequest.RequestUser).Any())
            {
                throw new AcceptFriendshipException("you already friends");
            }
            _friendshipRequests.Remove(friendRequest);
            AddFriend(this,friendRequest.RequestUser);
            Raise(new UserAcceptFriendshipRequestDomainEvent(this, friendRequest));
        }

        public void AddFriend(User execUser,User friend)
        {

            var foundFriend = _friends.Any(x => x.Friend == friend);
            if (foundFriend)
            {
                throw new AddFriendException("you already friends");
            }
            var userFriend = UserFriend.Create(this,friend, DateTime.Now, null, null, execUser);
            _friends.Add(userFriend);
            Raise(new UserAddFriendDomainEvent(this, friend));
        }

        public void RemoveFriend(User execUser, User friend)
        {
            if(!_friends.Any())
            {
                throw new RemoveFriendException("you have no friends");
            }
            var foundFriend = _friends.Where(x => x.Friend == friend).First();
            if (foundFriend == null)
            {
                throw new RemoveFriendException("you arent be friends");
            }
            _friends.Remove(foundFriend);
            Raise(new UserRemoveFriendDomainEvent(this, friend));
        }

        public void AddRole(User assignerUser,Role.Role role)
        {
            if (_roles.Where(x => x.Role == role).Any())
            {
                throw new AddRoleException("role already assigned");
            }
            var userRole = UserRole.Create(this,role, DateTime.Now, null, null);
            _roles.Add(userRole);
            SetLastModified(assignerUser);
            Raise(new UserAssignedRoleToUserDomainEvent(assignerUser,this,role));
        }

        public void RemoveRole(User revokerUser, Role.Role role)
        {
            if(!_roles.Any())
            {
                throw new RemoveRoleException("you have no roles assigned");
            }
            var foundUserRole = _roles.Where(x => x.Role == role).First();  
            if (foundUserRole==null)
            {
                throw new RemoveRoleException("role already revoked or is not assigned");
            }
            _roles.Remove(foundUserRole);
            SetLastModified(revokerUser);
            Raise(new UserRevokedRoleToUserDomainEvent(revokerUser,this,role));
        }

        public void Remove(User deletedByUser)
        {
            if(this.IsDeleted())
            {
                throw new UserAlreadyDeletedException("user is already deleted");
            }
            SetDeleted(deletedByUser);
            Raise(new UserDeletedDomainEvent(this));
        }

        public void SetLastModified(User modifiedBy)
        {
            this.LastModifiedTime = DateTime.Now;
            this.LastModifiedByUser = modifiedBy;
        }

        public void SetCreated(User createdBy)
        {
            this.CreatedTime = DateTime.Now;
            this.CreatedByUser = createdBy;
        }

        public void SetDeleted(User deletedBy)
        {
            this.DeletedTime = DateTime.Now;
            this.DeletedByUser = deletedBy;
        }

        public bool IsFullyRegistered()
        {
            return this.ActivationDateTime != null && this.ActivationDateTime != DateTime.MinValue;
        }

        public bool HasBirthday()
        {
            return this.DateOfBirth == DateOnly.FromDateTime(DateTime.Now);
        }
        public bool IsDeleted() => this.DeletedTime != null;
    }
}
