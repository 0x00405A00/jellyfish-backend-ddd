using Application.CQS.Messenger.Chat.Command.CreateMessage;
using AutoMapper;
using Infrastructure.Abstractions;
using Infrastructure.FileSys;
using MediatR;
using Shared.DataTransferObject.Messenger;
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
                        CreatedTime = DateTime.Now,
                        // Add other properties as needed
                    }
                });

        private readonly CreateMessageCommandHandler _handler;
        private readonly IMapper _mapperMock;
        private readonly IChatRepository _chatRepositoryMock;
        private readonly IUserRepository _userRepositoryMock;
        private readonly IMediaService _mediaServiceMock;
        private readonly IMediator _mediatorMock;

        private static readonly Domain.Entities.Users.User UserAdminInstance = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserInstance(UserAdminId);
        private static readonly Domain.Entities.Users.User UserInstance = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserInstance(UserId);
        private static readonly Domain.Entities.Chats.Chat ChatInstance = SharedTest.DomainTestInstance.Entity.Chats.InstancingHelper.GetChatInstance(UserAdminInstance, ChatId);

        public CreateMessageCommandTests()
        {
            _mapperMock = Substitute.For<IMapper>();
            _chatRepositoryMock = Substitute.For<IChatRepository>();
            _userRepositoryMock = Substitute.For<IUserRepository>();
            _mediaServiceMock = Substitute.For<IMediaService>();
            _mediatorMock = Substitute.For<IMediator>();

            _handler = new CreateMessageCommandHandler(
                _mediatorMock,
                _mapperMock,
                Substitute.For<IAntiVirus>(),
                Substitute.For<IAzureAdultContentDetection>(),
                _chatRepositoryMock,
                _userRepositoryMock,
                _mediaServiceMock);
        }

        [Fact]
        public async Task HandleCommand_ValidRequest_ReturnsSuccessResult()
        {
            // Arrange
            var validCommand = Command;
            ChatInstance.AddMember(UserAdminInstance, UserInstance);
            _chatRepositoryMock.GetAsync(Arg.Any<Expression<Func<Infrastructure.DatabaseEntity.Chat, bool>>>())
                .Returns(Task.FromResult(ChatInstance));
            // Act
            var result = await _handler.Handle(validCommand, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
        }

    }
}
