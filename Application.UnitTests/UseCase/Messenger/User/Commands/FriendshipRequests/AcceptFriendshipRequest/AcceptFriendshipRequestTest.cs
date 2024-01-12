using Application.CQS.Messenger.User.Command.FriendshipRequests.AcceptFriendshipRequest;
using AutoMapper;
using Domain.Entities.Users;
using Domain.Extension;
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

        private readonly Domain.Entities.Users.User UserInstance = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance(UserId);
        private readonly Domain.Entities.Users.User UserFriendInstance = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance(FriendUserId);

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
            var friendshipRequestId = FriendshipRequest.NewId();
            var request = FriendshipRequest.Create(friendshipRequestId,$"hey iam {UserInstance.UserName}. do you want to be my friend?", UserInstance.Id, UserFriendInstance.Id,DateTime.UtcNow.ToTypedDateTime(),null,null);
            UserFriendInstance.AddFriendshipRequest(request);
            _userRepositoryMock.GetAsync(Arg.Any<Expression<Func<Domain.Entities.Users.User, bool>>>()).Returns(UserFriendInstance,UserFriendInstance, UserInstance);

            // Act
            var result = await _handler.Handle(ValidCommand, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task Handle_InvalidExecutor_ReturnsFailure()
        {
            // Arrange
            _userRepositoryMock.GetAsync(Arg.Any<Expression<Func<Domain.Entities.Users.User, bool>>>()).Returns(null, UserFriendInstance, UserInstance );

            // Act
            var result = await _handler.Handle(ValidCommand, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
        }

        [Fact]
        public async Task Handle_InvalidTarget_ReturnsFailure()
        {
            // Arrange
            _userRepositoryMock.GetAsync(Arg.Any<Expression<Func<Domain.Entities.Users.User, bool>>>()).Returns(UserInstance);

            // Act
            var result = await _handler.Handle(ValidCommand, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
        }
    }
}
