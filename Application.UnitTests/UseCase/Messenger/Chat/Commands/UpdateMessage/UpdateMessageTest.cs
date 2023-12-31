using Application.CQS.Messenger.Chat.Command.UpdateMessage;
using AutoMapper;
using Infrastructure.Abstractions;
using Infrastructure.FileSys;
using MediatR;
using Shared.DataTransferObject.Messenger;
using System.Linq.Expressions;
using System.Text;

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
            Text = "Mein init Text",
            BinaryContentBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes("ValidBase64String")),
            BinaryContentMimeType = "image/jpeg",
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
        private readonly IMessageRepository _messageRepositoryMock;
        private readonly IUserRepository _userRepositoryMock;
        private readonly IMediaService _mediaServiceMock;
        private readonly IMediator _mediatorMock;

        private static readonly Domain.Entities.Users.User UserAdminInstance = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
        private static readonly Domain.Entities.Users.User UserInstance = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
        private static readonly Domain.Entities.Chats.Chat ChatInstance = SharedTest.DomainTestInstance.Entity.Chats.InstancingHelper.GetChatInstance(UserAdminInstance);

        public UpdateMessageTest()
        {
            _mediatorMock = Substitute.For<IMediator>();
            _mapperMock = Substitute.For<IMapper>();
            _messageRepositoryMock = Substitute.For<IMessageRepository>();
            _userRepositoryMock = Substitute.For<IUserRepository>();
            _mediaServiceMock = Substitute.For<IMediaService>();
            _handler = new UpdateMessageCommandHandler(
                _mediatorMock,
                _mapperMock,
                _messageRepositoryMock,
                _userRepositoryMock,
                _mediaServiceMock);
        }

        [Fact]
        public async Task Handle_ValidRequest_ReturnsSuccessResult()
        {
            // Arrange
            var handler = _handler;

            _userRepositoryMock.GetAsync(Arg.Any<Expression<Func<Domain.Entities.Users.User, bool>>>())
                .Returns(Task.FromResult(UserInstance));

            var message = ChatInstance.AddMessage(UserInstance, "yeah mein test", null);

            _messageRepositoryMock.GetAsync(Arg.Any<Expression<Func<Domain.Entities.Messages.Message, bool>>>())
                .Returns(Task.FromResult(message));

            // Act
            var result = await handler.Handle(ValidCommand, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            // Add more assertions based on your specific success conditions
        }
    }
}
