using Application.CQS.Messenger.Chat.Command.CreateMessage;
using AutoMapper;
using Infrastructure.Abstractions;
using MediatR;
using Shared.DataTransferObject.Messenger;
using Shared.Infrastructure.FileSys;
using System.Linq.Expressions;

namespace Application.UnitTests.UseCase.Messenger.Chat.Commands.CreateMessage
{
    public class CreateMessageCommandTests
    {
        private static readonly Guid ChatId = Guid.NewGuid();
        private static readonly Guid UserId = Guid.NewGuid();
        private static readonly Guid UserAdminId = Guid.NewGuid();

        private static readonly CreateMessageCommand Command = new CreateMessageCommand(
                ChatId: ChatId,
                MessageCreatorId: UserId,
                Messages: new List<MessageDTO>
                {
                    new MessageDTO
                    {
                        Text = "Hello, World!",
                        CreatedTime = DateTime.UtcNow,
                        // Add other properties as needed
                    }
                });

        private readonly CreateMessageCommandHandler _handler;
        private readonly IMapper _mapperMock;
        private readonly IChatRepository _chatRepositoryMock;
        private readonly IMessageRepository _messageRepository;
        private readonly IMessageOutboxRepository _messageOutboxRepository;
        private readonly IUserRepository _userRepositoryMock;
        private readonly IMediaService _mediaServiceMock;
        private readonly IMediator _mediatorMock;

        private static readonly Domain.Entities.Users.User UserAdminInstance = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance(UserAdminId);
        private static readonly Domain.Entities.Users.User UserInstance = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance(UserId);
        private static readonly Domain.Entities.Chats.Chat ChatInstance = SharedTest.DomainTestInstance.Entity.Chats.InstancingHelper.GetChatInstance(UserAdminInstance, ChatId);

        public CreateMessageCommandTests()
        {
            _mapperMock = Substitute.For<IMapper>();
            _chatRepositoryMock = Substitute.For<IChatRepository>();
            _messageRepository = Substitute.For<IMessageRepository>();
            _messageOutboxRepository = Substitute.For<IMessageOutboxRepository>();
            _userRepositoryMock = Substitute.For<IUserRepository>();
            _mediaServiceMock = Substitute.For<IMediaService>();
            _mediatorMock = Substitute.For<IMediator>();

            _handler = new CreateMessageCommandHandler(
                _mediatorMock,
                _mapperMock,
                Substitute.For<IAntiVirus>(),
                Substitute.For<IAzureAdultContentDetection>(),
                _chatRepositoryMock,
                _messageRepository,
                _messageOutboxRepository,
                _userRepositoryMock,
                _mediaServiceMock);
        }

        [Fact]
        public async Task HandleCommand_ValidRequest_ReturnsSuccessResult()
        {
            // Arrange
            var validCommand = Command;
            ChatInstance.AddMember(UserAdminInstance.Id, UserInstance.Id);
            _chatRepositoryMock.GetAsync(Arg.Any<Expression<Func<Domain.Entities.Chats.Chat, bool>>>())
                .Returns(Task.FromResult(ChatInstance));
            // Act
            var result = await _handler.Handle(validCommand, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
        }

    }
}
