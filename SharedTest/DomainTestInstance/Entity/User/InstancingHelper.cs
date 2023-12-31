using Domain.Const;
using Domain.Entities.Roles;
using Domain.Entities.Users;
using Domain.Extension;
using Domain.Primitives.Ids;
using Domain.ValueObjects;
using Shared.ApiDataTransferObject;

namespace SharedTest.DomainTestInstance.Entity.Users
{
    public static class InstancingHelper
    {

        public static User GetUserInstance(Guid? uuid = null, UserHasRelationToRole? adminRole = null)
        {

            // Arrange

            var id = new UserId(uuid??Guid.NewGuid());
            UserType userType = GetUserTypeInstance();
            var userName = "sampleUser";
            var password = "samplePassword";
            var firstName = "John";
            var lastName = "Doe";
            var email = Email.Parse("johndoe@example.com");
            var phone = PhoneNumber.Parse("1234567890");
            var dateOfBirth = new DateTime(1980, 12, 31);
            var createdTime = DateTime.UtcNow;

            var role = GetRoleUserInstance();
            var userRole = GetUserRoleInstance(role,id);
            List<UserHasRelationToRole> userRoles = new List<UserHasRelationToRole>() { userRole };
            if(adminRole is not null)
            {
                userRoles.Add(adminRole);
            }

            var user = User.Create(
                id,
                userType.Id,
                userName,
                password,
                firstName,
                lastName,
                null,//Optional: activationToken
                null,//Optional: activationCode
                null,//Optional: passwordResetCode
                null,//Optional: passwordResetToken
                null,//Optional: passwordResetExpiresIn
                email,
                phone,
                null, // Optional: picture
                userRoles, // Optional: roles
                dateOfBirth.ToTypedDateOnly(),
                null, // Optional: activationDateTime
                createdTime.ToTypedDateTime(),
                null, // Optional: lastModifiedTime
                null, // Optional: deletedTime
                null,// Optional: createdBy
                null,
                null); 

            return user;
        }

        public static UserType GetUserTypeInstance(string typeName = "User")
        {
            return UserType.Create(new UserTypeId(UserConst.UserType.User),
                                           typeName,
                                           DateTime.Now.ToTypedDateTime(),
                                           null,
                                           null,
                                           null,
                                           null,
                                           null);
        }

        public static FriendshipRequest GetFriendshipRequest(User fromUser, User toUser)
        {
            return FriendshipRequest.Create(
                FriendshipRequest.NewId(),
                "be my new friend",
                fromUser.Id,
                toUser.Id,
                DateTime.Now.ToTypedDateTime(),
                null,
                null);
        }

        public static Role GetRoleUserInstance()
        {
            return Role.Create(
                new RoleId(RoleConst.UserRoleUuid),
                UserConst.UserType.UserName,
                DateTime.Now.ToTypedDateTime(),
                null,
                null,
                null,
                null,
                null);
        }
        public static Role GetRoleAdminInstance()
        {
            return Role.Create(
                new RoleId(RoleConst.AdminRoleUuid),
                UserConst.UserType.AdminName,
                DateTime.Now.ToTypedDateTime(),
                null,
                null,
                null,
                null,
                null);
        }
        public static Role GetRoleRootInstance()
        {
            return Role.Create(
                new RoleId(RoleConst.RootRoleUuid),
                UserConst.UserType.RootName,
                DateTime.Now.ToTypedDateTime(),
                null,
                null,
                null,
                null,
                null);
        }
        public static UserHasRelationToRole GetUserRoleInstance(Role role,UserId user)
        {
            return UserHasRelationToRole.Create(
                UserHasRelationToRole.NewId(),
                user,
                role.Id,
                DateTime.Now.ToTypedDateTime(),
                null,
                null,
                null,
                null,
                null);
        }
    }
}
