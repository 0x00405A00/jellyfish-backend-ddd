using Application.CQS.Messenger.Chat.Command.AssignChatAdmin;
using AutoMapper;
using Infrastructure.Abstractions;
using Infrastructure.FileSys;
using MediatR;
using System.Linq.Expressions;

namespace Application.UnitTests.UseCase.Messenger.Chat.Commands.AssignChatAdmin
{
    public class AssignChatAdminCommandHandlerTests
    {
        private static readonly Guid ActorId = Guid.NewGuid();
        private static readonly Guid ChatId = Guid.NewGuid();
        private static readonly Guid UserId = Guid.NewGuid();

        private static readonly AssignChatAdminCommand ValidCommand = new AssignChatAdminCommand(ActorId,ChatId,UserId);
        
        private readonly AssignChatAdminCommandHandler _handler;
        private readonly IMapper _mapperMock;
        private readonly IChatRepository _chatRepositoryMock;
        private readonly IUserRepository _userRepositoryMock;
        private readonly IMediaService _mediaServiceMock;
        private readonly IMediator mediator;

        private static readonly Domain.Entities.Users.User UserAdminInstance = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance(ActorId);
        private static readonly Domain.Entities.Users.User UserInstance = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance(UserId);
        private readonly Domain.Entities.Chats.Chat ChatInstance = SharedTest.DomainTestInstance.Entity.Chats.InstancingHelper.GetChatInstance(UserAdminInstance,ChatId);

        public AssignChatAdminCommandHandlerTests()
        {
            _mapperMock = Substitute.For<IMapper>();
            _chatRepositoryMock = Substitute.For<IChatRepository>();
            _userRepositoryMock = Substitute.For<IUserRepository>();
            _mediaServiceMock = Substitute.For<IMediaService>();
            mediator = Substitute.For<IMediator>();

            _handler = new AssignChatAdminCommandHandler(
                _mapperMock,
                mediator,
                _chatRepositoryMock,
                _userRepositoryMock,
                _mediaServiceMock); 
        }

        [Fact]
        public async Task Handle_ValidRequest_ReturnsSuccessResult()
        {
            // Arrange
            var command = ValidCommand;
            ChatInstance.AddMember(UserAdminInstance.Id, UserInstance.Id);
            _userRepositoryMock.GetAsync(Arg.Any<Expression<Func<Domain.Entities.Users.User, bool>>>())
                .Returns(UserAdminInstance,UserInstance);
            _chatRepositoryMock.GetAsync(Arg.Any<Expression<Func<Domain.Entities.Chats.Chat, bool>>>())
                .Returns(Task.FromResult(ChatInstance));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task Handle_InvalidRequest_ReturnsFailureResult()
        {
            // Arrange
            var invalidCommand = ValidCommand with { ChatId = Guid.Empty };

            _userRepositoryMock.GetAsync(Arg.Any<Expression<Func<Domain.Entities.Users.User, bool>>>())
                .Returns(UserAdminInstance, UserInstance);
            _chatRepositoryMock.GetAsync(Arg.Any<Expression<Func<Domain.Entities.Chats.Chat, bool>>>())
                .Returns(Task.FromResult(ChatInstance));

            // Act
            var result = await _handler.Handle(invalidCommand, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
        }
    }
}
