using Application.CQS.Messenger.Chat.Command.AddChatMember;
using AutoMapper;
using Infrastructure.Abstractions;
using Infrastructure.FileSys;
using MediatR;
using System.Linq.Expressions;

namespace Application.UnitTests.UseCase.Messenger.Chat.Commands.AddChatMember
{
    public class AddChatMemberTest
    {
        private static readonly Guid ActorId = Guid.NewGuid();
        private static readonly Guid ChatId = Guid.NewGuid();
        private static readonly Guid UserId = Guid.NewGuid();

        private static readonly AddChatMemberCommand ValidCommand = new AddChatMemberCommand(
            ActorId,
            ChatId,
            UserId);

        private readonly AddChatMemberCommandHandler _handler;

        private readonly IMediaService _mediaService;
        private readonly IMapper _mapperMock;
        private readonly IUserRepository _userRepositoryMock;
        private readonly IChatRepository _chatRepository;
        private readonly IRoleRepository _roleRepositoryMock;
        private readonly IUnitOfWork _unitOfWorkMock;
        private readonly IMediator _mediatorMock;

        private static readonly Domain.Entities.User.User UserAdminInstance = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserInstance(ActorId);
        private static readonly Domain.Entities.User.User UserInstance = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserInstance(UserId);
        private static readonly Domain.Entities.Chats.Chat ChatInstance = SharedTest.DomainTestInstance.Entity.Chats.InstancingHelper.GetChatInstance(UserAdminInstance, ChatId);

        public AddChatMemberTest()
        {

            _mapperMock = Substitute.For<IMapper>();
            _userRepositoryMock = Substitute.For<IUserRepository>();
            _chatRepository = Substitute.For<IChatRepository>();
            _roleRepositoryMock = Substitute.For<IRoleRepository>();
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();
            _mediatorMock = Substitute.For<IMediator>();
            _handler = new AddChatMemberCommandHandler(
                _mapperMock,
                _mediatorMock,
                _chatRepository,
                _userRepositoryMock,
                _mediaService,
                _unitOfWorkMock);
        }

        [Fact]
        public async Task Handle_ValidRequest_ReturnsSuccessResult()
        {
            // Arrange
            var command = ValidCommand;

            _userRepositoryMock.GetAsync(Arg.Any<Expression<Func<Infrastructure.DatabaseEntity.User, bool>>>())
                .Returns(UserAdminInstance, UserInstance);
            _chatRepository.GetAsync(Arg.Any<Expression<Func<Infrastructure.DatabaseEntity.Chat, bool>>>())
                .Returns(Task.FromResult(ChatInstance));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
        }
    }
}
