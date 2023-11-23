using Domain.Primitives;
using Domain.ValueObjects;
using Infrastructure.Abstractions;
using Infrastructure.DatabaseEntity;
using Infrastructure.FileSys;

namespace Infrastructure.Mapper.Concrete
{
    internal class UserMapper : AbstractMapper<Domain.Entities.User.User, User>
    {
        public override async Task<User> MapToDatabaseEntity(Domain.Entities.User.User entity, bool mapRelationObjects)
        {
            if (entity == null)
                return null;
            User user = new User();
            user.Uuid = entity.Uuid.ToGuid() ;
            user.UserTypeUuid = entity.UserType.Uuid.ToGuid();
            user.UserName = entity.UserName;
            user.Email = entity.Email.EmailValue;
            user.Password = entity.Password;
            user.Phone = entity.Phone.PhoneNumb;
            user.ProfilePicturePath = entity.Picture.FilePath;
            user.ProfilePictureFileExt = entity.Picture.FileExtension;
            user.DateOfBirth = entity.DateOfBirth.ToDateTime(new TimeOnly());
            user.ActivationDatetime = entity.ActivationDateTime;
            user.ActivationToken = entity.ActivationToken;
            user.ActivationCode = entity.ActivationCode;

            user.PasswordResetCode = entity.PasswordResetCode;
            user.PasswordResetExpiresIn = entity.PasswordResetExpiresIn;
            user.PasswordResetToken = entity.PasswordResetToken;

            user.CreatedTime = entity.CreatedTime;
            user.LastModifiedTime = entity.LastModifiedTime;    
            user.DeletedTime = entity.DeletedTime;
            if(mapRelationObjects)
            {
                entity.Friends.ToList().ForEach(async(userFriend) => {
                    var friend = await userFriend.Friend.MapToDatabaseEntity<Domain.Entities.User.User, User>(false);
                    Infrastructure.DatabaseEntity.UserFriend userFriendDatabaseEntity = new DatabaseEntity.UserFriend();
                    userFriendDatabaseEntity.FriendUserUuid = friend.Uuid;
                    userFriendDatabaseEntity.UserUuid =user.Uuid;
                    userFriendDatabaseEntity.CreatedTime = userFriend.CreatedTime;
                    userFriendDatabaseEntity.DeletedTime = userFriend.DeletedTime;
                    userFriendDatabaseEntity.LastModifiedTime = userFriend.LastModifiedTime;
                    user.UserFriendFriendUserUus.Add(userFriendDatabaseEntity);
                });
                entity.Roles.ToList().ForEach(async(userRole) => {
                    var role = await userRole.Role.MapToDatabaseEntity<Domain.Entities.Role.Role, Role>(false);

                    UserRelationToRole userRelationToRoleDatabaseEntity = new UserRelationToRole();
                    userRelationToRoleDatabaseEntity.RoleUuid =role.Uuid;
                    userRelationToRoleDatabaseEntity.UserUuid =user.Uuid;
                    userRelationToRoleDatabaseEntity.CreatedTime =userRole.CreatedTime;
                    userRelationToRoleDatabaseEntity.DeletedTime = userRole.DeletedTime;
                    userRelationToRoleDatabaseEntity.LastModifiedTime = userRole.LastModifiedTime;
                    user.UserRelationToRoles.Add(userRelationToRoleDatabaseEntity);
                });
                entity.FriendshipRequests.ToList().ForEach(async(friendShipRequest) => {
                    var requester = await friendShipRequest.RequestUser.MapToDatabaseEntity<Domain.Entities.User.User, User>(false);

                    Infrastructure.DatabaseEntity.UserFriendshipRequest userFriendshipRequest = new UserFriendshipRequest();
                    userFriendshipRequest.UserUuid = requester.Uuid; 
                    userFriendshipRequest.TargetUserUuid =user.Uuid; 
                    userFriendshipRequest.TargetUserRequestMessage =friendShipRequest.TargetUserRequestMessage;
                    userFriendshipRequest.CreatedTime = friendShipRequest.CreatedTime;

                    user.UserFriendshipRequestTargetUserUus.Add(userFriendshipRequest);
                });
            }
            return user;
        }

        public override async Task<Domain.Entities.User.User> MapToDomainEntity(User entity,bool withRelation = false)
        {
            if (entity == null)
                return null;
            var userId = entity.Uuid.ToIdentification<Domain.Entities.User.UserId> ();
            var userType = await entity.UserTypeUu.MapToDomainEntity<Domain.Entities.User.UserType, UserType>(false);
            var phone = PhoneNumber.Parse(entity.Phone);
            
            var picture = Picture.Parse(entity.ProfilePicturePath, entity.ProfilePictureFileExt);
            try
            {
                var pic =await ((MediaContent)picture).LoadMediaContent(CancellationToken.None);
                picture.SetBinary(pic); 
            }
            catch (Exception ex)
            {

            }
            var email = Email.Parse(entity.Email);
            var birthDayDateOnly = DateOnly.FromDateTime(entity.DateOfBirth);

            ICollection<UserRole> roles = new List<UserRole>();
            ICollection<Domain.ValueObjects.UserFriend> friends = new List<Domain.ValueObjects.UserFriend>();
            ICollection<FriendshipRequest> friendshipRequests = new List<FriendshipRequest>();
            if (withRelation)
            {
                var rolesTasks = entity.UserRelationToRoles.Select(async(role) =>
                {

                    var r = await role.RoleUu.MapToDomainEntity<Domain.Entities.Role.Role, Role>(false);
                    var u = await role.UserUu.MapToDomainEntity<Domain.Entities.User.User, User>(false);
                    return UserRole.Create(
                        u,
                        r,
                        role.CreatedTime ?? DateTime.MinValue,
                        role.LastModifiedTime,
                        role.DeletedTime);
                }).ToList();
                roles =await Task.WhenAll(rolesTasks);
                var friendsTasks = entity.UserFriendUserUus.Select(async(friend) =>
                {
                    var userFriendDomainModel = await friend.FriendUserUu.MapToDomainEntity<Domain.Entities.User.User, User>(false);
                    var userDomainModel = await friend.UserUu.MapToDomainEntity<Domain.Entities.User.User, User>(false);

                    return Domain.ValueObjects.UserFriend.Create(
                        userDomainModel,
                        userFriendDomainModel,
                        friend.CreatedTime ?? DateTime.MinValue,
                        friend.LastModifiedTime,
                        friend.DeletedTime,
                        null);
                }).ToList();
                friends = await Task.WhenAll(friendsTasks);
                var friendshipRequestsTasks = entity.UserFriendshipRequestUserUus.Select(async(r) =>
                {
                    var targetUserRequestMessage = r.TargetUserRequestMessage;
                    var requestUserDomainModel = await r.UserUu.MapToDomainEntity<Domain.Entities.User.User, User>(false);
                    var targetUserDomainModel = await r.TargetUserUu.MapToDomainEntity<Domain.Entities.User.User, User>(false);
                    return FriendshipRequest.Create(targetUserRequestMessage, requestUserDomainModel, targetUserDomainModel);

                }).ToList();
                friendshipRequests = await Task.WhenAll(friendshipRequestsTasks);   
            }

            return Domain.Entities.User.User.Create(
                userId,
                userType,
                entity.UserName,
                entity.Password,
                entity.FirstName,
                entity.LastName,
                entity.ActivationToken,
                entity.ActivationCode,
                entity.PasswordResetCode,
                entity.PasswordResetToken,
                entity.PasswordResetExpiresIn,
                email,
                phone,
                picture,
                roles,
                friends,
                friendshipRequests,
                birthDayDateOnly,
                entity.ActivationDatetime,
                (DateTime)entity.CreatedTime!,
                entity.LastModifiedTime,
                entity.DeletedTime,
                null);
        }
    }
}
