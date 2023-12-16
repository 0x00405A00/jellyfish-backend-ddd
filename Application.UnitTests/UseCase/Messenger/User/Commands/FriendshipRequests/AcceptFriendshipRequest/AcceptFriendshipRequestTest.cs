using Application.CQS.Messenger.User.Command.FriendshipRequests.AcceptFriendshipRequest;
using AutoMapper;
using Domain.Entities.User;
using Infrastructure.Abstractions;
using MediatR;
using System.Linq.Expressions;

namespace Application.UnitTests.UseCase.Messenger.User.Commands.FriendshipRequests.AcceptFriendshipRequest
{
    public class AcceptFriendshipRequestTest
    {
        private static readonly Guid UserId = Guid.NewGuid();
        private static readonly Guid FriendUserId = Guid.NewGuid();
        private static readonly AcceptFriendshipRequestCommand ValidCommand = new AcceptFriendshipRequestCommand(FriendUserId, UserId, FriendUserId);

        private readonly AcceptFriendshipRequestCommandHandler _handler;
        private readonly IUserRepository _userRepositoryMock;
        private readonly IMapper _mapper;
        private readonly IMediator _mediatorMock;

        private readonly Domain.Entities.User.User UserInstance = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserInstance(UserId);
        private readonly Domain.Entities.User.User UserFriendInstance = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserInstance(FriendUserId);

        public AcceptFriendshipRequestTest()
        {
            _userRepositoryMock = Substitute.For<IUserRepository>();
            _mediatorMock = Substitute.For<IMediator>();
            _mapper = Substitute.For<IMapper>();

            _handler = new AcceptFriendshipRequestCommandHandler(
                _mediatorMock,
                _mapper,
                _userRepositoryMock);
        }

        [Fact]
        public async Task Handle_ValidCommand_ReturnsSuccess()
        {
            // Arrange
            var request = FriendshipRequest.Create($"hey iam {UserInstance.UserName}. do you want to be my friend?", UserInstance, UserFriendInstance);
            UserFriendInstance.AddFriendshipRequest(request);
            _userRepositoryMock.GetAsync(Arg.Any<Expression<Func<Infrastructure.DatabaseEntity.User, bool>>>()).Returns(UserFriendInstance,UserFriendInstance, UserInstance);

            // Act
            var result = await _handler.Handle(ValidCommand, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task Handle_InvalidExecutor_ReturnsFailure()
        {
            // Arrange
            _userRepositoryMock.GetAsync(Arg.Any<Expression<Func<Infrastructure.DatabaseEntity.User, bool>>>()).Returns(null, UserFriendInstance, UserInstance );

            // Act
            var result = await _handler.Handle(ValidCommand, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
        }

        [Fact]
        public async Task Handle_InvalidTarget_ReturnsFailure()
        {
            // Arrange
            _userRepositoryMock.GetAsync(Arg.Any<Expression<Func<Infrastructure.DatabaseEntity.User, bool>>>()).Returns(UserInstance);

            // Act
            var result = await _handler.Handle(ValidCommand, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
        }
    }
}
