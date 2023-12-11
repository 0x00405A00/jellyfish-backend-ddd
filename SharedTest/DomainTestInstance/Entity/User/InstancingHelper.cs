using Domain.Entities.Role;
using Domain.Entities.User;
using Domain.ValueObjects;

namespace SharedTest.DomainTestInstance.Entity.User
{
    public static class InstancingHelper
    {

        public static Domain.Entities.User.User GetUserInstance(Guid? uuid = null, UserRole? adminRole = null)
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
            var dateOfBirth = new DateOnly(1990, 1, 1);
            var createdTime = DateTime.UtcNow;

            var role = GetRoleInstance();
            var userRole = GetUserRoleInstance(role);
            ICollection<UserRole> userRoles = new List<UserRole>() { userRole };
            if(adminRole is not null)
            {
                userRoles.Add(adminRole);
            }

            var user = Domain.Entities.User.User.Create(
                id,
                userType,
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
                null, // Optional: friends
                null, // Optional: friendshipRequests
                dateOfBirth,
                null, // Optional: activationDateTime
                createdTime,
                null, // Optional: lastModifiedTime
                null, // Optional: deletedTime
                null,// Optional: createdBy
                null,
                null); 

            return user;
        }

        public static UserType GetUserTypeInstance(string typeName = "User")
        {
            return UserType.Create(new UserTypeId(Guid.NewGuid()),
                                           typeName,
                                           DateTime.Now,
                                           null,
                                           null,
                                           null,
                                           null,
                                           null);
        }

        public static FriendshipRequest GetFriendshipRequest(Domain.Entities.User.User fromUser, Domain.Entities.User.User toUser)
        {
            return FriendshipRequest.Create("be my new friend", fromUser, toUser);
        }

        public static Role GetRoleInstance(string roleName = "User")
        {
            return Role.Create(
                new RoleId(Guid.NewGuid()),
                roleName,
                null,
                DateTime.Now,
                null,
                null,
                null,
                null,
                null);
        }
        public static UserRole GetUserRoleInstance(Role role)
        {
            return UserRole.Create(role, DateTime.Now, null, null);
        }
    }
}
