using Application.CQS.Messenger.Chat.Command.UpdateChat;
using AutoMapper;
using Infrastructure.Abstractions;
using System.Linq.Expressions;
using System.Text;

namespace Application.UnitTests.UseCase.Messenger.Chat.Commands.UpdateChat
{
    public class UpdateChatTest
    {
        private static readonly Guid ChatId = Guid.NewGuid();
        private static readonly Guid UserId = Guid.NewGuid();
        private static readonly Guid ChatAdminUserId = Guid.NewGuid();
        private static readonly string ValidChatName = "ValidChatName";
        private static readonly string ValidChatDescription = "ValidChatDescription";
        private static readonly string ValidPictureBase64 = "ValidBase64String";
        private static readonly string ValidPictureMimeType = "image/png";

        private static readonly UpdateChatCommand ValidCommand = new UpdateChatCommand(
            ChatAdminUserId,
            ChatId,
            ValidChatName,
            ValidChatDescription,
            Convert.ToBase64String(Encoding.UTF8.GetBytes(ValidPictureBase64)),
            ValidPictureMimeType);

        private static readonly UpdateChatCommand InvalidUserIsNoAdminCommand = ValidCommand with { UpdatedByUserId = UserId };

        // Add more commands for invalid scenarios if needed

        private readonly UpdateChatCommandHandler _handler;
        private readonly IMapper _mapperMock;
        private readonly IChatRepository _chatRepositoryMock;
        private readonly IUserRepository _userRepositoryMock;

        private static readonly Domain.Entities.User.User UserAdminInstance = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserInstance(ChatAdminUserId);
        private static readonly Domain.Entities.User.User UserInstance = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserInstance(UserId);
        private static readonly Domain.Entities.Chats.Chat ChatInstance = SharedTest.DomainTestInstance.Entity.Chats.InstancingHelper.GetChatInstance(UserAdminInstance,ChatId);

        public UpdateChatTest()
        {
            _mapperMock = Substitute.For<IMapper>();
            _chatRepositoryMock = Substitute.For<IChatRepository>();
            _userRepositoryMock = Substitute.For<IUserRepository>();
            _handler = new UpdateChatCommandHandler(
                _mapperMock,
                _chatRepositoryMock,
                _userRepositoryMock);
        }

        [Fact]
        public async Task Handle_ValidRequest_UserHasPermissions_ReturnsSuccessResult()
        {
            // Arrange
            var handler = _handler;

            _userRepositoryMock.GetAsync(Arg.Any<Expression<Func<Infrastructure.DatabaseEntity.User, bool>>>())
                .Returns(Task.FromResult(UserAdminInstance));

            _chatRepositoryMock.GetAsync(Arg.Any<Expression<Func<Infrastructure.DatabaseEntity.Chat, bool>>>())
                .Returns(Task.FromResult(ChatInstance));

            // Act
            var result = await handler.Handle(ValidCommand, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            // Add more assertions based on your specific success conditions
        }
        [Fact]
        public async Task Handle_InvalidRequest_UserHasNoPermissions_ReturnsFailureResult()
        {
            // Arrange
            var handler = _handler;

            _userRepositoryMock.GetAsync(Arg.Any<Expression<Func<Infrastructure.DatabaseEntity.User, bool>>>())
                .Returns(Task.FromResult(UserInstance));

            _chatRepositoryMock.GetAsync(Arg.Any<Expression<Func<Infrastructure.DatabaseEntity.Chat, bool>>>())
                .Returns(Task.FromResult(ChatInstance));

            // Act
            var result = await handler.Handle(InvalidUserIsNoAdminCommand, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            // Add more assertions based on your specific success conditions
        }
    }
}
