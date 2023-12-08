using Application.CQS.Messenger.Chat.Command.AssignChatAdmin;
using AutoMapper;
using Infrastructure.Abstractions;
using Infrastructure.FileSys;

namespace Application.UnitTests.UseCase.Messenger.Chat.Commands.AssignChatAdmin
{
    public class AssignChatAdminCommandHandlerTests
    {
        private static readonly AssignChatAdminCommand Command = new AssignChatAdminCommand(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());
        
        private readonly AssignChatAdminCommandHandler _handler;
        private readonly IMapper _mapperMock;
        private readonly IUserTypeRepository _chatRepositoryMock;
        private readonly IUserRepository _userRepositoryMock;
        private readonly IMediaService _mediaServiceMock;
        private readonly IUnitOfWork _unitOfWorkMock;

        public AssignChatAdminCommandHandlerTests()
        {
            _mapperMock = Substitute.For<IMapper>();
            _chatRepositoryMock = Substitute.For<IUserTypeRepository>();
            _userRepositoryMock = Substitute.For<IUserRepository>();
            _mediaServiceMock = Substitute.For<IMediaService>();
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();

            _handler = new AssignChatAdminCommandHandler(
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
            var command = Command;

            // Implement additional Arrange steps if needed

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task Handle_InvalidRequest_ReturnsFailureResult()
        {
            // Arrange
            var invalidCommand = Command with { ChatId = Guid.Empty };

            // Implement additional Arrange steps if needed

            // Act
            var result = await _handler.Handle(invalidCommand, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
        }
    }
}
