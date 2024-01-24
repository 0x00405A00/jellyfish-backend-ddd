using Application.Mapper;
using AutoMapper;
using Domain.Entities.Messages;
using Domain.Primitives;
using Domain.Primitives.Ids;
using Microsoft.Extensions.Configuration;
using Shared.DataTransferObject;
using Shared.DataTransferObject.Messenger;
using SharedTest.DomainTestInstance.Entity.Chats;

namespace Application.UnitTests.Mapping
{
    public class DomainModelToChatDTOTests
    {
        private readonly IMapper _mapperMock;
        private readonly IConfiguration _configuration;
        private readonly MapperConfiguration _mapperConfiguration;

        public DomainModelToChatDTOTests()
        {

            _configuration = Substitute.For<IConfiguration>();

            // Configure the mock as needed
            _configuration["Infrastructure:ContentDelivery:DeliveryUrl"].Returns("https://jellyfish.net");

            var mappingProfile = new DomainModelToDTOMappingProfile(_configuration);
            _mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(mappingProfile));
            _mapperMock = _mapperConfiguration.CreateMapper();
        }
        [Fact]
        public void Validate_MappingProfiles()
        {
            _mapperConfiguration.AssertConfigurationIsValid();

            Assert.True(true, "Mapping is valid and no exception is there");
        }
        [Fact]
        public void Valid_MappingProfile_ChatDomainModel_Should_Map_To_Dto()
        {
            //Arrange
            var userDomainModel = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var chatDomainModel = InstancingHelper.GetChatInstance(userDomainModel);

            //Act
            var dataTransferObject = _mapperMock.Map<ChatDTO>(chatDomainModel);

            //Assert
            Assert.NotNull(dataTransferObject);
        }
        [Fact]
        public void Valid_MappingProfile_UserDomainModel_Should_Map_To_Dto()
        {
            //Arrange
            var domainModel = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();

            //Act
            var dataTransferObject = _mapperMock.Map<UserDTO>(domainModel);

            //Assert
            Assert.NotNull(dataTransferObject);
        }
        [Fact]
        public void Valid_MappingProfile_MessageDomainModel_Should_Map_To_Dto()
        {
            //Arrange
            var adminUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var chat = SharedTest.DomainTestInstance.Entity.Chats.InstancingHelper.GetChatInstance(adminUser);
            var messageId = Message.NewId();
            var domainModel = Message.Create(messageId,
                                             chat.Id,
                                             adminUser.Id,
                                             "this is a text message (test)",
                                             null,
                                             CustomDateTime.Now(),
                                             adminUser.Id,
                                             null,
                                             null,
                                             null,
                                             null);

            //Act
            var dataTransferObject = _mapperMock.Map<MessageDTO>(domainModel);

            //Assert
            Assert.NotNull(dataTransferObject);
        }
        [Fact]
        public void Valid_MappingProfile_RoleDomainModel_Should_Map_To_Dto()
        {
            //Arrange
            var domainModel = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetRoleAdminInstance();

            //Act
            var dataTransferObject = _mapperMock.Map<RoleDTO>(domainModel);

            //Assert
            Assert.NotNull(dataTransferObject);
        }
        [Fact]
        public void Valid_MappingProfile_FriendshipRequestDomainModel_Should_Map_To_Dto()
        {
            //Arrange
            var fromUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var toUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var domainModel = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetFriendshipRequest(fromUser,toUser);

            //Act
            var dataTransferObject = _mapperMock.Map<FriendshipRequestDTO>(domainModel);

            //Assert
            Assert.NotNull(dataTransferObject);
        }
        [Fact]
        public void Valid_MappingProfile_UserTypeDomainModel_Should_Map_To_Dto()
        {
            //Arrange
            var domainModel = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserTypeInstance();

            //Act
            var dataTransferObject = _mapperMock.Map<UserTypeDTO>(domainModel);

            //Assert
            Assert.NotNull(dataTransferObject);
        }
        [Theory]
        [MemberData(nameof(TestData.GetTestTypedIds), MemberType = typeof(TestData))]
        public void Valid_MappingProfile_StronglyTypedIds_Should_Map_To_Guid(object stronglyTypeId)
        {
            //Arrange
            var id = stronglyTypeId;
            var type = id.GetType();
            //Act
            var dataTransferObject = _mapperMock.Map(id,type,typeof(Guid));

            //Assert
            Assert.NotNull(dataTransferObject);
        }
        public class TestData
        {
            public static List<object[]> GetTestTypedIds()
            {
                List<object[]> response = new List<object[]>();
                var allIdTypesFromDomain = typeof(Identification).Assembly.GetTypes()
                    .Where(x => x.BaseType == typeof(Identification))
                    .ToList();
                foreach (var type in allIdTypesFromDomain)
                {
                    var ctor = type.GetConstructor(new Type[] { typeof(Guid) });
                    var instance = ctor.Invoke(new object[] { Guid.NewGuid() });
                    response.Add(new object[1] { instance });
                }
                return response;
            }
        }
    }
}
