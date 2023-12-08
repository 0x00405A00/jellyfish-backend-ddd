using Application.CQS.Messenger.Chat.Command.CreateMessage;
using AutoMapper;
using Infrastructure.Abstractions;
using Infrastructure.FileSys;
using MediatR;
using Shared.DataTransferObject.Messenger;

namespace Application.UnitTests.UseCase.Messenger.Chat.Commands.CreateMessage
{
    public class CreateMessageCommandTests
    {
        private static readonly CreateMessageCommand Command = new CreateMessageCommand(
                ChatId: Guid.NewGuid(),
                MessageCreatorId: Guid.NewGuid(),
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
        private readonly IUnitOfWork _unitOfWorkMock;
        private readonly IMediator _mediatorMock;

        public CreateMessageCommandTests()
        {
            _mapperMock = Substitute.For<IMapper>();
            _chatRepositoryMock = Substitute.For<IChatRepository>();
            _userRepositoryMock = Substitute.For<IUserRepository>();
            _mediaServiceMock = Substitute.For<IMediaService>();
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();
            _mediatorMock = Substitute.For<IMediator>();

            _handler = new CreateMessageCommandHandler(
                _mediatorMock,
                _mapperMock,
                Substitute.For<IAntiVirus>(),
                Substitute.For<IAzureAdultContentDetection>(),
                _chatRepositoryMock,
                _userRepositoryMock,
                _mediaServiceMock,
                _unitOfWorkMock);
        }

        [Fact]
        public void CreateMessageCommand_Creation_Success()
        {
            // Arrange
            var chatId = Guid.NewGuid();
            var messageCreatorId = Guid.NewGuid();
            var messages = new List<MessageDTO>
            {
                new MessageDTO
                {
                    Text = "Hello, World!",
                    CreatedTime = DateTime.Now,
                    // Add other properties as needed
                }
            };

            // Act
            var createMessageCommand = new CreateMessageCommand(chatId, messageCreatorId, messages);

            // Assert
            Assert.NotNull(createMessageCommand);
            Assert.Equal(chatId, createMessageCommand.ChatId);
            Assert.Equal(messageCreatorId, createMessageCommand.MessageCreatorId);
            Assert.Equal(messages, createMessageCommand.Messages);
        }

        [Fact]
        public async Task HandleCommand_ValidRequest_ReturnsSuccessResult()
        {
            // Arrange
            var validCommand = Command;

            var handler = CreateMessageCommandHandlerWithMocks();

            // Act
            var result = await handler.Handle(validCommand, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
            Assert.IsType<List<MessageDTO>>(result.Value);
            // Add more assertions based on your specific success conditions
        }

        private CreateMessageCommandHandler CreateMessageCommandHandlerWithMocks()
        {
            var mapperMock = Substitute.For<IMapper>();
            var chatRepositoryMock = Substitute.For<IChatRepository>();
            var userRepositoryMock = Substitute.For<IUserRepository>();
            var mediaServiceMock = Substitute.For<IMediaService>();
            var unitOfWorkMock = Substitute.For<IUnitOfWork>();
            var mediatorMock = Substitute.For<IMediator>();

            return new CreateMessageCommandHandler(
                mediatorMock,
                mapperMock,
                Substitute.For<IAntiVirus>(),
                Substitute.For<IAzureAdultContentDetection>(),
                chatRepositoryMock,
                userRepositoryMock,
                mediaServiceMock,
                unitOfWorkMock);
        }
    }
}
