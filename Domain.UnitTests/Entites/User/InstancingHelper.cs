﻿using Domain.Entities.Role;
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

            var role = InstancingHelper.GetRoleInstance();
            var userRole = InstancingHelper.GetUserRoleInstance(role);
            ICollection<UserRole> userRoles = new List<UserRole>() { userRole };

            var user = Entities.User.User.Create(
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
                null); // Optional: createdBy

            return user;
        }

        public static UserType GetUserTypeInstance(string typeName="User")
        {
            return UserType.Create(new UserTypeId(Guid.NewGuid()),
                                           typeName,
                                           DateTime.Now,
                                           null,
                                           null);
        }

        public static FriendshipRequest GetFriendshipRequest(Entities.User.User fromUser, Entities.User.User toUser)
        {
            return FriendshipRequest.Create("be my new friend", fromUser, toUser);
        }

        public static Role GetRoleInstance(string roleName = "User")
        {
            return Role.Create(new RoleId(Guid.NewGuid()), roleName, null, DateTime.Now, null, null);
        }
        public static UserRole GetUserRoleInstance(Role role)
        {
            return UserRole.Create(role, DateTime.Now, null, null);
        }
    }
}
