using Application.CQS.Messenger.Chat.Command.RemoveChatMember;
using AutoMapper;
using Infrastructure.Abstractions;
using Infrastructure.FileSys;
using MediatR;
using System.Linq.Expressions;

namespace Application.UnitTests.UseCase.Messenger.Chat.Commands.RemoveChatMember
{
    public class RemoveChatMemberTest
    {
        private static readonly Guid ActorId = Guid.NewGuid();
        private static readonly Guid ChatId = Guid.NewGuid();
        private static readonly Guid UserId = Guid.NewGuid();

        private static readonly RemoveChatMemberCommand ValidCommand = new RemoveChatMemberCommand(ActorId, ChatId, UserId);
        private static readonly RemoveChatMemberCommand InvalidActorCommand = ValidCommand with { ActorId = Guid.Empty };
        private static readonly RemoveChatMemberCommand InvalidChatCommand = ValidCommand with { ChatId = Guid.Empty };
        private static readonly RemoveChatMemberCommand InvalidUserCommand = ValidCommand with { UserId = Guid.Empty };

        private readonly RemoveChatMemberCommandHandler _handler;
        private readonly IChatRepository _chatRepositoryMock;
        private readonly IUserRepository _userRepositoryMock;
        private readonly IMediaService _mediaServiceMock;
        private readonly IMapper _mapperMock;
        private readonly IMediator _mediator;

        private static readonly Domain.Entities.User.User UserAdminInstance = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserInstance(ActorId);
        private static readonly Domain.Entities.User.User UserInstance = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserInstance(UserId);
        private static readonly Domain.Entities.Chats.Chat ChatInstance = SharedTest.DomainTestInstance.Entity.Chats.InstancingHelper.GetChatInstance(UserAdminInstance);

        public RemoveChatMemberTest()
        {
            _chatRepositoryMock = Substitute.For<IChatRepository>();
            _userRepositoryMock = Substitute.For<IUserRepository>();
            _mediaServiceMock = Substitute.For<IMediaService>();
            _mapperMock = Substitute.For<IMapper>();
            _mediator = Substitute.For<IMediator>();

            _handler = new RemoveChatMemberCommandHandler(
                _mapperMock,
                _mediator,
                _chatRepositoryMock,
                _userRepositoryMock,
                _mediaServiceMock);
        }

        [Fact]
        public async Task Handle_ValidRequest_ReturnsSuccessResult()
        {
            // Arrange
            _userRepositoryMock.GetAsync(Arg.Any<Expression<Func<Infrastructure.DatabaseEntity.User, bool>>>())
                .Returns(Task.FromResult(UserAdminInstance));
            ChatInstance.AddMember(UserAdminInstance,UserInstance);
            _chatRepositoryMock.GetAsync(Arg.Any<Expression<Func<Infrastructure.DatabaseEntity.Chat, bool>>>())
                .Returns(Task.FromResult(ChatInstance));

            // Act
            var result = await _handler.Handle(ValidCommand, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.True(result.Value);
        }

        [Fact]
        public async Task Handle_InvalidActor_ReturnsFailureResult()
        {
            // Arrange
            _userRepositoryMock.GetAsync(Arg.Any<Expression<Func<Infrastructure.DatabaseEntity.User, bool>>>())
                .Returns(Task.FromResult(UserAdminInstance));

            _chatRepositoryMock.GetAsync(Arg.Any<Expression<Func<Infrastructure.DatabaseEntity.Chat, bool>>>())
                .Returns(Task.FromResult(ChatInstance));

            // Act
            var result = await _handler.Handle(InvalidActorCommand, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
        }

        [Fact]
        public async Task Handle_InvalidChat_ReturnsFailureResult()
        {
            // Arrange
            _userRepositoryMock.GetAsync(Arg.Any<Expression<Func<Infrastructure.DatabaseEntity.User, bool>>>())
                .Returns(Task.FromResult(UserAdminInstance));

            _chatRepositoryMock.GetAsync(Arg.Any<Expression<Func<Infrastructure.DatabaseEntity.Chat, bool>>>())
                .Returns(Task.FromResult(ChatInstance));

            // Act
            var result = await _handler.Handle(InvalidChatCommand, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
        }

        [Fact]
        public async Task Handle_InvalidUser_ReturnsFailureResult()
        {
            // Arrange
            _userRepositoryMock.GetAsync(Arg.Any<Expression<Func<Infrastructure.DatabaseEntity.User, bool>>>())
                .Returns(Task.FromResult(UserAdminInstance));

            _chatRepositoryMock.GetAsync(Arg.Any<Expression<Func<Infrastructure.DatabaseEntity.Chat, bool>>>())
                .Returns(Task.FromResult(ChatInstance));

            // Act
            var result = await _handler.Handle(InvalidUserCommand, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
        }

    }
}

