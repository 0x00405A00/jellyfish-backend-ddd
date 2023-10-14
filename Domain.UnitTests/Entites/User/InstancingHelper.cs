using Domain.Entities.Role;
using Domain.Entities.User;
using Domain.ValueObjects;

namespace Domain.UnitTests.Entites.User
{
    public static class InstancingHelper
    {

        public static Entities.User.User GetUserInstance()
        {

            // Arrange

            var id = new UserId(Guid.NewGuid());
            UserType userType = GetUserTypeInstance();
            var userName = "sampleUser";
            var password = "samplePassword";
            var firstName = "John";
            var lastName = "Doe";
            var email = Email.Parse("johndoe@example.com");
            var phone = PhoneNumber.Parse("1234567890");
            var dateOfBirth = new DateOnly(1990, 1, 1);
            var createdTime = DateTime.UtcNow;

            var user = Entities.User.User.Create(
                id,
                userType,
                userName,
                password,
                firstName,
                lastName,
                email,
                phone,
                null, // Optional: Picture
                null, // Optional: Roles
                null, // Optional: Friends
                null, // Optional: FriendshipRequests
                dateOfBirth,
                null, // Optional: ActivationDateTime
                createdTime,
                null, // Optional: LastModifiedTime
                null, // Optional: DeletedTime
                null); // Optional: CreatedBy

            return user;
        }

        public static UserType GetUserTypeInstance()
        {
            return UserType.Create(new UserTypeId(Guid.NewGuid()),
                                           "testtype",
                                           DateTime.Now,
                                           null,
                                           null);
        }

        public static FriendshipRequest GetFriendshipRequest(Entities.User.User fromUser, Entities.User.User toUser)
        {
            return FriendshipRequest.Create("be my new friend", fromUser, toUser);
        }

        public static Role GetRoleInstance()
        {
            return Role.Create(new RoleId(Guid.NewGuid()), "role", null, DateTime.Now, null, null);
        }
    }
}
