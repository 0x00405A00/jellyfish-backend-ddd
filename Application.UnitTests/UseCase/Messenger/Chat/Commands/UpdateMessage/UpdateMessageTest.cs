using Application.CQS.Messenger.Chat.Command.UpdateMessage;
using AutoMapper;
using Infrastructure.Abstractions;
using Infrastructure.FileSys;
using MediatR;
using Shared.DataTransferObject.Messenger;
using System.Linq.Expressions;

namespace Application.UnitTests.UseCase.Messenger.Chat.Commands.UpdateMessage
{
    public class UpdateMessageTest
    {
        private static readonly Guid ModifiedById = Guid.NewGuid();
        private static readonly Guid ChatId = Guid.NewGuid();
        private static readonly Guid MessageId = Guid.NewGuid();
        private static readonly MessageDTO ValidMessageDTO = new MessageDTO
        {
            Uuid = Guid.NewGuid(),
            ChatId = ChatId,
            OwnerUuid = Guid.NewGuid(),
            Text = "ValidText",
            BinaryContentBase64 = "ValidBase64String",
            BinaryContentMimeType = "image/png",
            CreatedTime = DateTime.Now,
            LastModifiedTime = DateTime.Now,
            DeletedTime = null,
            CreatedByUserUuid = Guid.NewGuid(),
            LastModifiedByUserUuid = Guid.NewGuid(),
            DeletedByUserUuid = null
        };

        private static readonly UpdateMessageCommand ValidCommand = new UpdateMessageCommand(
            ModifiedById,
            ChatId,
            MessageId,
            ValidMessageDTO);

        // Add more commands for invalid scenarios if needed

        private readonly UpdateMessageCommandHandler _handler;
        private readonly IMapper _mapperMock;
        private readonly IChatRepository _chatRepositoryMock;
        private readonly IUserRepository _userRepositoryMock;
        private readonly IMediaService _mediaServiceMock;
        private readonly IUnitOfWork _unitOfWorkMock;
        private readonly IMediator _mediatorMock;

        private static readonly Domain.Entities.User.User UserAdminInstance = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserInstance();
        private static readonly Domain.Entities.User.User UserInstance = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserInstance();
        private static readonly Domain.Entities.Chats.Chat ChatInstance = SharedTest.DomainTestInstance.Entity.Chats.InstancingHelper.GetChatInstance(UserAdminInstance);

        public UpdateMessageTest()
        {
            _mediatorMock = Substitute.For<IMediator>();
            _mapperMock = Substitute.For<IMapper>();
            _chatRepositoryMock = Substitute.For<IChatRepository>();
            _userRepositoryMock = Substitute.For<IUserRepository>();
            _mediaServiceMock = Substitute.For<IMediaService>();
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();
            _handler = new UpdateMessageCommandHandler(
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
            var handler = _handler;

            _userRepositoryMock.GetAsync(Arg.Any<Expression<Func<Infrastructure.DatabaseEntity.User, bool>>>())
                .Returns(Task.FromResult(UserInstance));

            _chatRepositoryMock.GetAsync(Arg.Any<Expression<Func<Infrastructure.DatabaseEntity.Chat, bool>>>())
                .Returns(Task.FromResult(ChatInstance));

            // Act
            var result = await handler.Handle(ValidCommand, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            // Add more assertions based on your specific success conditions
        }
    }
}
