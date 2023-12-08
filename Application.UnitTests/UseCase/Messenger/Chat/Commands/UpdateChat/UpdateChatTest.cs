using Application.CQS.Messenger.Chat.Command.UpdateChat;
using AutoMapper;
using Infrastructure.Abstractions;
using System.Linq.Expressions;

namespace Application.UnitTests.UseCase.Messenger.Chat.Commands.UpdateChat
{
    public class UpdateChatTest
    {
        private static readonly Guid UpdatedByUserId = Guid.NewGuid();
        private static readonly Guid ChatId = Guid.NewGuid();
        private static readonly string ValidChatName = "ValidChatName";
        private static readonly string ValidChatDescription = "ValidChatDescription";
        private static readonly string ValidPictureBase64 = "ValidBase64String";
        private static readonly string ValidPictureMimeType = "image/png";

        private static readonly UpdateChatCommand ValidCommand = new UpdateChatCommand(
            UpdatedByUserId,
            ChatId,
            ValidChatName,
            ValidChatDescription,
            ValidPictureBase64,
            ValidPictureMimeType);

        // Add more commands for invalid scenarios if needed

        private readonly UpdateChatCommandHandler _handler;
        private readonly IMapper _mapperMock;
        private readonly IChatRepository _chatRepositoryMock;
        private readonly IUserRepository _userRepositoryMock;
        private readonly IUnitOfWork _unitOfWorkMock;

        private static readonly Domain.Entities.User.User UserAdminInstance = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserInstance();
        private static readonly Domain.Entities.User.User UserInstance = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserInstance();
        private static readonly Domain.Entities.Chats.Chat ChatInstance = SharedTest.DomainTestInstance.Entity.Chats.InstancingHelper.GetChatInstance(UserAdminInstance);

        public UpdateChatTest()
        {
            _mapperMock = Substitute.For<IMapper>();
            _chatRepositoryMock = Substitute.For<IChatRepository>();
            _userRepositoryMock = Substitute.For<IUserRepository>();
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();
            _handler = new UpdateChatCommandHandler(
                _mapperMock,
                _chatRepositoryMock,
                _userRepositoryMock,
                _unitOfWorkMock);
        }

        [Fact]
        public async Task Handle_ValidRequest_ReturnsSuccessResult()
        {
            // Arrange
            var handler = _handler;

            _userRepositoryMock.GetAsync(Arg.Any<Expression<Func<Infrastructure.DatabaseEntity.User, bool>>>())
                .Returns(Task.FromResult(UserInstance));

            _chatRepositoryMock.GetAsync(Arg.Any<Expression<Func<Infrastructure.DatabaseEntity.Chat, bool>>>())
                .Returns(Task.FromResult(ChatInstance));

            // Act
            var result = await handler.Handle(ValidCommand, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            // Add more assertions based on your specific success conditions
        }
    }
}
