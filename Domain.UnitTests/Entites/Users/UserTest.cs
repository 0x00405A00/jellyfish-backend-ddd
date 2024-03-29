﻿using Domain.Const;
using Domain.Entities.Users;
using Domain.Entities.Users.Exceptions;
using Domain.Exceptions;
using Domain.Extension;
using Domain.Primitives.Ids;
using Domain.ValueObjects;

namespace Domain.UnitTests.Entites.Users
{
    public class UserTest
    {
        public static User CreatedBy = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
        public static User ModifiedBy = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
        public static UserType UserType = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserTypeInstance();

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
            var phone = PhoneNumber.Parse("+491764567213");
            var dateOfBirth = new DateTime(1980,12,31);
            var createdTime = DateTime.UtcNow;

            var role = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetRoleUserInstance();
            var userRole = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserRoleInstance(role,id);
            List<UserHasRelationToRole> userRoles = new List<UserHasRelationToRole>() { userRole };

            //Act
            var result = User.Create(
                id,
                UserType.Id,
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

            // Assert
            Assert.NotNull(result);
            Assert.Equal(id, result.Id);
            Assert.Equal(UserType.Id, result.UserTypeForeignKey);
            Assert.Equal(userName, result.UserName);
            // Add more assertions based on your object properties
        }

        [Fact]
        public void Create_ValidParameters_RootUser()
        {
            // Arrange & Act
            var user = User.GetSystemUser(); // Hier einen gültigen UserType einsetzen

            // Assert
            Assert.NotNull(user);
        }

        [Fact]
        public void UpdateUserType_ValidUserType_UpdatesUserType()
        {
            // Arrange
            var user = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var modifiedUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var newUserType = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserTypeInstance(); // Hier einen gültigen UserType einsetzen

            // Act
            user.UpdateUserType(modifiedUser.Id, newUserType);

            // Assert
            Assert.Equal(newUserType, user.UserType);
        }

        [Fact]
        public void UpdateUserType_NullUserType_ThrowsArgumentNullException()
        {
            // Arrange
            var user = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var modifiedUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            UserType invalidUserType = null;

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() => user.UpdateUserType(modifiedUser.Id, invalidUserType));
            Assert.NotNull(exception);
        }

        [Fact]
        public void UpdateUserName_ValidUserName_UpdatesUserName()
        {
            // Arrange
            var user = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var modifiedUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var newUserName = "newUserName"; // Hier einen gültigen Benutzernamen einsetzen

            // Act
            user.UpdateUserName(modifiedUser.Id, newUserName);

            // Assert
            Assert.Equal(newUserName, user.UserName);
        }

        [Fact]
        public void UpdateUserName_InvalidUserName_ThrowsInvalidUserNameException()
        {
            // Arrange
            var user = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var modifiedUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            string invalidUserName = null;

            // Act and Assert
            var exception = Assert.Throws<InvalidUserNameException>(() => user.UpdateUserName(modifiedUser.Id, invalidUserName));
            Assert.NotNull(exception);
        }
        [Fact]
        public void UpdateFirstName_ValidFirstName_UpdatesFirstName()
        {
            // Arrange
            var user = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var modifiedUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var newFirstName = "John";

            // Act
            user.UpdateFirstName(modifiedUser.Id, newFirstName);

            // Assert
            Assert.Equal(newFirstName, user.FirstName);
        }

        [Fact]
        public void UpdateFirstName_InvalidFirstName_ThrowsInvalidFirstNameException()
        {
            // Arrange
            var user = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var modifiedUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            string invalidFirstName = null;

            // Act and Assert
            var exception = Assert.Throws<InvalidFirstNameException>(() => user.UpdateFirstName(modifiedUser.Id, invalidFirstName));
            Assert.NotNull(exception);
        }

        [Fact]
        public void UpdateEmail_ValidEmail_UpdatesEmail()
        {
            // Arrange
            var user = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var modifiedUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var newEmail = Email.Parse("new@example.com");

            // Act
            user.UpdateEmail(modifiedUser.Id, newEmail);

            // Assert
            Assert.Equal(newEmail, user.Email);
        }

        [Fact]
        public void UpdateEmail_NullEmail_ThrowsArgumentNullException()
        {
            // Arrange
            var user = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var modifiedUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            Email invalidEmail = null;

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() => user.UpdateEmail(modifiedUser.Id, invalidEmail));
            Assert.NotNull(exception);
        }

        [Fact]
        public void UpdatePhone_ValidPhone_UpdatesPhone()
        {
            // Arrange
            var user = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var modifiedUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var newPhone = PhoneNumber.Parse("+491764567213");

            // Act

            user.UpdatePhone(modifiedUser.Id, newPhone);

            //Assert 
            Assert.Equal(newPhone.PhoneNumb, user.Phone.PhoneNumb);
        }

        [Fact]
        public void UpdatePhone_NullPhone_ThrowsArgumentNullException()
        {
            // Arrange
            var user = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var modifiedUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            PhoneNumber invalidPhone = null;

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() => user.UpdatePhone(modifiedUser.Id, invalidPhone));
            Assert.NotNull(exception);
        }
        [Fact]
        public void AddFriendshipRequest_ValidFriendshipRequest_AddsFriendshipRequest()
        {
            // Arrange
            var user = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var fromUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance(); // Erstelle gültige Benutzer
            var toUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance(); // Erstelle gültige Benutzer
            var friendRequest = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetFriendshipRequest(fromUser, toUser);

            // Act
            user.AddFriendshipRequest(friendRequest);

            // Assert
            Assert.Contains(friendRequest.RequestUserForeignKey, user.FriendshipRequestsWhereIamRequester.Select(x=>x.RequestUserForeignKey).ToList());
        }

        [Fact]
        public void AddFriendshipRequest_DuplicateFriendshipRequest_ThrowsAddFriendshipRequestException()
        {
            // Arrange
            var user = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var fromUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance(); // Erstelle gültige Benutzer
            var toUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance(); // Erstelle gültige Benutzer
            var friendRequest = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetFriendshipRequest(fromUser, toUser);
            user.AddFriendshipRequest(friendRequest);

            // Act and Assert
            var exception = Assert.Throws<AddFriendshipRequestException>(() => user.AddFriendshipRequest(friendRequest));
            Assert.NotNull(exception);
        }

        [Fact]
        public void RemoveFriendshipRequest_ValidFriendshipRequest_RemovesFriendshipRequest()
        {
            // Arrange
            var user = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var fromUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance(); // Erstelle gültige Benutzer
            var toUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance(); // Erstelle gültige Benutzer
            var friendRequest = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetFriendshipRequest(fromUser, toUser);
            user.AddFriendshipRequest(friendRequest);

            // Act
            user.DeclineFriendshipRequest(friendRequest);

            // Assert
            Assert.DoesNotContain(friendRequest, user.FriendshipRequestsWhereIamRequester);
        }

        [Fact]
        public void RemoveFriendshipRequest_NonExistentFriendshipRequest_ThrowsRemoveFriendshipRequestException()
        {
            // Arrange
            var user = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var fromUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance(); // Erstelle gültige Benutzer
            var toUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance(); // Erstelle gültige Benutzer
            var friendRequest = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetFriendshipRequest(fromUser, toUser);

            // Act and Assert
            var exception = Assert.Throws<RemoveFriendshipRequestException>(() => user.DeclineFriendshipRequest(friendRequest));
            Assert.NotNull(exception);
        }
        [Fact]
        public void AcceptFriendshipRequest_ValidFriendshipRequest_AcceptsFriendship()
        {
            // Arrange
            var user = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var otherUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var friendRequest = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetFriendshipRequest(user, otherUser); // Erstelle gültige FriendshipRequest

            // Act
            otherUser.AddFriendshipRequest(friendRequest);//normaly loaded via repository
            otherUser.AcceptFriendshipRequest(friendRequest);

            // Assert
            Assert.Contains(friendRequest.RequestUserForeignKey, otherUser.GetFriends().Select(x => x.UserFriendForeignKey).ToList());
            Assert.DoesNotContain(friendRequest.RequestUserForeignKey, otherUser.FriendshipRequestsWhereIamRequester.Select(x => x.RequestUserForeignKey).ToList());
        }

        [Fact]
        public void AcceptFriendshipRequest_NonExistentFriendshipRequest_ThrowsAcceptFriendshipException()
        {
            // Arrange
            var user = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var friendRequest = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetFriendshipRequest(user, SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance()); // Erstelle gültige FriendshipRequest, aber akzeptiere sie nicht zuerst

            // Act and Assert
            var exception = Assert.Throws<AcceptFriendshipException>(() => user.AcceptFriendshipRequest(friendRequest));
            Assert.NotNull(exception);
        }

        [Fact]
        public void AcceptFriendshipRequest_AlreadyFriends_ThrowsAcceptFriendshipException()
        {
            // Arrange
            var user = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var friend = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance(); // Erstelle einen Freund
            user.AddFriend(user.Id, friend.Id); // Füge den Freund hinzu
            var friendRequest = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetFriendshipRequest(user, friend); // Erstelle gültige FriendshipRequest

            // Act and Assert
            var exception = Assert.Throws<AcceptFriendshipException>(() => user.AcceptFriendshipRequest(friendRequest));
            Assert.NotNull(exception);
        }
        [Fact]
        public void AddFriend_ValidFriend_AddsFriend()
        {
            // Arrange
            var user = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var friend = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance(); // Erstelle einen gültigen Freund

            // Act
            user.AddFriend(user.Id, friend.Id);

            // Assert
            Assert.Contains(friend.Id, user.GetFriends().Select(x => x.UserFriendForeignKey).ToList());
        }

        [Fact]
        public void AddFriend_AlreadyFriends_ThrowsAddFriendException()
        {
            // Arrange
            var user = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var friend = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance(); // Erstelle einen gültigen Freund
            user.AddFriend(user.Id, friend.Id); // Füge den Freund hinzu

            // Act and Assert
            var exception = Assert.Throws<AddFriendException>(() => user.AddFriend(user.Id, friend.Id));
            Assert.NotNull(exception);
        }
        [Fact]
        public void RemoveFriend_ValidFriend_RemovesFriend()
        {
            // Arrange
            var user = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var friend = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance(); // Erstelle einen gültigen Freund
            user.AddFriend(user.Id, friend.Id); // Füge den Freund hinzu

            // Act
            user.RemoveFriend(user.Id, friend);

            // Assert
            Assert.DoesNotContain(friend, user.GetFriends().Select(x => x.UserFriend).ToList());
        }

        [Fact]
        public void RemoveFriend_NonExistentFriend_ThrowsRemoveFriendException()
        {
            // Arrange
            var user = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var friend = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance(); // Erstelle einen gültigen Freund

            // Act and Assert
            var exception = Assert.Throws<RemoveFriendException>(() => user.RemoveFriend(user.Id, friend));
            Assert.NotNull(exception);
        }
        [Fact]
        public void AddRole_ValidRole_AddsRole()
        {
            // Arrange
            var user = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var assignerUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var role = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetRoleAdminInstance(); // Erstelle eine gültige Rolle

            // Act
            user.AddRole(assignerUser.Id, role.Id);

            // Assert
            Assert.Contains(role.Id, user.UserHasRelationToRoles.Select(x => x.RoleForeignKey).ToList());
        }

        [Fact]
        public void AddRole_AlreadyAssignedRole_ThrowsAddRoleException()
        {
            // Arrange
            var user = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var assignerUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var role = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetRoleUserInstance(); // Erstelle eine gültige Rolle

            // Act and Assert
            var exception = Assert.Throws<AddRoleException>(() => user.AddRole(assignerUser.Id, role.Id));
            Assert.NotNull(exception);
        }
        [Fact]
        public void RemoveRole_ValidRole_RemovesRole()
        {
            // Arrange
            var user = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var role = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetRoleAdminInstance(); // Erstelle eine gültige Rolle
            var revokerUserId =Guid.NewGuid().ToIdentification<UserId>();
            var revokerUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance(revokerUserId.Id,adminRole: UserHasRelationToRole.NewAdmin(revokerUserId));
            user.AddRole(revokerUser.Id, role.Id); // Weise die Rolle zu

            // Act
            user.RemoveRole(revokerUser.Id, role.Id);

            // Assert
            Assert.DoesNotContain(role.Id, user.UserHasRelationToRoles.Select(x => x.RoleForeignKey).ToList());
        }

        [Fact]
        public void RemoveRole_RoleNotAssigned_ThrowsRemoveRoleException()
        {
            // Arrange
            var user = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var revokerUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var role = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetRoleAdminInstance(); // Erstelle eine gültige Rolle

            // Act and Assert
            var exception = Assert.Throws<RemoveRoleException>(() => user.RemoveRole(revokerUser.Id, role.Id));
            Assert.NotNull(exception);
        }
        [Fact]
        public void Remove_UserNotAlreadyDeleted_RemovesUser()
        {
            // Arrange
            var user = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var deletedByUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();

            // Act
            user.Remove(deletedByUser.Id);

            // Assert
            Assert.True(user.IsDeleted());
        }

        [Fact]
        public void Remove_UserAlreadyDeleted_ThrowsUserAlreadyDeletedException()
        {
            // Arrange
            var user = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var deletedByUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            user.Remove(deletedByUser.Id); // Markiere den Benutzer als gelöscht

            // Act and Assert
            var exception = Assert.Throws<UserAlreadyDeletedException>(() => user.Remove(deletedByUser.Id));
            Assert.NotNull(exception);
        }
    }
}
