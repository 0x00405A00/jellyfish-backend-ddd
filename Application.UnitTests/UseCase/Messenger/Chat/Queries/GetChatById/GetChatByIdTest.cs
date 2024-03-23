using Application.CQS.Messenger.Chat.Queries.GetChatById;
using AutoMapper;
using Infrastructure.Abstractions;
using System.Linq.Expressions;

namespace Application.UnitTests.UseCase.Messenger.Chat.Queries.GetChatById
{
    public class GetChatByIdTest
    {
        private static readonly Guid ChatId = Guid.NewGuid();
        private static readonly GetChatByIdQuery ValidQuery = new GetChatByIdQuery(ChatId);

        // Add more queries for invalid scenarios if needed

        private readonly GetChatByIdQueryHandler _handler;
        private readonly IMapper _mapperMock;
        private readonly IChatRepository _chatRepositoryMock;

        private static readonly Domain.Entities.Users.User UserAdminInstance = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
        private static readonly Domain.Entities.Users.User UserInstance = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
        private static readonly Domain.Entities.Chats.Chat ChatInstance = SharedTest.DomainTestInstance.Entity.Chats.InstancingHelper.GetChatInstance(UserAdminInstance);

        public GetChatByIdTest()
        {
            _mapperMock = Substitute.For<IMapper>();
            _chatRepositoryMock = Substitute.For<IChatRepository>();
            _handler = new GetChatByIdQueryHandler(_mapperMock, _chatRepositoryMock);
        }

        [Fact]
        public async Task Handle_ValidRequest_ReturnsSuccessResult()
        {
            // Arrange
            var handler = _handler;

            _chatRepositoryMock.GetAsync(Arg.Any<Expression<Func<Domain.Entities.Chats.Chat, bool>>>())
                .Returns(Task.FromResult(ChatInstance));

            // Act
            var result = await handler.Handle(ValidQuery, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            // Add more assertions based on your specific success conditions
        }

        [Fact]
        public async Task Handle_InvalidChat_ReturnsFailureResult()
        {
            // Arrange
            var handler = _handler;

            _chatRepositoryMock.GetAsync(Arg.Any<Expression<Func<Domain.Entities.Chats.Chat, bool>>>())
                .Returns(Task.FromResult<Domain.Entities.Chats.Chat>(null));

            // Act
            var result = await handler.Handle(ValidQuery, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            // Add more assertions based on your specific failure conditions
        }
    }
}
