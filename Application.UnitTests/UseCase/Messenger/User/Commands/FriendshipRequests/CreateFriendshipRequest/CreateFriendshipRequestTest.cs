using Application.CQS.Messenger.User.Command.FriendshipRequests.CreateFriendshipRequest;
using AutoMapper;
using Infrastructure.Abstractions;
using MediatR;
using System.Linq.Expressions;

namespace Application.UnitTests.UseCase.Messenger.User.Commands.FriendshipRequests.CreateFriendshipRequest
{
    public class CreateFriendshipRequestTest
    {
        private static readonly Guid UserId = Guid.NewGuid();
        private static readonly Guid FriendUserId = Guid.NewGuid();
        private static readonly CreateFriendshipRequestCommand ValidCommand = new CreateFriendshipRequestCommand(UserId, FriendUserId, "do you want to be my friend?");

        private readonly CreateFriendshipRequestCommandHandler _handler;
        private readonly IUserRepository _userRepositoryMock;
        private readonly IMapper _mapper;
        private readonly IMediator _mediatorMock;

        private readonly Domain.Entities.Users.User UserInstance = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserInstance(UserId);
        private readonly Domain.Entities.Users.User UserFriendInstance = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserInstance(FriendUserId);

        public CreateFriendshipRequestTest()
        {
            _userRepositoryMock = Substitute.For<IUserRepository>();
            _mediatorMock = Substitute.For<IMediator>();
            _mapper = Substitute.For<IMapper>();

            _handler = new CreateFriendshipRequestCommandHandler(
                _mediatorMock,
                _mapper,
                _userRepositoryMock);
        }

        [Fact]
        public async Task Handle_ValidCommand_ReturnsSuccess()
        {
            // Arrange
            _userRepositoryMock.GetAsync(Arg.Any<Expression<Func<Infrastructure.DatabaseEntity.User, bool>>>())
                              .Returns(UserInstance, UserFriendInstance);

            // Act
            var result = await _handler.Handle(ValidCommand, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task Handle_RequestUserNotFound_ReturnsFailure()
        {
            // Arrange
            _userRepositoryMock.GetAsync(Arg.Any<Expression<Func<Infrastructure.DatabaseEntity.User, bool>>>())
                              .Returns(null,UserInstance);

            // Act
            var result = await _handler.Handle(ValidCommand, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
        }

    }
}
