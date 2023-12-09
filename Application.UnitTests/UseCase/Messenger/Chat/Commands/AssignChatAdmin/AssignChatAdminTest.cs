using Application.CQS.Messenger.Chat.Command.AssignChatAdmin;
using AutoMapper;
using Domain.Entities.User;
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
        private readonly IUnitOfWork _unitOfWorkMock;
        private readonly IMediator mediator;

        private static readonly Domain.Entities.User.User UserAdminInstance = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserInstance(ActorId);
        private static readonly Domain.Entities.User.User UserInstance = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserInstance(UserId);
        private static readonly Domain.Entities.Chats.Chat ChatInstance = SharedTest.DomainTestInstance.Entity.Chats.InstancingHelper.GetChatInstance(UserAdminInstance,ChatId);

        public AssignChatAdminCommandHandlerTests()
        {
            _mapperMock = Substitute.For<IMapper>();
            _chatRepositoryMock = Substitute.For<IChatRepository>();
            _userRepositoryMock = Substitute.For<IUserRepository>();
            _mediaServiceMock = Substitute.For<IMediaService>();
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();
            mediator = Substitute.For<IMediator>();

            _handler = new AssignChatAdminCommandHandler(
                _mapperMock,
                mediator,
                _chatRepositoryMock,
                _userRepositoryMock,
                _mediaServiceMock,
                _unitOfWorkMock); 
        }

        [Fact]
        public async Task Handle_ValidRequest_ReturnsSuccessResult()
        {
            // Arrange
            var command = ValidCommand;
            ChatInstance.AddMember(UserAdminInstance, UserInstance);
            _userRepositoryMock.GetAsync(Arg.Any<Expression<Func<Infrastructure.DatabaseEntity.User, bool>>>())
                .Returns(Task.FromResult(UserAdminInstance));
            _chatRepositoryMock.GetAsync(Arg.Any<Expression<Func<Infrastructure.DatabaseEntity.Chat, bool>>>())
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

            _userRepositoryMock.GetAsync(Arg.Any<Expression<Func<Infrastructure.DatabaseEntity.User, bool>>>())
                .Returns(Task.FromResult(UserAdminInstance));
            _chatRepositoryMock.GetAsync(Arg.Any<Expression<Func<Infrastructure.DatabaseEntity.Chat, bool>>>())
                .Returns(Task.FromResult(ChatInstance));

            // Act
            var result = await _handler.Handle(invalidCommand, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
        }
    }
}
