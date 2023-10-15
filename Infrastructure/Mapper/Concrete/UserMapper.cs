using Domain.Primitives;
using Domain.ValueObjects;
using Infrastructure.Abstractions;
using Infrastructure.DatabaseEntity;
using Microsoft.AspNetCore.Connections;

namespace Infrastructure.Mapper.Concrete
{
    internal class UserMapper : AbstractMapper<Domain.Entities.User.User, User>
    {
        public override User MapToDatabaseEntity(Domain.Entities.User.User entity, bool mapRelationObjects)
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
            user.DateOfBirth = entity.DateOfBirth.ToDateTime(new TimeOnly());
            user.ActivationDatetime = entity.ActivationDateTime;
            user.CreatedTime = entity.CreatedTime;
            user.LastModifiedTime = entity.LastModifiedTime;    
            user.DeletedTime = entity.DeletedTime;
            user.Deleted = Convert.ToSByte(entity.DeletedTime!=DateTime.MinValue);
            if(mapRelationObjects)
            {
                entity.Friends.ToList().ForEach(userFriend => {
                    var friend = userFriend.Friend.MapToDatabaseEntity<Domain.Entities.User.User, User>(false);
                    Infrastructure.DatabaseEntity.UserFriend userFriendDatabaseEntity = new DatabaseEntity.UserFriend();
                    userFriendDatabaseEntity.FriendUserUu = friend;
                    userFriendDatabaseEntity.UserUu =user;
                    userFriendDatabaseEntity.CreatedTime = userFriend.CreatedTime;
                    userFriendDatabaseEntity.DeletedTime = userFriend.DeletedTime;
                    userFriendDatabaseEntity.Deleted = Convert.ToSByte(userFriend.DeletedTime != DateTime.MinValue);
                    userFriendDatabaseEntity.LastModifiedTime = userFriend.LastModifiedTime;
                    user.UserFriendFriendUserUus.Add(userFriendDatabaseEntity);
                });
                entity.Roles.ToList().ForEach(userRole => {
                    var role = userRole.Role.MapToDatabaseEntity<Domain.Entities.Role.Role, Role>(false);

                    UserRelationToRole userRelationToRoleDatabaseEntity = new UserRelationToRole();
                    userRelationToRoleDatabaseEntity.RoleUu =role;
                    userRelationToRoleDatabaseEntity.UserUu =user;
                    userRelationToRoleDatabaseEntity.CreatedTime =userRole.CreatedTime;
                    userRelationToRoleDatabaseEntity.DeletedTime = userRole.DeletedTime;
                    userRelationToRoleDatabaseEntity.Deleted = Convert.ToSByte(userRole.DeletedTime != DateTime.MinValue);
                    userRelationToRoleDatabaseEntity.LastModifiedTime = userRole.LastModifiedTime;
                    user.UserRelationToRoles.Add(userRelationToRoleDatabaseEntity);
                });
                entity.FriendshipRequests.ToList().ForEach(friendShipRequest => {
                    var requester = friendShipRequest.RequestUser.MapToDatabaseEntity<Domain.Entities.User.User, User>(false);

                    Infrastructure.DatabaseEntity.UserFriendshipRequest userFriendshipRequest = new UserFriendshipRequest();
                    userFriendshipRequest.UserUu = requester; 
                    userFriendshipRequest.TargetUserUu =user; 
                    userFriendshipRequest.TargetUserRequestMessage =friendShipRequest.TargetUserRequestMessage;
                    userFriendshipRequest.CreatedTime = friendShipRequest.CreatedTime;

                    user.UserFriendshipRequestTargetUserUus.Add(userFriendshipRequest);
                });
            }
            return user;
        }

        public override Domain.Entities.User.User MapToDomainEntity(User entity,bool withRelation = false)
        {
            if (entity == null)
                return null;
            var userId = entity.Uuid.ToIdentification<Domain.Entities.User.UserId> ();
            var userType = entity.UserTypeUu.MapToDomainEntity<Domain.Entities.User.UserType, UserType>(false);
            var phone = PhoneNumber.Parse(entity.Phone);
            var picture = Picture.Parse(entity.Picture);
            var email = Email.Parse(entity.Email);
            var birthDayDateOnly = DateOnly.FromDateTime(entity.DateOfBirth);

            ICollection<UserRole> roles = new List<UserRole>();
            ICollection<Domain.ValueObjects.UserFriend> friends = new List<Domain.ValueObjects.UserFriend>();
            ICollection<FriendshipRequest> friendshipRequests = new List<FriendshipRequest>();
            if (withRelation)
            {
                roles = entity.UserRelationToRoles.Select(role =>
                {

                    var r = role.RoleUu.MapToDomainEntity<Domain.Entities.Role.Role, Role>(false);
                    var u = role.UserUu.MapToDomainEntity<Domain.Entities.User.User, User>(false);
                    return UserRole.Create(
                        u,
                        r,
                        role.CreatedTime ?? DateTime.MinValue,
                        role.LastModifiedTime,
                        role.DeletedTime);
                }).ToList();
                friends = entity.UserFriendUserUus.Select(friend =>
                {
                    var userFriendDomainModel = friend.FriendUserUu.MapToDomainEntity<Domain.Entities.User.User, User>(false);
                    var userDomainModel = friend.UserUu.MapToDomainEntity<Domain.Entities.User.User, User>(false);

                    return Domain.ValueObjects.UserFriend.Create(
                        userDomainModel,
                        userFriendDomainModel,
                        friend.CreatedTime ?? DateTime.MinValue,
                        friend.LastModifiedTime,
                        friend.DeletedTime,
                        null);
                }).ToList();
                friendshipRequests = entity.UserFriendshipRequestUserUus.Select(r =>
                {
                    var targetUserRequestMessage = r.TargetUserRequestMessage;
                    var requestUserDomainModel = r.UserUu.MapToDomainEntity<Domain.Entities.User.User, User>(false);
                    var targetUserDomainModel = r.TargetUserUu.MapToDomainEntity<Domain.Entities.User.User, User>(false);
                    return FriendshipRequest.Create(targetUserRequestMessage, requestUserDomainModel, targetUserDomainModel);

                }).ToList();
            }

            return Domain.Entities.User.User.Create(
                userId,
                userType,
                entity.UserName,
                entity.Password,
                entity.FirstName,
                entity.LastName,
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
