using Infrastructure.DatabaseEntity;
using Infrastructure.Mapper;

namespace Infrastructure_Test.Mapper.DatabaseToEntity
{
    public class MapTest
    {
        //NameingConvention for TestMethods: [ThingToTest]_Should_[ExspectedResult]_[Conditions]
        //Names in Brackets are Variable, without are the oposite (constant)

        public MapTest()
        {

        }
        [Theory]
        [ClassData(typeof(UserDatabaseEntityTestData))]
        public void UserDatabaseEntity_ShouldMapTo_UserDomainEntity_WithoutNull(User user)
        {
            var t1 = user.UserFriendFriendUserUus.GetType();
            var result = user.MapToDomainEntity<Domain.Entities.User.User, User>(true);
            Assert.NotNull(result);
            Assert.NotNull(result.Friends);
        }
    }
    public class UserDatabaseEntityTestData : TheoryData<User>
    {
        public UserDatabaseEntityTestData() {
            var friend1 = new User
            {
                Uuid = Guid.NewGuid(),
                UserTypeUu = new UserType { Name = "testusertype", CreatedTime = DateTime.Now, Uuid = Guid.NewGuid() },
                ActivationCode = "1234",
                ActivationDatetime = DateTime.Now,
                ActivationToken = "fdfgddfgdfgdfgfdg",
                Deleted = 0,
                FirstName = "Test",
                LastName = "Test",
                DateOfBirth = DateTime.Now,
                UserTypeUuid = Guid.NewGuid(),
                UserName = "Test",
                Phone = "+49176123456",
                Password = "Password",
                CreatedTime = DateTime.Now,
                DeletedTime = DateTime.Now,
                LastModifiedTime = DateTime.Now,
            };
            var user = new User
            {
                Uuid = Guid.NewGuid(),
                UserTypeUu = new UserType { Name = "testusertype", CreatedTime = DateTime.Now, Uuid = Guid.NewGuid() },
                ActivationCode = "1234",
                ActivationDatetime = DateTime.Now,
                ActivationToken = "fdfgddfgdfgdfgfdg",
                Deleted = 0,
                FirstName = "Test",
                LastName = "Test",
                DateOfBirth = DateTime.Now,
                UserTypeUuid = Guid.NewGuid(),
                UserName = "Test",
                Phone = "+49176123456",
                Password = "Password",
                CreatedTime = DateTime.Now,
                DeletedTime = DateTime.Now,
                LastModifiedTime = DateTime.Now,
            };
            user.UserFriendUserUus = new List<UserFriend> { new UserFriend { Uuid = Guid.NewGuid(), UserUu = user, FriendUserUu = friend1 }, new UserFriend { Uuid = Guid.NewGuid(), UserUu = user, FriendUserUu = friend1 } };
            Add(user);
        }
    }
}