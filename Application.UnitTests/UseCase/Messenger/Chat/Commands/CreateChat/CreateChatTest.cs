using Application.CQS.Messenger.Chat.Command.CreateChat;
using AutoMapper;
using Infrastructure.Abstractions;
using Infrastructure.FileSys;
using Shared.DataTransferObject.Messenger;

namespace Application.UnitTests.UseCase.Messenger.Chat.Commands.CreateChat
{

    public class CreateChatCommandHandlerTests
    {
        private static readonly CreateChatCommand Command = new(
            ChatOwnerUuid: Guid.NewGuid(),
            ChatName: "TestChat",
            ChatDescription: "Test Description",
            Members: new List<Guid> { Guid.NewGuid(), Guid.NewGuid() },
            Picture: "Base64EncodedPicture", // Replace with an actual Base64 encoded picture
            PictureMimeType: "image/png");

        private readonly CreateChatCommandHandler _handler;
        private readonly IMapper _mapperMock;
        private readonly IChatRepository _chatRepositoryMock;
        private readonly IUserRepository _userRepositoryMock;
        private readonly IMediaService _mediaServiceMock;
        private readonly IUnitOfWork _unitOfWorkMock;

        public CreateChatCommandHandlerTests()
        {
            _mapperMock = Substitute.For<IMapper>();
            _chatRepositoryMock = Substitute.For<IChatRepository>();
            _userRepositoryMock = Substitute.For<IUserRepository>();
            _mediaServiceMock = Substitute.For<IMediaService>();
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();

            _handler = new CreateChatCommandHandler(
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

            // Act
            var result = await _handler.Handle(Command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task Handle_InvalidRequest_ReturnsFailureResult()
        {
            // Arrange
            var invalidCommand = Command with { ChatOwnerUuid = Guid.Empty };

            // Act
            var result = await _handler.Handle(invalidCommand, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
        }
    }
}
