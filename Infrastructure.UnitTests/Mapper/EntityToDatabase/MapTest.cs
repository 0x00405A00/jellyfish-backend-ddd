using Domain.Entities.Role;
using Domain.Entities.User;
using Domain.Primitives;
using Domain.ValueObjects;
using Infrastructure;
using Infrastructure.Abstractions;
using Infrastructure.Mapper;
using Moq;

namespace Infrastructure_Test.Mapper.EntityToDatabase
{
    public class MapTest
    {
        //NameingConvention for TestMethods: [ThingToTest]_Should_[ExspectedResult]_[Conditions]
        //Names in Brackets are Variable, without are the oposite (constant)
        public Mock<IUserRepository> mock = new Mock<IUserRepository>();
        public MapTest()
        {
        }
        [Theory]
        [ClassData(typeof(UserDomainEntityTestData))]
        public void UserDomainEntity_ShouldMapTo_DatabaseEntity_WithoutNull(User user)
        {
            try
            {
                var result = user.MapToDatabaseEntity<User,Infrastructure.DatabaseEntity.User>(false); 
                Assert.Null(result);
            }
            catch (Exception ex)
            {


                Assert.Fail(ex.ToString());
            }
        }

        [Theory]
        [ClassData(typeof(UserDomainEntityTestData))]
        public void MapToDatabaseEntity_ValidUser_MapsCorrectly(User user)
        {
            // Arrange


            // Act
            var result = user.MapToDatabaseEntity<User, Infrastructure.DatabaseEntity.User>(true);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.Uuid.ToGuid(), result.Uuid);
            Assert.Equal(user.UserType.Uuid.ToGuid(), result.UserTypeUuid);
            Assert.Equal(user.UserName, result.UserName);
            Assert.Equal(user.Email.EmailValue, result.Email);
            Assert.Equal(user.Password, result.Password);
            Assert.Equal(user.Phone.PhoneNumb, result.Phone);
            Assert.Equal(user.DateOfBirth, DateOnly.FromDateTime(result.DateOfBirth));
            Assert.Equal(user.ActivationDateTime, result.ActivationDatetime);
            Assert.Equal(user.CreatedTime, result.CreatedTime);
            Assert.Equal(user.LastModifiedTime, result.LastModifiedTime);

            // Additional assertions for friends
            Assert.NotNull(result.UserFriendFriendUserUus);
            Assert.Equal(2, result.UserFriendFriendUserUus.Count); // Assuming two friends were added.

            // Add more assertions for friend properties if needed
        }
    }
    public class UserDomainEntityTestData : TheoryData<User>
    {
        public UserDomainEntityTestData()
        {
            var systemUser = User.GetSystemUser();

            var role = Role.Create(new RoleId(),"role",null,DateTime.Now,null,null);


            var friend = User.Create(
                Guid.NewGuid().ToIdentification<UserId>(),
                UserType.Create(new UserTypeId(Guid.NewGuid()), "testusertype", DateTime.Now, null, null),
                "otheruser",
                "mypass",
                "otheruser",
                "otheruser",
                Email.Parse("email@email.de"),
                PhoneNumber.Parse("+49176000123"),
                null,
                null,
                null,
                null,
                DateOnly.Parse("1996-06-05"),
                null,
                DateTime.Now,
                null,
                null,
                systemUser);

            var user = User.Create(
                Guid.NewGuid().ToIdentification<UserId>(),
                UserType.Create(new UserTypeId(Guid.NewGuid()), "testusertype", DateTime.Now, null, null),
                "username",
                "mypass",
                "firstname",
                "lastname",
                Email.Parse("email@email.de"),
                PhoneNumber.Parse("+49176000123"),
                null,
                null,
                null,
                null,
                DateOnly.Parse("1996-06-05"),
                null,
                DateTime.Now,
                null,
                null,
                systemUser);

            //friend ist hier requester für Test
            var friendshipRequest = FriendshipRequest.Create("do you want to be my friend",friend,user);

            user.AddFriend(systemUser, friend);
            user.AddFriendshipRequest(friendshipRequest);
            user.AddRole(systemUser, role);
            Add(user);
        }
    }
}