using Application.CQS.Messenger.Chat.Command.DeleteMessage;
using AutoMapper;
using Domain.Extension;
using Infrastructure.Abstractions;
using Infrastructure.FileSys;
using MediatR;
using System.Linq.Expressions;

namespace Application.UnitTests.UseCase.Messenger.Chat.Commands.DeleteMessage
{
    public class DeleteMessageTest
    {
        private static readonly Guid UserId = Guid.NewGuid();
        private static readonly Guid UserAdminId = Guid.NewGuid();

        private static readonly Guid ChatId = Guid.NewGuid();
        private static readonly Guid MessageId = Guid.NewGuid();

        private static readonly DeleteMessageCommand ValidCommand = new DeleteMessageCommand(UserId, ChatId, MessageId);
        private static readonly DeleteMessageCommand InvalidDeletedByCommand = ValidCommand with { DeletedById = Guid.Empty };
        private static readonly DeleteMessageCommand InvalidChatIdCommand = ValidCommand with { ChatId = Guid.Empty };
        private static readonly DeleteMessageCommand InvalidMessageIdCommand = ValidCommand with { MessageId = Guid.Empty };

        private readonly DeleteMessageCommandHandler _handler;
        private readonly IChatRepository _chatRepositoryMock;
        private readonly IUserRepository _userRepositoryMock;
        private readonly IMediaService _mediaServiceMock;
        private readonly IMediator _mediatorMock;
        private readonly IMapper _mapperMock;
        private readonly IMessageRepository _messageRepository;

        private static readonly Domain.Entities.Users.User UserAdminInstance = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance(UserAdminId);
        private static readonly Domain.Entities.Users.User UserInstance = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance(UserId);
        private readonly Domain.Entities.Chats.Chat ChatInstance = SharedTest.DomainTestInstance.Entity.Chats.InstancingHelper.GetChatInstance(UserInstance);

        public DeleteMessageTest()
        {
            _chatRepositoryMock = Substitute.For<IChatRepository>();
            _userRepositoryMock = Substitute.For<IUserRepository>();
            _mediaServiceMock = Substitute.For<IMediaService>();
            _mediatorMock = Substitute.For<IMediator>();
            _mapperMock = Substitute.For<IMapper>();
            _messageRepository = Substitute.For<IMessageRepository>();

            _handler = new DeleteMessageCommandHandler(
                _mediatorMock,
                _mapperMock,
                _userRepositoryMock,
                _mediaServiceMock,
                _messageRepository);
        }

        [Fact]
        public async Task Handle_ValidRequest_ReturnsSuccessResult()
        {
            // Arrange

            var message = ChatInstance.AddMessage(UserInstance, "test", null);
            var command = ValidCommand with { MessageId = message.Id.ToGuid() };

            _userRepositoryMock.GetAsync(Arg.Any<Expression<Func<Infrastructure.DatabaseEntity.User, bool>>>())
                .Returns(Task.FromResult(UserInstance));
            _messageRepository.GetAsync(Arg.Any<Expression<Func<Infrastructure.DatabaseEntity.Message, bool>>>())
                .Returns(Task.FromResult(message));
            _chatRepositoryMock.GetAsync(Arg.Any<Expression<Func<Infrastructure.DatabaseEntity.Chat, bool>>>())
                .Returns(Task.FromResult(ChatInstance));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task Handle_InvalidDeletedBy_ReturnsFailureResult()
        {
            // Arrange

            ChatInstance.AddMember(UserInstance, UserAdminInstance);
            var message = ChatInstance.AddMessage(UserInstance, "test", null);

            _userRepositoryMock.GetAsync(Arg.Any<Expression<Func<Infrastructure.DatabaseEntity.User, bool>>>())
                .Returns(Task.FromResult(UserInstance));
            _chatRepositoryMock.GetAsync(Arg.Any<Expression<Func<Infrastructure.DatabaseEntity.Chat, bool>>>())
                .Returns(Task.FromResult(ChatInstance));

            // Act
            var result = await _handler.Handle(InvalidDeletedByCommand, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
        }

        [Fact]
        public async Task Handle_InvalidChatId_ReturnsFailureResult()
        {
            // Arrange
            ChatInstance.AddMember(UserInstance, UserAdminInstance);
            var message = ChatInstance.AddMessage(UserInstance, "test", null);

            _userRepositoryMock.GetAsync(Arg.Any<Expression<Func<Infrastructure.DatabaseEntity.User, bool>>>())
                .Returns(Task.FromResult(UserInstance));
            _chatRepositoryMock.GetAsync(Arg.Any<Expression<Func<Infrastructure.DatabaseEntity.Chat, bool>>>())
                .Returns(Task.FromResult(ChatInstance));

            // Act
            var result = await _handler.Handle(InvalidChatIdCommand, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
        }

        [Fact]
        public async Task Handle_InvalidMessageId_ReturnsFailureResult()
        {
            // Arrange
            ChatInstance.AddMember(UserInstance, UserAdminInstance);
            var message = ChatInstance.AddMessage(UserInstance, "test", null);

            _userRepositoryMock.GetAsync(Arg.Any<Expression<Func<Infrastructure.DatabaseEntity.User, bool>>>())
                .Returns(Task.FromResult(UserInstance));
            _chatRepositoryMock.GetAsync(Arg.Any<Expression<Func<Infrastructure.DatabaseEntity.Chat, bool>>>())
                .Returns(Task.FromResult(ChatInstance));

            // Act
            var result = await _handler.Handle(InvalidMessageIdCommand, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
        }
    }
}
