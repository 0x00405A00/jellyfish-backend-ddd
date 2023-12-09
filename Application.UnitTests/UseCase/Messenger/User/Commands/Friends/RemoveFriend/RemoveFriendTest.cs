using Application.CQS.Messenger.User.Command.Friends.RemoveFriend;
using AutoMapper;
using Domain.Extension;
using Infrastructure.Abstractions;
using MediatR;
using System.Linq.Expressions;

namespace Application.UnitTests.UseCase.Messenger.User.Commands.Friends.RemoveFriend
{
    public class RemoveFriendTest
    {
        private static readonly Guid UserId = Guid.NewGuid();
        private static readonly Guid FriendUserId = Guid.NewGuid();
        private static readonly RemoveFriendCommand ValidCommand = new RemoveFriendCommand(
            ExecuteUserId: UserId,
            TargetUserId: FriendUserId);

        private readonly RemoveFriendCommandHandler _handler;
        private readonly IUserRepository _userRepositoryMock;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWorkMock;
        private readonly IMediator _mediatorMock;

        private static readonly Domain.Entities.User.User UserInstance = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserInstance(UserId);
        private static readonly Domain.Entities.User.User UserFriendInstance = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserInstance(FriendUserId);

        public RemoveFriendTest()
        {
            _userRepositoryMock = Substitute.For<IUserRepository>();
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();
            _mediatorMock = Substitute.For<IMediator>();
            _mapper = Substitute.For<IMapper>();

            _handler = new RemoveFriendCommandHandler(
                _mediatorMock,
                _mapper,
                _userRepositoryMock,
                _unitOfWorkMock);
        }

        [Fact]
        public async Task Handle_ValidCommand_ReturnsSuccess()
        {
            // Arrange
            UserInstance.AddFriend(UserInstance, UserFriendInstance);
            UserFriendInstance.AddFriend(UserFriendInstance, UserInstance);

            _userRepositoryMock.GetAsync(Arg.Any<Expression<Func<Infrastructure.DatabaseEntity.User, bool>>>())
                      .Returns(UserInstance, UserFriendInstance);

            // Act
            var result = await _handler.Handle(ValidCommand, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
        }
    }
}
