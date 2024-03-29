﻿using Application.CQS.Messenger.User.Command.Friends.RemoveFriend;
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
        private readonly IMediator _mediatorMock;

        private static readonly Domain.Entities.Users.User UserInstance = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance(UserId);
        private static readonly Domain.Entities.Users.User UserFriendInstance = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance(FriendUserId);

        public RemoveFriendTest()
        {
            _userRepositoryMock = Substitute.For<IUserRepository>();
            _mediatorMock = Substitute.For<IMediator>();
            _mapper = Substitute.For<IMapper>();

            _handler = new RemoveFriendCommandHandler(
                _mediatorMock,
                _mapper,
                _userRepositoryMock);
        }

        [Fact]
        public async Task Handle_ValidCommand_ReturnsSuccess()
        {
            // Arrange
            UserInstance.AddFriend(UserInstance.Id, UserFriendInstance.Id);
            UserFriendInstance.AddFriend(UserFriendInstance.Id, UserInstance.Id);

            _userRepositoryMock.GetAsync(Arg.Any<Expression<Func<Domain.Entities.Users.User, bool>>>())
                      .Returns(UserInstance, UserFriendInstance);

            // Act
            var result = await _handler.Handle(ValidCommand, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
        }
    }
}
