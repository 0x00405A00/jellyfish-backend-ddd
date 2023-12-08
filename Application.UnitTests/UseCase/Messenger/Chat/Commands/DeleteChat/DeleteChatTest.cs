﻿using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Application.CQS.Messenger.Chat.Command.DeleteChat;
using Domain.Entities.Chats;
using Domain.Entities.Chats.Exception;
using Infrastructure.Abstractions;
using NSubstitute;
using Xunit;

namespace Application.UnitTests.UseCase.Messenger.Chat.Commands.DeleteChat
{
    public class DeleteChatTest
    {
        private static readonly Guid ChatId = Guid.NewGuid();
        private static readonly Guid DeletedByUserId = Guid.NewGuid();

        private static readonly DeleteChatCommand ValidCommand = new DeleteChatCommand(ChatId, DeletedByUserId);
        private static readonly DeleteChatCommand InvalidUserCommand = ValidCommand with { DeletedByUserId = Guid.Empty };
        private static readonly DeleteChatCommand InvalidChatCommand = ValidCommand with { ChatId = Guid.Empty };

        private readonly DeleteChatCommandHandler _handler;
        private readonly IChatRepository _chatRepositoryMock;
        private readonly IUserRepository _userRepositoryMock;
        private readonly IUnitOfWork _unitOfWorkMock;

        private static readonly Domain.Entities.User.User UserAdminInstance = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserInstance();
        private static readonly Domain.Entities.User.User UserInstance = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserInstance();
        private static readonly Domain.Entities.Chats.Chat ChatInstance = SharedTest.DomainTestInstance.Entity.Chats.InstancingHelper.GetChatInstance(UserAdminInstance);

        public DeleteChatTest()
        {
            _chatRepositoryMock = Substitute.For<IChatRepository>();
            _userRepositoryMock = Substitute.For<IUserRepository>();
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();

            _handler = new DeleteChatCommandHandler(_chatRepositoryMock, _userRepositoryMock, _unitOfWorkMock);
        }

        [Fact]
        public async Task Handle_ValidRequest_ReturnsSuccessResult()
        {
            // Arrange
            _userRepositoryMock.GetAsync(Arg.Any<Expression<Func<Infrastructure.DatabaseEntity.User, bool>>>())
                .Returns(Task.FromResult(UserAdminInstance));

            _chatRepositoryMock.GetAsync(Arg.Any<Expression<Func<Infrastructure.DatabaseEntity.Chat, bool>>>())
                .Returns(Task.FromResult(ChatInstance));

            // Act
            var result = await _handler.Handle(ValidCommand, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(ChatId, result.Value);
            // Add more assertions based on your specific success conditions
        }

        [Fact]
        public async Task Handle_InvalidUser_ReturnsFailureResult()
        {
            // Arrange
            _userRepositoryMock.GetAsync(Arg.Any<Expression<Func<Infrastructure.DatabaseEntity.User, bool>>>())
                .Returns(Task.FromResult<Domain.Entities.User.User>(null));

            // Act
            var result = await _handler.Handle(InvalidUserCommand, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            // Add more assertions based on your specific failure conditions
        }

        [Fact]
        public async Task Handle_InvalidChat_ReturnsFailureResult()
        {
            // Arrange
            _userRepositoryMock.GetAsync(Arg.Any<Expression<Func<Infrastructure.DatabaseEntity.User, bool>>>())
                .Returns(Task.FromResult(UserAdminInstance));

            _chatRepositoryMock.GetAsync(Arg.Any<Expression<Func<Infrastructure.DatabaseEntity.Chat, bool>>>())
                .Returns(Task.FromResult<Domain.Entities.Chats.Chat>(null));

            // Act
            var result = await _handler.Handle(InvalidChatCommand, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            // Add more assertions based on your specific failure conditions
        }

        [Fact]
        public async Task Handle_UserIsNoAdmin_ReturnsFailureResult()
        {
            // Arrange
            _userRepositoryMock.GetAsync(Arg.Any<Expression<Func<Infrastructure.DatabaseEntity.User, bool>>>())
                .Returns(Task.FromResult(UserInstance));

            _chatRepositoryMock.GetAsync(Arg.Any<Expression<Func<Infrastructure.DatabaseEntity.Chat, bool>>>())
                .Returns(Task.FromResult(ChatInstance));

            _chatRepositoryMock.Remove(Arg.Any<Domain.Entities.Chats.Chat>());

            // Act
            var result = await _handler.Handle(ValidCommand, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            // Add more assertions based on your specific failure conditions
        }
    }
}
