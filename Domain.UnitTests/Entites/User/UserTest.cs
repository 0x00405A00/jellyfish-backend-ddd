﻿using Domain.Entities.User;
using Domain.Entities.User.Exception;
using Domain.ValueObjects;

namespace Domain.UnitTests.Entites.User
{
    public class UserTest
    {
        public static Domain.Entities.User.User CreatedBy = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserInstance();
        public static Domain.Entities.User.User ModifiedBy = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserInstance();
        public static Domain.Entities.User.UserType UserType = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserTypeInstance();

        public UserTest()
        {
            
        }
        [Fact]

        public void Create_ValidParameters_ReturnsUserInstance()
        {
            // Arrange
            var id = new UserId(Guid.NewGuid());
            var userName = "sampleUser";
            var password = "samplePassword";
            var firstName = "John";
            var lastName = "Doe";
            var email = Email.Parse("johndoe@example.com");
            var phone = PhoneNumber.Parse("1234567890");
            var dateOfBirth = new DateOnly(1990, 1, 1);
            var createdTime = DateTime.UtcNow;

            var role = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetRoleInstance();
            var userRole = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserRoleInstance(role);
            ICollection<UserRole> userRoles = new List<UserRole>() { userRole };

            //Act
            var result = Entities.User.User.Create(
                id,
                UserType,
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

            // Assert
            Assert.NotNull(result);
            Assert.Equal(id, result.Uuid);
            Assert.Equal(UserType, result.UserType);
            Assert.Equal(userName, result.UserName);
            // Add more assertions based on your object properties
        }

        [Fact]
        public void UpdateUserType_ValidUserType_UpdatesUserType()
        {
            // Arrange
            var user = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserInstance();
            var modifiedUser = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserInstance();
            var newUserType = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserTypeInstance(); // Hier einen gültigen UserType einsetzen

            // Act
            user.UpdateUserType(modifiedUser, newUserType);

            // Assert
            Assert.Equal(newUserType, user.UserType);
        }

        [Fact]
        public void UpdateUserType_NullUserType_ThrowsArgumentNullException()
        {
            // Arrange
            var user = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserInstance();
            var modifiedUser = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserInstance();
            UserType invalidUserType = null;

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() => user.UpdateUserType(modifiedUser, invalidUserType));
            Assert.NotNull(exception);
        }

        [Fact]
        public void UpdateUserName_ValidUserName_UpdatesUserName()
        {
            // Arrange
            var user = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserInstance();
            var modifiedUser = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserInstance();
            var newUserName = "newUserName"; // Hier einen gültigen Benutzernamen einsetzen

            // Act
            user.UpdateUserName(modifiedUser, newUserName);

            // Assert
            Assert.Equal(newUserName, user.UserName);
        }

        [Fact]
        public void UpdateUserName_InvalidUserName_ThrowsInvalidUserNameException()
        {
            // Arrange
            var user = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserInstance();
            var modifiedUser = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserInstance();
            string invalidUserName = null;

            // Act and Assert
            var exception = Assert.Throws<InvalidUserNameException>(() => user.UpdateUserName(modifiedUser, invalidUserName));
            Assert.NotNull(exception);
        }
        [Fact]
        public void UpdateFirstName_ValidFirstName_UpdatesFirstName()
        {
            // Arrange
            var user = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserInstance();
            var modifiedUser = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserInstance();
            var newFirstName = "John";

            // Act
            user.UpdateFirstName(modifiedUser, newFirstName);

            // Assert
            Assert.Equal(newFirstName, user.FirstName);
        }

        [Fact]
        public void UpdateFirstName_InvalidFirstName_ThrowsInvalidFirstNameException()
        {
            // Arrange
            var user = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserInstance();
            var modifiedUser = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserInstance();
            string invalidFirstName = null;

            // Act and Assert
            var exception = Assert.Throws<InvalidFirstNameException>(() => user.UpdateFirstName(modifiedUser, invalidFirstName));
            Assert.NotNull(exception);
        }

        [Fact]
        public void UpdateEmail_ValidEmail_UpdatesEmail()
        {
            // Arrange
            var user = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserInstance();
            var modifiedUser = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserInstance();
            var newEmail = Email.Parse("new@example.com");

            // Act
            user.UpdateEmail(modifiedUser, newEmail);

            // Assert
            Assert.Equal(newEmail, user.Email);
        }

        [Fact]
        public void UpdateEmail_NullEmail_ThrowsArgumentNullException()
        {
            // Arrange
            var user = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserInstance();
            var modifiedUser = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserInstance();
            Email invalidEmail = null;

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() => user.UpdateEmail(modifiedUser, invalidEmail));
            Assert.NotNull(exception);
        }

        [Fact]
        public void UpdatePhone_ValidPhone_UpdatesPhone()
        {
            // Arrange
            var user = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserInstance();
            var modifiedUser = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserInstance();
            var newPhone = PhoneNumber.Parse("(123)-456-7890");

            // Act
            user.UpdatePhone(modifiedUser, newPhone);

            // Assert
            Assert.Equal(newPhone, user.Phone);
        }

        [Fact]
        public void UpdatePhone_NullPhone_ThrowsArgumentNullException()
        {
            // Arrange
            var user = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserInstance();
            var modifiedUser =  SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserInstance();
            PhoneNumber invalidPhone = null;

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() => user.UpdatePhone(modifiedUser, invalidPhone));
            Assert.NotNull(exception);
        }
        [Fact]
        public void AddFriendshipRequest_ValidFriendshipRequest_AddsFriendshipRequest()
        {
            // Arrange
            var user = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserInstance();
            var fromUser = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserInstance(); // Erstelle gültige Benutzer
            var toUser = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserInstance(); // Erstelle gültige Benutzer
            var friendRequest = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetFriendshipRequest(fromUser, toUser);

            // Act
            user.AddFriendshipRequest(friendRequest);

            // Assert
            Assert.Contains(friendRequest, user.FriendshipRequests);
        }

        [Fact]
        public void AddFriendshipRequest_DuplicateFriendshipRequest_ThrowsAddFriendshipRequestException()
        {
            // Arrange
            var user = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserInstance();
            var fromUser = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserInstance(); // Erstelle gültige Benutzer
            var toUser = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserInstance(); // Erstelle gültige Benutzer
            var friendRequest = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetFriendshipRequest(fromUser, toUser);
            user.AddFriendshipRequest(friendRequest);

            // Act and Assert
            var exception = Assert.Throws<AddFriendshipRequestException>(() => user.AddFriendshipRequest(friendRequest));
            Assert.NotNull(exception);
        }

        [Fact]
        public void RemoveFriendshipRequest_ValidFriendshipRequest_RemovesFriendshipRequest()
        {
            // Arrange
            var user = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserInstance();
            var fromUser = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserInstance(); // Erstelle gültige Benutzer
            var toUser = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserInstance(); // Erstelle gültige Benutzer
            var friendRequest = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetFriendshipRequest(fromUser, toUser);
            user.AddFriendshipRequest(friendRequest);

            // Act
            user.RemoveFriendshipRequest(friendRequest);

            // Assert
            Assert.DoesNotContain(friendRequest, user.FriendshipRequests);
        }

        [Fact]
        public void RemoveFriendshipRequest_NonExistentFriendshipRequest_ThrowsRemoveFriendshipRequestException()
        {
            // Arrange
            var user = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserInstance();
            var fromUser = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserInstance(); // Erstelle gültige Benutzer
            var toUser = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserInstance(); // Erstelle gültige Benutzer
            var friendRequest = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetFriendshipRequest(fromUser, toUser);

            // Act and Assert
            var exception = Assert.Throws<RemoveFriendshipRequestException>(() => user.RemoveFriendshipRequest(friendRequest));
            Assert.NotNull(exception);
        }
        [Fact]
        public void AcceptFriendshipRequest_ValidFriendshipRequest_AcceptsFriendship()
        {
            // Arrange
            var user = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserInstance();
            var friendRequest = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetFriendshipRequest(user, SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserInstance()); // Erstelle gültige FriendshipRequest

            // Act
            user.AddFriendshipRequest(friendRequest);
            user.AcceptFriendshipRequest(friendRequest);

            // Assert
            Assert.Contains(friendRequest.RequestUser, user.Friends.Select(x=>x.Friend).ToList());
            Assert.DoesNotContain(friendRequest, user.FriendshipRequests);
        }

        [Fact]
        public void AcceptFriendshipRequest_NonExistentFriendshipRequest_ThrowsAcceptFriendshipException()
        {
            // Arrange
            var user = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserInstance();
            var friendRequest = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetFriendshipRequest(user, SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserInstance()); // Erstelle gültige FriendshipRequest, aber akzeptiere sie nicht zuerst

            // Act and Assert
            var exception = Assert.Throws<AcceptFriendshipException>(() => user.AcceptFriendshipRequest(friendRequest));
            Assert.NotNull(exception);
        }

        [Fact]
        public void AcceptFriendshipRequest_AlreadyFriends_ThrowsAcceptFriendshipException()
        {
            // Arrange
            var user = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserInstance();
            var friend = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserInstance(); // Erstelle einen Freund
            user.AddFriend(user, friend); // Füge den Freund hinzu
            var friendRequest = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetFriendshipRequest(user, friend); // Erstelle gültige FriendshipRequest

            // Act and Assert
            var exception = Assert.Throws<AcceptFriendshipException>(() => user.AcceptFriendshipRequest(friendRequest));
            Assert.NotNull(exception);
        }
        [Fact]
        public void AddFriend_ValidFriend_AddsFriend()
        {
            // Arrange
            var user = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserInstance();
            var friend = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserInstance(); // Erstelle einen gültigen Freund

            // Act
            user.AddFriend(user,friend);

            // Assert
            Assert.Contains(friend, user.Friends.Select(x => x.Friend).ToList());
        }

        [Fact]
        public void AddFriend_AlreadyFriends_ThrowsAddFriendException()
        {
            // Arrange
            var user = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserInstance();
            var friend = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserInstance(); // Erstelle einen gültigen Freund
            user.AddFriend(user, friend); // Füge den Freund hinzu

            // Act and Assert
            var exception = Assert.Throws<AddFriendException>(() => user.AddFriend(user, friend));
            Assert.NotNull(exception);
        }
        [Fact]
        public void RemoveFriend_ValidFriend_RemovesFriend()
        {
            // Arrange
            var user = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserInstance();
            var friend = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserInstance(); // Erstelle einen gültigen Freund
            user.AddFriend(user,friend); // Füge den Freund hinzu

            // Act
            user.RemoveFriend(user, friend);

            // Assert
            Assert.DoesNotContain(friend, user.Friends.Select(x => x.Friend).ToList());
        }

        [Fact]
        public void RemoveFriend_NonExistentFriend_ThrowsRemoveFriendException()
        {
            // Arrange
            var user = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserInstance();
            var friend = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserInstance(); // Erstelle einen gültigen Freund

            // Act and Assert
            var exception = Assert.Throws<RemoveFriendException>(() => user.RemoveFriend(user, friend));
            Assert.NotNull(exception);
        }
        [Fact]
        public void AddRole_ValidRole_AddsRole()
        {
            // Arrange
            var user = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserInstance();
            var assignerUser = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserInstance();
            var role = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetRoleInstance(); // Erstelle eine gültige Rolle

            // Act
            user.AddRole(assignerUser, role);

            // Assert
            Assert.Contains(role, user.Roles.Select(x => x.Role).ToList());
        }

        [Fact]
        public void AddRole_AlreadyAssignedRole_ThrowsAddRoleException()
        {
            // Arrange
            var user = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserInstance();
            var assignerUser = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserInstance();
            var role = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetRoleInstance(); // Erstelle eine gültige Rolle
            user.AddRole(assignerUser, role); // Weise die Rolle bereits zu

            // Act and Assert
            var exception = Assert.Throws<AddRoleException>(() => user.AddRole(assignerUser, role));
            Assert.NotNull(exception);
        }
        [Fact]
        public void RemoveRole_ValidRole_RemovesRole()
        {
            // Arrange
            var user = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserInstance();
            var revokerUser = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserInstance();
            var role = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetRoleInstance(); // Erstelle eine gültige Rolle
            user.AddRole(revokerUser, role); // Weise die Rolle zu

            // Act
            user.RemoveRole(revokerUser, role);

            // Assert
            Assert.DoesNotContain(role, user.Roles.Select(x => x.Role).ToList());
        }

        [Fact]
        public void RemoveRole_RoleNotAssigned_ThrowsRemoveRoleException()
        {
            // Arrange
            var user = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserInstance();
            var revokerUser = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserInstance();
            var role = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetRoleInstance(); // Erstelle eine gültige Rolle

            // Act and Assert
            var exception = Assert.Throws<RemoveRoleException>(() => user.RemoveRole(revokerUser, role));
            Assert.NotNull(exception);
        }
        [Fact]
        public void Remove_UserNotAlreadyDeleted_RemovesUser()
        {
            // Arrange
            var user = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserInstance();
            var deletedByUser = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserInstance();

            // Act
            user.Remove(deletedByUser);

            // Assert
            Assert.True(user.IsDeleted());
        }

        [Fact]
        public void Remove_UserAlreadyDeleted_ThrowsUserAlreadyDeletedException()
        {
            // Arrange
            var user = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserInstance();
            var deletedByUser = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserInstance();
            user.Remove(deletedByUser); // Markiere den Benutzer als gelöscht

            // Act and Assert
            var exception = Assert.Throws<UserAlreadyDeletedException>(() => user.Remove(deletedByUser));
            Assert.NotNull(exception);
        }
    }
}
