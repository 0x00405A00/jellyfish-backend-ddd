using Application.CQS.Messenger.Chat.Command.DeleteMessage;
using AutoMapper;
using Infrastructure.Abstractions;
using Infrastructure.FileSys;
using MediatR;

namespace Application.UnitTests.UseCase.Messenger.Chat.Commands.DeleteMessage
{
    public class DeleteMessageTest
    {
        private static readonly Guid DeletedById = Guid.NewGuid();
        private static readonly Guid ChatId = Guid.NewGuid();
        private static readonly Guid MessageId = Guid.NewGuid();

        private static readonly DeleteMessageCommand ValidCommand = new DeleteMessageCommand(DeletedById, ChatId, MessageId);
        private static readonly DeleteMessageCommand InvalidDeletedByCommand = ValidCommand with { DeletedById = Guid.Empty };
        private static readonly DeleteMessageCommand InvalidChatIdCommand = ValidCommand with { ChatId = Guid.Empty };
        private static readonly DeleteMessageCommand InvalidMessageIdCommand = ValidCommand with { MessageId = Guid.Empty };

        private readonly IUserTypeRepository _chatRepositoryMock;
        private readonly IUserRepository _userRepositoryMock;
        private readonly IMediaService _mediaServiceMock;
        private readonly IUnitOfWork _unitOfWorkMock;
        private readonly IMediator _mediatorMock;
        private readonly IMapper _mapperMock;

        private readonly DeleteMessageCommandHandler _handler;

        public DeleteMessageTest()
        {
            _chatRepositoryMock = Substitute.For<IUserTypeRepository>();
            _userRepositoryMock = Substitute.For<IUserRepository>();
            _mediaServiceMock = Substitute.For<IMediaService>();
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();
            _mediatorMock = Substitute.For<IMediator>();
            _mapperMock = Substitute.For<IMapper>();

            _handler = new DeleteMessageCommandHandler(
                _mediatorMock,
                _mapperMock,
                _chatRepositoryMock,
                _userRepositoryMock,
                _mediaServiceMock,
                _unitOfWorkMock);
        }

        [Fact]
        public async Task Handle_ValidRequest_ReturnsSuccessResult()
        {
            // Arrange

            // Implement additional Arrange steps if needed

            // Act
            var result = await _handler.Handle(ValidCommand, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(MessageId, result.Value);
            // Add more assertions based on your specific success conditions
        }

        [Fact]
        public async Task Handle_InvalidDeletedBy_ReturnsFailureResult()
        {
            // Arrange
            // Act
            var result = await _handler.Handle(InvalidDeletedByCommand, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            // Add more assertions based on your specific failure conditions
        }

        [Fact]
        public async Task Handle_InvalidChatId_ReturnsFailureResult()
        {
            // Arrange

            // Act
            var result = await _handler.Handle(InvalidChatIdCommand, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            // Add more assertions based on your specific failure conditions
        }

        [Fact]
        public async Task Handle_InvalidMessageId_ReturnsFailureResult()
        {
            // Arrange

            // Act
            var result = await _handler.Handle(InvalidMessageIdCommand, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            // Add more assertions based on your specific failure conditions
        }

        // Add more [Fact] methods as needed
    }
}
