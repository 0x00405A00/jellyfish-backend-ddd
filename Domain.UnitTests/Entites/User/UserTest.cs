
using Domain.Entities.Role;
using Domain.Entities.User;
using Domain.Entities.User.Event;
using Domain.Entities.User.Exception;
using Domain.ValueObjects;
using System;

namespace Domain.UnitTests.Entites.User
{
    public class UserTest
    {
        public UserTest()
        {
            
        }
        [Fact]
        public void Create_ValidParameters_CreatesUser()
        {
            // Arrange
            var userId = new UserId(Guid.NewGuid());
            var userType = InstancingHelper.GetUserTypeInstance();
            var userName = "TestUser";
            var password = "TestPassword";
            var firstName = "John";
            var lastName = "Doe";
            var email = Email.Parse("test@example.com");
            var phone = PhoneNumber.Parse("(123)-456-7890");
            Picture picture = null; // Setze das Bild, falls benötigt
            ICollection<UserRole> roles = new List<UserRole>();
            ICollection<UserFriend> friends = new List<UserFriend>();
            ICollection<FriendshipRequest> friendshipRequests = new List<FriendshipRequest>();
            var dateOfBirth = new DateOnly(1990, 1, 1);
            DateTime? activationDateTime = null;
            var createdTime = DateTime.Now;
            DateTime? lastModifiedTime = null;
            DateTime? deletedTime = null;
            Entities.User.User createdBy = null;

            // Act
            var user = Entities.User.User.Create(
                userId,
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
                deletedTime,
                createdBy);

            // Assert
            Assert.NotNull(user);
        }

        [Fact]
        public void Create_NullUserType_ThrowsArgumentNullException()
        {
            // Arrange
            var userId = new UserId(Guid.NewGuid());
            UserType userType = null; // UserType ist null
            var userName = "TestUser";
            var password = "TestPassword";
            var firstName = "John";
            var lastName = "Doe";
            var email = Email.Parse("test@example.com");
            var phone = PhoneNumber.Parse("(123)-456-7890");

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() => Entities.User.User.Create(
                userId,
                userType,
                userName,
                password,
                firstName,
                lastName,
                email,
                phone,
                null,
                null,
                null,
                null,
                new DateOnly(1990, 1, 1),
                null,
                DateTime.Now,
                null,
                null,
                null));
            Assert.NotNull(exception);
        }

        [Fact]
        public void Create_NullRoles_ThrowsInvalidDataException()
        {
            // Arrange
            var userId = new UserId(Guid.NewGuid());
            var userType = InstancingHelper.GetUserTypeInstance();
            var userName = "TestUser";
            var password = "TestPassword";
            var firstName = "John";
            var lastName = "Doe";
            var email = Email.Parse("test@example.com");
            var phone = PhoneNumber.Parse("(123)-456-7890");
            List<UserRole> roles = new List<UserRole> { null }; // Eine Rolle ist null

            // Act and Assert
            var exception = Assert.Throws<InvalidDataException>(() => Entities.User.User.Create(
                userId,
                userType,
                userName,
                password,
                firstName,
                lastName,
                email,
                phone,
                null,
                roles,
                null,
                null,
                new DateOnly(1990, 1, 1),
                null,
                DateTime.Now,
                null,
                null,
                null));
            Assert.NotNull(exception);
        }

        [Fact]
        public void Create_NullFriendshipRequests_ThrowsInvalidDataException()
        {
            // Arrange
            var userId = new UserId(Guid.NewGuid());
            var userType = InstancingHelper.GetUserTypeInstance();
            var userName = "TestUser";
            var password = "TestPassword";
            var firstName = "John";
            var lastName = "Doe";
            var email = Email.Parse("test@example.com");
            var phone = PhoneNumber.Parse("(123)-456-7890");
            List<FriendshipRequest> friendshipRequests = new List<FriendshipRequest> { null }; // Eine Anfrage ist null

            // Act and Assert
            var exception = Assert.Throws<InvalidDataException>(() => Entities.User.User.Create(
                userId,
                userType,
                userName,
                password,
                firstName,
                lastName,
                email,
                phone,
                null,
                null,
                null,
                friendshipRequests,
                new DateOnly(1990, 1, 1),
                null,
                DateTime.Now,
                null,
                null,
                null));
            Assert.NotNull(exception);
        }
        [Fact]
        public void UpdatePassword_ValidPassword_SetsPassword()
        {
            // Arrange
            var user = InstancingHelper.GetUserInstance();
            var updateUser = InstancingHelper.GetUserInstance();
            var validPassword = "StrongPassword123!";

            // Act
            user.UpdatePassword(updateUser, validPassword);

            // Assert
            Assert.Equal(validPassword, user.Password);
        }

        [Fact]
        public void UpdatePassword_InvalidPassword_ThrowsException()
        {
            // Arrange
            var user = InstancingHelper.GetUserInstance();
            var updateUser = InstancingHelper.  GetUserInstance();
            var invalidPassword = "WeakPwd"; // Assuming this doesn't meet the password policy.

            // Act 

            var act = () => user.UpdatePassword(updateUser, invalidPassword);

            // Assert
            Domain.Entities.User.Exception.InvalidPasswordException exception = Assert.Throws<Domain.Entities.User.Exception.InvalidPasswordException>(act);
            Assert.NotNull(exception);
        }

        [Fact]
        public void UpdatePassword_EmptyPassword_ThrowsException()
        {
            // Arrange
            var user = InstancingHelper.GetUserInstance();
            var updateUser = InstancingHelper.GetUserInstance();
            string emptyPassword = null; // Assuming this is an empty password.

            // Act
            var act = () => user.UpdatePassword(updateUser, emptyPassword);

            // Assert
            Domain.Entities.User.Exception.InvalidPasswordException exception = Assert.Throws<Domain.Entities.User.Exception.InvalidPasswordException>(act);
            Assert.NotNull(exception);
        }


        [Fact]
        public void UpdateUserType_ValidUserType_UpdatesUserType()
        {
            // Arrange
            var user = InstancingHelper.GetUserInstance();
            var modifiedUser = InstancingHelper.GetUserInstance();
            var newUserType = InstancingHelper.GetUserTypeInstance(); // Hier einen gültigen UserType einsetzen

            // Act
            user.UpdateUserType(modifiedUser, newUserType);

            // Assert
            Assert.Equal(newUserType, user.UserType);
        }

        [Fact]
        public void UpdateUserType_NullUserType_ThrowsArgumentNullException()
        {
            // Arrange
            var user = InstancingHelper.GetUserInstance();
            var modifiedUser = InstancingHelper.GetUserInstance();
            UserType invalidUserType = null;

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() => user.UpdateUserType(modifiedUser, invalidUserType));
            Assert.NotNull(exception);
        }

        [Fact]
        public void UpdateUserName_ValidUserName_UpdatesUserName()
        {
            // Arrange
            var user = InstancingHelper.GetUserInstance();
            var modifiedUser = InstancingHelper.GetUserInstance();
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
            var user = InstancingHelper.GetUserInstance();
            var modifiedUser = InstancingHelper.GetUserInstance();
            string invalidUserName = null;

            // Act and Assert
            var exception = Assert.Throws<InvalidUserNameException>(() => user.UpdateUserName(modifiedUser, invalidUserName));
            Assert.NotNull(exception);
        }
        [Fact]
        public void UpdateFirstName_ValidFirstName_UpdatesFirstName()
        {
            // Arrange
            var user = InstancingHelper.GetUserInstance();
            var modifiedUser = InstancingHelper.GetUserInstance();
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
            var user = InstancingHelper.GetUserInstance();
            var modifiedUser = InstancingHelper.GetUserInstance();
            string invalidFirstName = null;

            // Act and Assert
            var exception = Assert.Throws<InvalidFirstNameException>(() => user.UpdateFirstName(modifiedUser, invalidFirstName));
            Assert.NotNull(exception);
        }

        [Fact]
        public void UpdateEmail_ValidEmail_UpdatesEmail()
        {
            // Arrange
            var user = InstancingHelper.GetUserInstance();
            var modifiedUser = InstancingHelper.GetUserInstance();
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
            var user = InstancingHelper.GetUserInstance();
            var modifiedUser = InstancingHelper.GetUserInstance();
            Email invalidEmail = null;

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() => user.UpdateEmail(modifiedUser, invalidEmail));
            Assert.NotNull(exception);
        }

        [Fact]
        public void UpdatePhone_ValidPhone_UpdatesPhone()
        {
            // Arrange
            var user = InstancingHelper.GetUserInstance();
            var modifiedUser = InstancingHelper.GetUserInstance();
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
            var user = InstancingHelper.GetUserInstance();
            var modifiedUser =  InstancingHelper.GetUserInstance();
            PhoneNumber invalidPhone = null;

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() => user.UpdatePhone(modifiedUser, invalidPhone));
            Assert.NotNull(exception);
        }
        [Fact]
        public void AddFriendshipRequest_ValidFriendshipRequest_AddsFriendshipRequest()
        {
            // Arrange
            var user = InstancingHelper.GetUserInstance();
            var fromUser = InstancingHelper.GetUserInstance(); // Erstelle gültige Benutzer
            var toUser = InstancingHelper.GetUserInstance(); // Erstelle gültige Benutzer
            var friendRequest = InstancingHelper.GetFriendshipRequest(fromUser, toUser);

            // Act
            user.AddFriendshipRequest(friendRequest);

            // Assert
            Assert.Contains(friendRequest, user.FriendshipRequests);
        }

        [Fact]
        public void AddFriendshipRequest_DuplicateFriendshipRequest_ThrowsAddFriendshipRequestException()
        {
            // Arrange
            var user = InstancingHelper.GetUserInstance();
            var fromUser = InstancingHelper.GetUserInstance(); // Erstelle gültige Benutzer
            var toUser = InstancingHelper.GetUserInstance(); // Erstelle gültige Benutzer
            var friendRequest = InstancingHelper.GetFriendshipRequest(fromUser, toUser);
            user.AddFriendshipRequest(friendRequest);

            // Act and Assert
            var exception = Assert.Throws<AddFriendshipRequestException>(() => user.AddFriendshipRequest(friendRequest));
            Assert.NotNull(exception);
        }

        [Fact]
        public void RemoveFriendshipRequest_ValidFriendshipRequest_RemovesFriendshipRequest()
        {
            // Arrange
            var user = InstancingHelper.GetUserInstance();
            var fromUser = InstancingHelper.GetUserInstance(); // Erstelle gültige Benutzer
            var toUser = InstancingHelper.GetUserInstance(); // Erstelle gültige Benutzer
            var friendRequest = InstancingHelper.GetFriendshipRequest(fromUser, toUser);
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
            var user = InstancingHelper.GetUserInstance();
            var fromUser = InstancingHelper.GetUserInstance(); // Erstelle gültige Benutzer
            var toUser = InstancingHelper.GetUserInstance(); // Erstelle gültige Benutzer
            var friendRequest = InstancingHelper.GetFriendshipRequest(fromUser, toUser);

            // Act and Assert
            var exception = Assert.Throws<RemoveFriendshipRequestException>(() => user.RemoveFriendshipRequest(friendRequest));
            Assert.NotNull(exception);
        }
        [Fact]
        public void AcceptFriendshipRequest_ValidFriendshipRequest_AcceptsFriendship()
        {
            // Arrange
            var user = InstancingHelper.GetUserInstance();
            var friendRequest = InstancingHelper.GetFriendshipRequest(user, InstancingHelper.GetUserInstance()); // Erstelle gültige FriendshipRequest

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
            var user = InstancingHelper.GetUserInstance();
            var friendRequest = InstancingHelper.GetFriendshipRequest(user, InstancingHelper.GetUserInstance()); // Erstelle gültige FriendshipRequest, aber akzeptiere sie nicht zuerst

            // Act and Assert
            var exception = Assert.Throws<AcceptFriendshipException>(() => user.AcceptFriendshipRequest(friendRequest));
            Assert.NotNull(exception);
        }

        [Fact]
        public void AcceptFriendshipRequest_AlreadyFriends_ThrowsAcceptFriendshipException()
        {
            // Arrange
            var user = InstancingHelper.GetUserInstance();
            var friend = InstancingHelper.GetUserInstance(); // Erstelle einen Freund
            user.AddFriend(user, friend); // Füge den Freund hinzu
            var friendRequest = InstancingHelper.GetFriendshipRequest(user, friend); // Erstelle gültige FriendshipRequest

            // Act and Assert
            var exception = Assert.Throws<AcceptFriendshipException>(() => user.AcceptFriendshipRequest(friendRequest));
            Assert.NotNull(exception);
        }
        [Fact]
        public void AddFriend_ValidFriend_AddsFriend()
        {
            // Arrange
            var user = InstancingHelper.GetUserInstance();
            var friend = InstancingHelper.GetUserInstance(); // Erstelle einen gültigen Freund

            // Act
            user.AddFriend(user,friend);

            // Assert
            Assert.Contains(friend, user.Friends.Select(x => x.Friend).ToList());
        }

        [Fact]
        public void AddFriend_AlreadyFriends_ThrowsAddFriendException()
        {
            // Arrange
            var user = InstancingHelper.GetUserInstance();
            var friend = InstancingHelper.GetUserInstance(); // Erstelle einen gültigen Freund
            user.AddFriend(user, friend); // Füge den Freund hinzu

            // Act and Assert
            var exception = Assert.Throws<AddFriendException>(() => user.AddFriend(user, friend));
            Assert.NotNull(exception);
        }
        [Fact]
        public void RemoveFriend_ValidFriend_RemovesFriend()
        {
            // Arrange
            var user = InstancingHelper.GetUserInstance();
            var friend = InstancingHelper.GetUserInstance(); // Erstelle einen gültigen Freund
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
            var user = InstancingHelper.GetUserInstance();
            var friend = InstancingHelper.GetUserInstance(); // Erstelle einen gültigen Freund

            // Act and Assert
            var exception = Assert.Throws<RemoveFriendException>(() => user.RemoveFriend(user, friend));
            Assert.NotNull(exception);
        }
        [Fact]
        public void AddRole_ValidRole_AddsRole()
        {
            // Arrange
            var user = InstancingHelper.GetUserInstance();
            var assignerUser = InstancingHelper.GetUserInstance();
            var role = InstancingHelper.GetRoleInstance(); // Erstelle eine gültige Rolle

            // Act
            user.AddRole(assignerUser, role);

            // Assert
            Assert.Contains(role, user.Roles.Select(x => x.Role).ToList());
        }

        [Fact]
        public void AddRole_AlreadyAssignedRole_ThrowsAddRoleException()
        {
            // Arrange
            var user = InstancingHelper.GetUserInstance();
            var assignerUser = InstancingHelper.GetUserInstance();
            var role = InstancingHelper.GetRoleInstance(); // Erstelle eine gültige Rolle
            user.AddRole(assignerUser, role); // Weise die Rolle bereits zu

            // Act and Assert
            var exception = Assert.Throws<AddRoleException>(() => user.AddRole(assignerUser, role));
            Assert.NotNull(exception);
        }
        [Fact]
        public void RemoveRole_ValidRole_RemovesRole()
        {
            // Arrange
            var user = InstancingHelper.GetUserInstance();
            var revokerUser = InstancingHelper.GetUserInstance();
            var role = InstancingHelper.GetRoleInstance(); // Erstelle eine gültige Rolle
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
            var user = InstancingHelper.GetUserInstance();
            var revokerUser = InstancingHelper.GetUserInstance();
            var role = InstancingHelper.GetRoleInstance(); // Erstelle eine gültige Rolle

            // Act and Assert
            var exception = Assert.Throws<RemoveRoleException>(() => user.RemoveRole(revokerUser, role));
            Assert.NotNull(exception);
        }
        [Fact]
        public void Remove_UserNotAlreadyDeleted_RemovesUser()
        {
            // Arrange
            var user = InstancingHelper.GetUserInstance();
            var deletedByUser = InstancingHelper.GetUserInstance();

            // Act
            user.Remove(deletedByUser);

            // Assert
            Assert.True(user.IsDeleted());
        }

        [Fact]
        public void Remove_UserAlreadyDeleted_ThrowsUserAlreadyDeletedException()
        {
            // Arrange
            var user = InstancingHelper.GetUserInstance();
            var deletedByUser = InstancingHelper.GetUserInstance();
            user.Remove(deletedByUser); // Markiere den Benutzer als gelöscht

            // Act and Assert
            var exception = Assert.Throws<UserAlreadyDeletedException>(() => user.Remove(deletedByUser));
            Assert.NotNull(exception);
        }
    }
}
